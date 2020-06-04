using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CurtainLoader : TriggerEvent
{
    public bool loadOnStart;
    public List<GameObject> railings;

    private void Start()
    {
        foreach (GameObject railing in railings)
        {
            railing.SetActive(loadOnStart);
        }
    }

    public override void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("Player"))
        {
            foreach (GameObject railing in railings)
            {
                railing.SetActive(true);
            }
        }
    }

    public override void OnTriggerExit(Collider other)
    {
        if(other.gameObject.CompareTag("Player"))
        {
            foreach (GameObject railing in railings)
            {
                railing.SetActive(false);
            }
        }
    }
}
