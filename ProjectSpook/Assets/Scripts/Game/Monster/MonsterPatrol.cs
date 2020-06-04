using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class MonsterPatrol : MonoBehaviour
{
    // Variable to decide if monster pauses at a waypoint
    [SerializeField]
    bool waitOnArrival = false;

    [SerializeField]
    float waitTime = 3.0f;

    [SerializeField]
    List<Waypoint> patrolPoints = new List<Waypoint>();

    [SerializeField]
    float rotateSpeed = 30.0f;

    private NavMeshAgent monster;
    private int currentPatrolIndex;
    private bool travelling;
    private bool waiting;
    private float waitTimer;
    public bool sighted;
    private Transform sightedPosition;
    private bool flipped = false;

    public void Start()
    {
        monster = this.GetComponent<NavMeshAgent>();
        sighted = false;

        if (monster == null)
        {
            Debug.LogError("Object does not have NavMeshAgent compenent: " + gameObject.name);
        }
        else
        {
            if (patrolPoints != null && patrolPoints.Count >= 2)
            {
                currentPatrolIndex = 0;
                travelling = true;
                SetDestination(patrolPoints[currentPatrolIndex].transform);

            }
            else {
                Debug.LogError("Insufficient patrol points");
            }
        }

        StartCoroutine(Patrol());

    }

    private IEnumerator Patrol()
    {
        while (true) {
            yield return new WaitForSeconds(0.01f);
            if (!sighted && travelling && monster.remainingDistance <= 1.0f)
            {
                travelling = false;

                if (waitOnArrival)
                {
                    waiting = true;
                    waitTimer = 0f;
                }
                else if (!sighted)
                {
                    ChangePatrolPoint();
                    SetDestination(patrolPoints[currentPatrolIndex].transform);
                }
            }

            if (waiting && !sighted)
            {
                waitTimer += Time.deltaTime;
                if (waitTimer >= waitTime)
                {
                    waiting = false;
                    flipped = false;
                    ChangePatrolPoint();
                    SetDestination(patrolPoints[currentPatrolIndex].transform);
                }
                else
                {
                    if (!flipped && waitTimer >= waitTime / 2)
                    {
                        flipped = true;
                        rotateSpeed = -rotateSpeed;
                    }
                    transform.RotateAround(transform.position, Vector3.up, rotateSpeed * Time.deltaTime);
                }
            }

            if (sighted)
            {
                SetDestination(sightedPosition);
            }
        }

    }

    public void SetDestination(Transform newTarget)
    {
       
        Vector3 targetVector = newTarget.position;
        monster.SetDestination(targetVector);
        travelling = true;
        
    }

    private void ChangePatrolPoint()
    {
        currentPatrolIndex = UnityEngine.Random.Range(0, patrolPoints.Count);
    }

    public void SetSighted(bool givenSighted, Transform givenPosition)
    {
        sighted = givenSighted;
        sightedPosition = givenPosition;
    }

}
