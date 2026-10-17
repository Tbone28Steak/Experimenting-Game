using UnityEngine;
using UnityEngine.Events;

public class Interaction : MonoBehaviour
{
    public bool canGrab;
    public bool canUse;
    public bool canInteract;
    public UnityEvent onUse;
    public UnityEvent onInteract;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if(canGrab){
            if (GetComponent<Rigidbody>() == null){gameObject.AddComponent<Rigidbody>();}
        }


    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Use() {
        Debug.Log("using object");

        if(onUse != null) {
            onUse.Invoke();
        }

        
    }

    public void Interact() {
        Debug.Log("interacting with object");

        if(onInteract != null) {
            onInteract.Invoke();
        }
    }
}
