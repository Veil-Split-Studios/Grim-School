using UnityEngine;

public class Day_NightCycle : MonoBehaviour
{
    public enum TimeOfDay
    {
        Morning,
        Afternoon,
        Evening,
        Night
    }

    //Time of day will decide if the security guards will have their flashlights on or off
    public TimeOfDay currentTimeOfDay;

    [SerializeField]
    private Light sun;

    //Time of day&hour will decide if the security guards will have their flashlights on or off
    [SerializeField]
    private float hour;
    private float minutes;
    private float seconds;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        hour = 6; // Start at 6 AM 
    }

    // Update is called once per frame
    void Update()
    {
        SunRotation(sun);  
    }


    public void SunRotation(Light Light)
    {
        float timeScale = 60f; // Speed of time progression (1 real second = 1 in-game minute)
        seconds += Time.deltaTime * timeScale;
        if (seconds >= 60)
        {
            seconds = 0;
            minutes++;
            if (minutes >= 15)
            {
                minutes = 0;
                hour++;
                if (hour >= 24)
                {
                    hour = 0;
                }
                UpdateTimeOfDay(hour);

            }
        }

        Light.transform.Rotate(new Vector3(1f, 0, 0), 1 * Time.deltaTime);

        
    }

    public void UpdateTimeOfDay(float hour)
    {
        if (hour >= 6 && hour < 12)
        {
            currentTimeOfDay = TimeOfDay.Morning;
        }
        else if (hour >= 12 && hour < 18)
        {
            currentTimeOfDay = TimeOfDay.Afternoon;
        }
        else if (hour >= 18 && hour < 21)
        {
            currentTimeOfDay = TimeOfDay.Evening;
        }
        else if (hour >= 0 && hour < 6)
        {
            currentTimeOfDay = TimeOfDay.Night;
        }
    }

    









}
