using TMPro;
using Unity.VisualScripting;
using UnityEditor.TerrainTools;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class PlayerInteraction : MonoBehaviour

{
    [HideInInspector] public PlayerInput playerInput;
    [HideInInspector] public CharacterController characterController;
    public GameObject playerCamera;
    public GameObject hand;


    public bool interactInput;
    public bool useInput;
    public float interactionRange = 5f;
    public GameObject grabbedObject;
    public Interaction grabbedObjectInteraction;


    public GameObject interactionPrompt;
    public GameObject grabPrompt;
    public GameObject usePrompt;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        interactionPrompt = GameObject.Find("InteractPrompt");
        grabPrompt = GameObject.Find("GrabPrompt");
        usePrompt = GameObject.Find("UsePrompt");
        usePrompt.SetActive(false);

        playerInput = GetComponent<PlayerInput>();
        hand = GameObject.Find("Hand");
    }

    // Update is called once per frame
    void Update()
    {
        
        interactInput = playerInput.actions["Interact"].WasPressedThisFrame();
        useInput = playerInput.actions["Use"].WasPressedThisFrame();
        Interaction();
    }

    void Interaction()
    {
        Ray interactionRay = new(playerCamera.transform.position, playerCamera.transform.forward);
        Physics.Raycast(interactionRay, out RaycastHit detectHit, interactionRange);
        
        // interaction text UI
        if(detectHit.collider) {
            Interaction detectInteraction = detectHit.collider.GetComponent<Interaction>();
            if(detectInteraction != null) {
                if(detectInteraction.canGrab){grabPrompt.SetActive(true);}
                if(detectInteraction.canInteract){interactionPrompt.SetActive(true);}
            }
            else {
                interactionPrompt.SetActive(false);
                grabPrompt.SetActive(false);
            }
        }
        else {
            interactionPrompt.SetActive(false);
            grabPrompt.SetActive(false);
        }
            

        // interaction logic
        if (interactInput) {
            if(grabbedObject) {
                Drop(grabbedObject);
                (grabbedObject, grabbedObjectInteraction) = (null, null);   
            }
            else {
                Physics.Raycast(interactionRay, out RaycastHit interactionHit, interactionRange);
                Collider interactionCollider = interactionHit.collider;

                if(interactionCollider) {
                    Interaction interaction = interactionCollider.GetComponent<Interaction>();
                

                    Debug.Log(interactionHit.collider);

                    if(interaction != null){
                        if(interaction.canGrab && !grabbedObject) {
                            grabbedObject = interaction.gameObject;
                            grabbedObjectInteraction = interaction;

                            Grab(grabbedObjectInteraction, grabbedObject);                                
                        } 

                        if(interaction.canInteract){Interact(interaction);}
                    }
                }
            }
        }
        
        //interaction with held object
        if(useInput) {
            if(grabbedObject && grabbedObjectInteraction.canUse) {
                grabbedObjectInteraction.Use();
            }
        }
    }

    void Grab(Interaction interaction, GameObject grabbedObject) {
        Debug.Log("grabbing");

        if(interaction.canUse){usePrompt.SetActive(true);}
        else{usePrompt.SetActive(false);}

        Rigidbody rb = grabbedObject.GetComponent<Rigidbody>();
        if (rb != null){rb.isKinematic = true;}

        grabbedObject.layer = 2;
        grabbedObject.transform.SetParent(hand.transform);
        grabbedObject.transform.localPosition = new Vector3(0, 0, 0);
        grabbedObject.transform.rotation = new Quaternion(0, 0, 0, 0);
    }

    void Drop(GameObject grabbedObject) {
        Debug.Log("dropping");

        Rigidbody rb = grabbedObject.GetComponent<Rigidbody>();
        if (rb != null){rb.isKinematic = false;}
        grabbedObject.transform.SetParent(null);
        grabbedObject.layer = 0;
        usePrompt.SetActive(false);
        
    }

    void Interact(Interaction interaction) {
        Debug.Log("interacting");
        interaction.Interact();
    }
}
