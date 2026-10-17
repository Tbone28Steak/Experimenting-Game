using System;
using UnityEngine;
using UnityEngine.Events;

public class TriggerController : MonoBehaviour
{
    public string triggerType;
    // weight
    // weight_player
    // button
    public string triggerTarget;
    public Boolean activated = false;
    public GameObject activeObject;
    public UnityEvent onActivated;
    public UnityEvent onDeactivated;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }



    void OnTriggerEnter(Collider obj) {
        Debug.Log(obj + "|" + obj.tag);

        if(activeObject == null) {
            activeObject = obj.gameObject;
            activated = true;

            switch(triggerType) {
                case "weight":  
                    // GameObject.Find(triggerTarget).GetComponent<TriggerDoor>().Trigger();
                    onActivated.Invoke();
                break;
                case "weight_box":
                    if(obj.tag == "TriggerButton") {
                        // GameObject.Find(triggerTarget).GetComponent<TriggerDoor>().Trigger();
                        onActivated.Invoke();
                    }
                break;
                case "weight_player":
                    if(obj.tag == "Player") {
                        // GameObject.Find(triggerTarget).GetComponent<TriggerDoor>().Trigger();
                        onActivated.Invoke();
                    }
                break;
                case "toggle":
                break;
            }
        }
    }

    void OnTriggerExit(Collider obj) {
        if(obj.gameObject == activeObject) {
            activeObject = null;
            activated = false;

            switch(triggerType) {
                case "weight":  
                    // GameObject.Find(triggerTarget).GetComponent<TriggerDoor>().UnTrigger();
                    onDeactivated.Invoke();
                break;
                case "weight_box":
                    if(obj.tag == "TriggerButton") {
                        // GameObject.Find(triggerTarget).GetComponent<TriggerDoor>().UnTrigger();
                        onDeactivated.Invoke();
                    }
                break;
                case "weight_player":
                    if(obj.tag == "Player") {
                        // GameObject.Find(triggerTarget).GetComponent<TriggerDoor>().UnTrigger();
                        onDeactivated.Invoke();
                    }
                break;
                case "toggle":
                break;
            }
        }
    }
}
