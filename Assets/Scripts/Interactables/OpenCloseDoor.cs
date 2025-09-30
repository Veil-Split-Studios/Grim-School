using System.Collections;
using UnityEngine;

public class OpenCloseDoor : Interact
{
    [SerializeField]
    private Animator doorAnimator; // Reference to the Animator component for door animations

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F)) // If key is pressed
        {
            Interactable();
        }
    }


    public override void Interactable()
    {
        if (!hasInteracted)
        {
            // Logic to open the door
            Debug.Log("Door opened");
            hasInteracted = true;
            // Play opening animation
            doorAnimator.SetBool("DoorOpen", true);
        }
        else
        {
            // Logic to close the door
            Debug.Log("Door closed");
            hasInteracted = false;
            //Play closing animation
            doorAnimator.SetBool("DoorOpen", false);
        }
    }
}
