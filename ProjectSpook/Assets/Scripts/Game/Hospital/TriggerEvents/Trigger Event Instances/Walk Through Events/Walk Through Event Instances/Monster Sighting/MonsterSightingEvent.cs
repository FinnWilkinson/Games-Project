using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterSightingEvent : WalkThroughEvent
{

    private DoorSwingEvent doorSwing;

    private void Start()
    {
        doorSwing = this.GetComponent<DoorSwingEvent>();
        doorSwing.enabled = false;
    }

    public override void OnTriggerEnter(Collider other)
    {
        Debug.Log("ENTERED");
        StartCoroutine(WaitUpdateSign());
    }

    private IEnumerator WaitUpdateSign()
    {
        yield return new WaitForSeconds(5.5f);
        doorSwing.enabled = true;
        gameManager.UpdateExitSign();
    }
}
