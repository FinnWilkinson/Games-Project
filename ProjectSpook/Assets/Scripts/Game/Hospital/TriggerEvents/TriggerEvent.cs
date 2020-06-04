using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;

public class TriggerEvent : MonoBehaviour
{
    public bool updateSign;
    protected ExitSignManager gameManager;

    public virtual void Awake()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<ExitSignManager>();
    }

    public virtual void OnTriggerEnter(Collider other)
    {
        if (updateSign) {
            gameManager.UpdateExitSign();
        }
    }

    public virtual void OnTriggerExit(Collider other)
    {
        
    }
}
