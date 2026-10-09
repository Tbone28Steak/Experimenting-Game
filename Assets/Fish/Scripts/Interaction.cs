using UnityEngine;

public class Interaction : MonoBehaviour
{
    public bool canGrab;
    public bool canUse;
    public string useFunction;
    public bool canInteract;
    public string interactFunction;
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
        Debug.Log("double sup");

        switch(useFunction) {
            case "test":
                transform.Rotate(15, 0, 0);
                break;
            case "":
                Debug.Log("no use function found");
                break;
            
        }
    }

    public void Interact() {
        Debug.Log("sup");

        switch(interactFunction) {
            case "test":
                transform.Rotate(15, 0, 0);
                break;
            case "":
                Debug.Log("no interact function found");
                break;
            
        }
    }
}
