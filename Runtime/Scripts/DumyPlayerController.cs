using UnityEngine;

public class DumyPlayerController : MonoBehaviour
{

    private CharacterController controller;

    private Vector3 direction;
    public float speed;

    private int desiredLane=1; //0:left, 1:middle, 2:right
    public float laneDistance=4f;


    public float jumpForce;
    public float Gravity=-20f;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        controller=GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        controller.Move(direction*Time.deltaTime);
        direction.z=speed;


        if(controller.isGrounded){
            direction.y=-1;
            if(Input.GetKeyDown(KeyCode.UpArrow)){
                 Jump();
            }
        }
        else{
            direction.y+= Gravity * Time.deltaTime;
        }
        

        if(Input.GetKeyDown(KeyCode.RightArrow)){
            desiredLane++;
            if(desiredLane==3){
                desiredLane=2;
            }
        }
        if(Input.GetKeyDown(KeyCode.LeftArrow)){
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
        controller.Move(direction * Time.fixedDeltaTime);
    }


    private void Jump(){
        direction.y=jumpForce;
    }

    

    private void OnControllerColliderHit(ControllerColliderHit hit)
    {
        Debug.Log("Hit detected with: " + hit.gameObject.name);
        if (hit.transform.tag == "Obstacle")
        {
            Debug.Log("obstacle");
            PlayerManager.gameOver = true;
        }
    }
}
