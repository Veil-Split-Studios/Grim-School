using UnityEngine;
using UnityEngine.AI;

public class NPCInvestigate : MonoBehaviour
{
    public NavMeshAgent securityAgent; // Reference to the NavMeshAgent component for pathfinding

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        securityAgent = GetComponent<NavMeshAgent>(); // Get the NavMeshAgent component attached to the same GameObject
    }

    public void Investiget(Vector3 investigationPosition)
    {
        Vector3 direction = investigationPosition - transform.position; // Calculate the direction to the investigation position
        float angle = Mathf.Atan2(direction.z, direction.x) * Mathf.Rad2Deg; // Calculate the angle to the investigation position in degrees
        transform.rotation = Quaternion.Euler(new Vector3(0, angle, 0)); // Rotate the NPC to face the investigation position

        securityAgent.SetDestination(investigationPosition); // Set the destination of the NavMeshAgent to the investigation position
    }
}
