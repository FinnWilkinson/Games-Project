using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tutorial : MonoBehaviour
{
    public List<Objective> objectives;
    public List<Door> lockedDoors;

    private bool completed = false;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine("CheckCompletion", 0.05f);
    }

    protected IEnumerator CheckCompletion(float delay)
    {
        while (!completed)
        {
            yield return new WaitForSeconds(delay);
            completed = true;
            foreach (Objective o in objectives) {
                if (!o.IsCompleted()) {
                    completed = false;
                }
            }
        }

        foreach (Door d in lockedDoors) {
            d.SetLocked(false);
            d.GetComponent<Rigidbody>().velocity = new Vector3(1.5f, 0.0f, 0.0f);
        }

    }
}
