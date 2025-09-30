using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Settings : UIBehaviour
{

    public GameObject settingsUI;// Reference to the settings UI GameObject
    public GameObject currentUI; // Reference to the current UI GameObject
    public GameObject previousUI; //Reference to the current UI GameObject
    public GameObject arrowToMainMenu; // Reference to the arrow to main menu GameObject

    public Button settingsButton; // Reference to the settings button

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        settingsButton = GetComponent<Button>();
        settingsButton.onClick.AddListener(() => ButtonClicked(new BaseEventData(EventSystem.current)));
    }

    // Update is called once per frame
    void Update()
    {

        
    }

    public override void ButtonClicked(BaseEventData newData)
    {
        Debug.Log("Button clicked in UIBehaviour base class!");
        //Open settings menu by disabling the current UI and enabling the settings UI
        if (currentUI != null)
        {
            currentUI.SetActive(false);
            settingsUI.SetActive(true);
            arrowToMainMenu.SetActive(true); // Show the arrow to main menu
            if (this.gameObject.activeInHierarchy)
            {
                currentUI = previousUI; // Set the current UI to this button's GameObject
                previousUI = currentUI;
            }
        }

        
    }
}
