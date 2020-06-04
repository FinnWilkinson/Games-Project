using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Objective : MonoBehaviour
{
    public Objective previousObjective = null;
    protected bool completed;

    public bool IsCompleted()
    {
        return completed;
    }

}
