using UnityEngine;

public class AreaEffect : MonoBehaviour
{
    public string effectType;
    public float previousValue;
    
    void Start()
    {
        
    }

    // Update is called once per frame
    void OnTriggerEnter(Collider obj) {
        Debug.Log("entered:" + obj);
        if(obj.tag == "Player") {
            PlayerMovement playerMov = obj.GetComponent<PlayerMovement>();
            switch(effectType) {
                case "slow":
                    previousValue = playerMov.moveSpeed;

                    playerMov.canSprint = false;
                    playerMov.moveSpeed /= 2;
                    break;
                case "nograv":
                    previousValue = playerMov.gravity;
                    playerMov.gravity = 0f;
                    break;
                case "nojump":
                    playerMov.canJump = false;
                    break;
                case "nomove":
                    playerMov.canMove = false;
                    break;
                case "lift":
                    previousValue = playerMov.gravity;
                    playerMov.gravity = 10f;
                    break;
                case "jumpboost":
                    previousValue = playerMov.jumpPower;
                    playerMov.jumpPower *= 5;
                    break;
                case "":
                    Debug.Log("nothing");
                    break;
            }
        }
        
    }

    void OnTriggerExit(Collider obj) {
        if(obj.tag == "Player") {
            PlayerMovement playerMov = obj.GetComponent<PlayerMovement>();
            switch(effectType) {
                case "slow":
                    playerMov.moveSpeed = previousValue;
                    playerMov.canSprint = true;
                    break;
                case "nograv":
                    playerMov.gravity = previousValue;
                    playerMov.time = 0;
                    break;
                case "nojump":
                    playerMov.canJump = true;
                    break;
                case "nomove":
                    playerMov.canMove = true;
                    break;
                case "lift":
                    playerMov.gravity = previousValue;
                    playerMov.time = 0;
                    break;
                case "jumpboost":
                    playerMov.jumpPower = previousValue;
                    break;
                case "":
                    Debug.Log("nothing");
                    break;
            }
        }
    }
}
