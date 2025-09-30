using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;

public interface IBehaviour
{
    // Define any methods or properties that should be implemented by classes inheriting from this interface
    void ButtonClicked(BaseEventData baseEventData);
}
public class UIBehaviour : MonoBehaviour, IBehaviour
{

    public Scene sceneToLoad; // Reference to the scene to load when the button is clicked
    
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

    }

    // Implement the ButtonClicked method from the IBehaviour interface
    public virtual void ButtonClicked(BaseEventData newData)
    {
        Debug.Log("Button clicked in UIBehaviour base class!");
    }
}
