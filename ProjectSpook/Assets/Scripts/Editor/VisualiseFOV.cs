using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor (typeof(FieldOfView))]
public class VisualiseFOV : Editor
{
    private void OnSceneGUI()
    {
        // Visualise a circle in the editor with radius = FOV radius
        FieldOfView fov = (FieldOfView) target;
        Handles.color = Color.white;
        Vector3 headheight = new Vector3(fov.transform.position.x, fov.viewingHeight * (fov.transform.position.y + fov.transform.lossyScale.y), fov.transform.position.z);
        Handles.DrawWireArc(headheight, Vector3.up, Vector3.forward, 360, fov.viewRadius);

        // Visualsie a section of the circle to represent the viewing angle
        Vector3 viewAngleA = fov.DirectionFromAngle(-fov.viewAngle / 2, false);
        Vector3 viewAngleB = fov.DirectionFromAngle(fov.viewAngle / 2, false);

        Handles.DrawLine(headheight, headheight + viewAngleA * fov.viewRadius);
        Handles.DrawLine(headheight, headheight + viewAngleB * fov.viewRadius);

        Handles.color = Color.red;
        foreach (Transform visibleTarget in fov.visibleTargets) {
            Handles.DrawLine(headheight, visibleTarget.position);
        }
    }
}
