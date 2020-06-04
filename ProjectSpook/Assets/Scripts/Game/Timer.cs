using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Timer : MonoBehaviour
{
    public float targetTime = 70.0f;

    private void Start()
    {
        this.gameObject.SetActive(false);
    }

    private IEnumerator TimerFunction()
    {
        yield return new WaitForSeconds(1.0f);
        if(targetTime <= 0.0f)
        {
            CancelInvoke("TimerFunction");
        }
    }
}
