using UnityEngine;


public interface Interactable
{
    void Interactable();
}

public abstract class Interact : MonoBehaviour, Interactable
{
    public bool hasInteracted = false;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public virtual void Interactable()
    {
        //Add interaction  logic here
    }

}
