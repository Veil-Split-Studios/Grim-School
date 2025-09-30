using System;
using UnityEngine;

public class GuardPatrol : MonoBehaviour
{

    [Header("Referanslar")]
    [SerializeField]
    private GameObject PatrolPoints;
    [SerializeField]
    private Transform[] patrolPointsArray;
    [SerializeField]
    private int currentIndex = 0;

    void Start()
    {
        int count = PatrolPoints.transform.childCount;
        patrolPointsArray = new Transform[count];
        for (int i = 0; i < count; i++)
        {
            patrolPointsArray[i] = PatrolPoints.transform.GetChild(i);
        }
        
    }

    private bool isReversing = false;

    void Update()
    {
        if (patrolPointsArray == null || patrolPointsArray.Length == 0)
            return;

        if (transform.position != patrolPointsArray[currentIndex].position)
        {
            MoveToNextPoint();
        }
        else
        {
            if (!isReversing)
            {
                if (currentIndex < patrolPointsArray.Length - 1)
                {
                    currentIndex++;
                }
                else
                {
                    isReversing = true;
                    currentIndex--;
                }
            }
            else
            {
                if (currentIndex > 0)
                {
                    currentIndex--;
                }
                else
                {
                    isReversing = false;
                    currentIndex++;
                }
            }
        }
    }

    void MoveToNextPoint()
    {
        if (patrolPointsArray.Length == 0)
            return;
        // Move towards the next patrol point
        Vector3 targetPosition = patrolPointsArray[currentIndex].position;
        transform.position = Vector3.MoveTowards(transform.position, 
            targetPosition, 2f * Time.deltaTime);
    }
    void OnDrawGizmos()
    {
        if (patrolPointsArray == null || patrolPointsArray.Length == 0)
            return;

        Gizmos.color = Color.red;
        for (int i = 0; i < patrolPointsArray.Length; i++)
        {
            if (patrolPointsArray[i] != null)
                Gizmos.DrawLine(transform.position, patrolPointsArray[i].position);
        }
    }
}
