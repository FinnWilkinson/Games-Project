using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockWallEvent : TriggerEvent
{
    public GameObject wall;

    private void Start()
    {
        wall.SetActive(false);
    }


    public override void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("Player"))
        {
            wall.SetActive(true);
        }
    }
}
