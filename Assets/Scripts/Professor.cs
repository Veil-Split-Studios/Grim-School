using System.Text;
using TMPro;
using UnityEngine;

public class Professor : MonoBehaviour
{
    [SerializeField]
    private MissionData _taskData;

    [SerializeField]
    private GameObject _missionPanel;
    [SerializeField]
    private TextMeshProUGUI _objective;


    public bool hasAcceptedMission;
    public bool missionInProgreess;
    public bool hasCompletedMission;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
        _objective = _missionPanel.GetComponentInChildren<TextMeshProUGUI>();
    }

    // Update is called once per frame
    void Update()
    {
        if (hasAcceptedMission)
        {
            missionStatus();
        }
    }

    public void missionStatus()
    {
        if (missionInProgreess)
        {
            //Activate UI & change UI text
            _missionPanel.gameObject.SetActive(true);
            _objective.text = _taskData.professorMission;

        }
    }
}
