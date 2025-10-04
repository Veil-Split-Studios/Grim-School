using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;
public class PlayerGetMission : MonoBehaviour
{

    [SerializeField]
    private Professor _getMission;

    [SerializeField]
    private Image acceptanceRadial;

    [SerializeField]
    private float rayLength;
    [SerializeField]
    private float radialValue;

    [SerializeField]
    private bool hasAcceptedMission;
    [SerializeField]
    private bool hasCompletedMission;
    

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.F))
        {
            GetMission();
        }
        else
        {
            ResetAcceptMissionRadial();
        }
    }


    private void AcceptMissionRadial()
    {
        acceptanceRadial.fillAmount = radialValue;

        if (radialValue >= 1f)
        {
            radialValue = 1f;
            _getMission.hasAcceptedMission = true;
            _getMission.missionInProgreess = true;
            return;
        }

        if (radialValue <= 1f)
        {
            radialValue += 0.01f;
        }

        

    }

    private void ResetAcceptMissionRadial()
    {
        radialValue = acceptanceRadial.fillAmount;
        radialValue = 0f;
        acceptanceRadial.fillAmount = radialValue;
    }

    public void GetMission()
    {
        
        Debug.DrawRay(transform.position, transform.forward, Color.red, rayLength);
        RaycastHit hit;
        if (Physics.Raycast(transform.position, transform.forward, out hit, rayLength))
        {
            _getMission = hit.transform.GetComponent<Professor>();
            if (_getMission == null) return;

            Debug.Log(hit.transform.gameObject);

            if (!_getMission.hasAcceptedMission)
            {
                acceptanceRadial.gameObject.SetActive(true);
                AcceptMissionRadial();
            }
            else if (_getMission.hasAcceptedMission)
            {
                acceptanceRadial.gameObject.SetActive(false);
                ResetAcceptMissionRadial();
            }
            else
            {
                ResetAcceptMissionRadial();
            }

        }
        else
        {
            ResetAcceptMissionRadial();
        }
    }

    public void MissionCompleted()
    {
        if (!_getMission.hasAcceptedMission && !_getMission.hasCompletedMission)
        {
            return;
        }

        if (_getMission.hasCompletedMission)
        {
            //Add influence point to the character equivalent to the reward given from the mission giver.

        }


    }


    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawRay(transform.position, transform.forward * rayLength);
    }
}
