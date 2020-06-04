using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RollingTray : Draggable
{
    protected override void RepositionLogical()
    {
        base.RepositionLogical();
        ApplyHandVelocity(attachedHand, objectBody, 0.3f);
    }
}
