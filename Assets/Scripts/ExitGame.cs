using UnityEngine;

public class ExitGame : UIBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public override void ButtonClicked(UnityEngine.EventSystems.BaseEventData newData)
    {
        Debug.Log("Exit button clicked in UIBehaviour base class!");
        // Exit the game
        Application.Quit();

        // If running in the editor, stop playing the scene
    }
}
