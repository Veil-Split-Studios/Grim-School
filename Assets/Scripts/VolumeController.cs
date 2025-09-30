using UnityEngine;
using UnityEngine.UI;

public interface ISoundController
{
    void SetVolume();
 
}

public abstract class VolumeController : MonoBehaviour, ISoundController
{
    
    public float currentVolume; // Current volume level

    public Slider volumeSlider; // Reference to the UI Slider component
    public AudioSource audioVolume;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public virtual void SetVolume()
    {
        
        volumeSlider.value = currentVolume;

        currentVolume = audioVolume.volume;

        // Implement the logic to set the volume here
        Debug.Log("Volume has been set.");
    }
}
