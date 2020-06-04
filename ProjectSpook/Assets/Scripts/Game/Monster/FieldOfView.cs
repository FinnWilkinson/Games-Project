using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FieldOfView : MonoBehaviour
{
    public float viewRadius;
    [Range(0, 360)]
    public float viewAngle;
    [Range(0, 1)]
    public float viewingHeight;

    // Layer masks for what is classed as a target (e.g. the player) or an obstacle (e.g. Default layered walls)
    public LayerMask targetMask;
    public LayerMask obstacleMask;
    private MonsterPatrol move;

    // No logic implemented for visible targets yet, just adds to list
    public List<Transform> visibleTargets = new List<Transform>();

    private void Start()
    {
        move = this.GetComponent<MonsterPatrol>();
        // Start a co-routine which checks for visible targets every X seconds
        StartCoroutine("FindTargetsWithDelay", 0.05f);
    }

    IEnumerator FindTargetsWithDelay(float delay)
    {
        while (true) {
            yield return new WaitForSeconds(delay);
            FindVisibleTargets();
        }
    }

    void FindVisibleTargets()
    {
        // Clear list to remove duplicates
        visibleTargets.Clear();

        // List of target colliders within view of the sphere around the monster
        Collider[] targetsInViewRadius = Physics.OverlapSphere(transform.position, viewRadius, targetMask);

        if (targetsInViewRadius.Length == 0)
        {
            move.SetSighted(false, null);
        }
        else {
            for (int i = 0; i < targetsInViewRadius.Length; i++)
            {
                Transform target = targetsInViewRadius[i].transform;
                Vector3 dirToTarget = (target.position - transform.position).normalized;

                // Check to see if the angle to the target is within the viewing angle
                if (Vector3.Angle(transform.forward, dirToTarget) < viewAngle / 2)
                {
                    float distToTarget = Vector3.Distance(transform.position, target.position);

                    // If obstacles aren't obsuring the view, add to list of visible targets
                    Vector3 headheight = new Vector3(this.transform.position.x, viewingHeight * (this.transform.position.y + this.transform.lossyScale.y), this.transform.position.z);
                    if (!Physics.Raycast(headheight, dirToTarget, distToTarget, obstacleMask))
                    {
                        visibleTargets.Add(target);
                        move.SetSighted(true, target);
                    }
                    else
                    {
                        move.SetSighted(false, null);
                    }
                }
            }
        }
        

    }

    public Vector3 DirectionFromAngle(float angle, bool globalAngle)
    {
        // Want to track if the given angle is global
        // (This is so the viewing angle rotates WITH the monster and isn't fixed to a global orientation)
        if (!globalAngle) {
            angle += transform.eulerAngles.y;
        }
        return new Vector3(Mathf.Sin(angle * Mathf.Deg2Rad), 0, Mathf.Cos(angle * Mathf.Deg2Rad));
    }


}
