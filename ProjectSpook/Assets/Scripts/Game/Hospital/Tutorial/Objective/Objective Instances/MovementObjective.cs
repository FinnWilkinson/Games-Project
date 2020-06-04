using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementObjective : Objective
{
    private bool inArea = false;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine("CheckCompletion", 0.05f);
    }

    IEnumerator CheckCompletion(float delay)
    {
        while (!completed)
        {
            yield return new WaitForSeconds(delay);
            if (previousObjective.IsCompleted() && inArea)
            {
                completed = true;
            }
        }

    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("Player")) {
            inArea = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player") && !completed) {
            inArea = false;
        }
    }
}
