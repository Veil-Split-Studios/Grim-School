using UnityEngine;
using UnityEngine.EventSystems;

public class ToMainMenu : UIBehaviour
{
    public GameObject mainMenuUI; // Reference to the main menu UI GameObject
    public GameObject settingsUI; // Reference to the settings UI GameObject
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public override void ButtonClicked(BaseEventData newData)
    {
        settingsUI.SetActive(false); // Disable the settings UI
        mainMenuUI.SetActive(true); // Enable the main menu UI
        this.gameObject.SetActive(false); // Disable the button itself
    }
}
