using Mono.Cecil;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class StartGame : UIBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public override void ButtonClicked(BaseEventData startButtonData)
    {
        Debug.Log("Start Game button clicked!");
        
        SceneManager.LoadScene("Playground");
    }
}
