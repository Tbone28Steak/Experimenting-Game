using System;
using UnityEngine;

public class TriggerDoor : MonoBehaviour
{
    public AnimationType openAnimation;

    public enum AnimationType
    {
        rotate,
        slideHorizontal,
        slideVertical
    }
    public Vector3 initialPosition;
    public Vector3 currentPosition;    
    public Vector3 initialRotation;
    public Vector3 currentRotation;

    public Boolean isOpen = false;
    public float doorTime;
    public float doorSpeed = 1;
    public float moveDistance = 1;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        currentRotation = transform.eulerAngles;
        initialRotation = transform.eulerAngles;

        currentPosition = transform.position;
        initialPosition = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if(doorTime < 1) {doorTime += (0.5f * Time.deltaTime) * doorSpeed;}

        switch(openAnimation) {
            case AnimationType.rotate:            
                if(isOpen) {currentRotation.y = Mathf.Lerp(currentRotation.y, initialRotation.y + 90, doorTime);}
                else       {currentRotation.y = Mathf.Lerp(currentRotation.y, initialRotation.y,      doorTime);}
            break;
            case AnimationType.slideHorizontal:
                if(isOpen) {currentPosition.x = Mathf.Lerp(currentPosition.x, initialPosition.x + moveDistance, doorTime);}
                else       {currentPosition.x = Mathf.Lerp(currentPosition.x, initialPosition.x,                doorTime);}
            break;
            case AnimationType.slideVertical:
                if(isOpen) {currentPosition.y = Mathf.Lerp(currentPosition.y, initialPosition.y + moveDistance, doorTime);}
                else       {currentPosition.y = Mathf.Lerp(currentPosition.y, initialPosition.y,                doorTime);}
            break;
        }

        transform.eulerAngles = currentRotation;
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
