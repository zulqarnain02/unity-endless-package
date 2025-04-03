using UnityEngine;
using System.Collections;
public class PlayerController : MonoBehaviour
{

    private CharacterController controller;

    private Vector3 direction;
    public float speed;

    public float maxSpeed;
    private int desiredLane=1; //0:left, 1:middle, 2:right
    public float laneDistance=4f;


    public float jumpForce;
    public float Gravity=-20f;

    private bool isSliding=false;
    public Animator animator;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        controller=GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        if(!PlayerManager.isGameStarted){
            return;
        }

        //increase speed
        if(speed<maxSpeed){
            speed+=0.1f*Time.deltaTime;
        }
        
        animator.SetBool("IsGameStarted", true);

        controller.Move(direction*Time.deltaTime);
        direction.z=speed;

        animator.SetBool("StartJump", !controller.isGrounded);
        if(controller.isGrounded){
            direction.y=-1;
            if(SwipeManager.swipeUp){
                 Jump();
            }
        }
        else{
            direction.y+= Gravity * Time.deltaTime;
        }
        

        if(SwipeManager.swipeDown && !isSliding){
            StartCoroutine(Slide());
        }

        if(SwipeManager.swipeRight){
            desiredLane++;
            if(desiredLane==3){
                desiredLane=2;
            }
        }
        if(SwipeManager.swipeLeft){
            desiredLane--;
            if(desiredLane==-1){
                desiredLane=0;
            }
        }

        Vector3 targetPosition =transform.position.z*transform.forward + transform.position.y * transform.up;
        if(desiredLane==0){
            targetPosition+=Vector3.left*laneDistance;

        }else if(desiredLane==2){
            targetPosition+=Vector3.right*laneDistance;
            
        }


        //transform.position=Vector3.Lerp(transform.position,targetPosition,70*Time.deltaTime);
        //controller.center=controller.center;

        if(transform.position==targetPosition) return;

        Vector3 diff=targetPosition-transform.position;

        Vector3 moveDir=diff.normalized*25*Time.deltaTime;
        if(moveDir.sqrMagnitude<diff.sqrMagnitude){
            controller.Move(moveDir);
        }else{
            controller.Move(diff);
        }
    }

    public void FixedUpdate(){
        if(!PlayerManager.isGameStarted){
            return;
        }
        controller.Move(direction * Time.fixedDeltaTime);
    }


    private void Jump(){
        direction.y=jumpForce;
    }

    

    private void OnControllerColliderHit(ControllerColliderHit hit)
    {
        //Debug.Log("Hit detected with: " + hit.gameObject.name);
        if (hit.transform.tag == "Obstacle")
        {
            Debug.Log("obstacle");
            PlayerManager.gameOver = true;
        }
    }

    private IEnumerator Slide(){
        isSliding=true;
        animator.SetBool("IsSliding", true);
        controller.center=new Vector3(0, -0.5f, 0);
        controller.height=1;

        yield return new WaitForSeconds(0.6f);

        controller.center=new Vector3(0, 0, 0);
        controller.height=2;
        animator.SetBool("IsSliding", false);
        isSliding=false;
    }
}
