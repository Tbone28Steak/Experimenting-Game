using System;
using UnityEngine;
using UnityEngine.UIElements;

public class TriggerDoor : MonoBehaviour
{
    public string openAnimation;
    public Vector3 initialPosition;
    public Vector3 currentPosition;    
    public Quaternion initialRotation;
    public Quaternion currentRotation;

    public Boolean isOpen = false;
    public float doorTime;
    public float doorSpeed = 1;
    public float moveDistance = 1;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        currentRotation = transform.rotation;
        initialRotation = transform.rotation;

        currentPosition = transform.position;
        initialPosition = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if(doorTime < 1) {doorTime += (0.5f * Time.deltaTime) * doorSpeed;}

        switch(openAnimation) {
            case "rotate":
                // if(isOpen) {currentRotation.y = Mathf.Lerp(currentRotation.y, initialRotation.y + 1, doorTime);}
                // else       {currentRotation.y = Mathf.Lerp(currentRotation.y, initialRotation.y,      doorTime);}
            break;
            case "slideHorizontal":
                if(isOpen) {currentPosition.x = Mathf.Lerp(currentPosition.x, initialPosition.x + moveDistance, doorTime);}
                else       {currentPosition.x = Mathf.Lerp(currentPosition.x, initialPosition.x,                doorTime);}
            break;
            case "slideVertical":
                if(isOpen) {currentPosition.y = Mathf.Lerp(currentPosition.y, initialPosition.y + moveDistance, doorTime);}
                else       {currentPosition.y = Mathf.Lerp(currentPosition.y, initialPosition.y,                doorTime);}
            break;
        }

        transform.rotation = currentRotation;
        transform.position = currentPosition;
        
    }

    public void Trigger() {
        doorTime = 0;
        isOpen = true;
    }

    public void UnTrigger() {
        doorTime = 0;
        isOpen = false;
    }
}
