using UnityEngine;

public class MusicVolume : VolumeController
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        volumeSlider.onValueChanged.AddListener(delegate { SetVolume(); });
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public override void SetVolume()
    {

        audioVolume.volume = volumeSlider.value;

        // Implement the logic to set the volume here
        Debug.Log("Music Volume has been set.");
    }
}
