using UnityEngine;
using UnityEngine.AI;

public class NPCChase : MonoBehaviour
{
    public NavMeshAgent securityAgent; // Reference to the NavMeshAgent component for pathfinding
    

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        securityAgent = GetComponent<NavMeshAgent>(); // Get the NavMeshAgent component attached to the same GameObject
    }

    
    public void ChasePlayer(Transform Player)
    {
        //Make the NPC rotate towards the player

        Vector3 direction = Player.position - transform.position; // Calculate the direction to the player
        float angle = Mathf.Atan2(direction.z, direction.x) * Mathf.Rad2Deg; // Calculate the angle to the player in degrees

        Quaternion.Slerp(transform.rotation, Quaternion.Euler(new Vector3(0, angle, 0)), 0.15f); // Smoothly rotate towards the player

        Debug.Log("Chasing player: " + Player.name); // Log the player's name to the console for debugging
        securityAgent.SetDestination(Player.position); // Set the destination of the NavMeshAgent to the player's position


    }
}
