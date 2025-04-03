using UnityEngine;

public class Coin : MonoBehaviour
{

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(40* Time.deltaTime, 0, 0);
    }

    private void OnTriggerEnter(Collider other){
        if(other.tag=="Player"){
            PlayerManager.numberOfCoins +=1;
            Debug.Log("coins: "+PlayerManager.numberOfCoins);
            Destroy(gameObject);
        }
    }
}
