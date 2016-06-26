using UnityEngine;
using System.Collections;
using UnityEditor;

[CustomEditor (typeof (fieldOfView))]
public class FieldOfViewEditor : Editor {

	void OnSceneGUI()
    {
        fieldOfView fow = (fieldOfView)target;
        Handles.color = Color.white;
        Handles.DrawWireArc(fow.transform.position, Vector3.up, Vector3.forward, 360, fow.ViewRadius);
        Vector3 viewAngleA = fow.directionFromAngle(-fow.viewAngle / 2, false);
        Vector3 viewAngleB = fow.directionFromAngle(fow.viewAngle / 2, false);

        Handles.DrawLine(fow.transform.position, fow.transform.position + viewAngleA * fow.ViewRadius);
        Handles.DrawLine(fow.transform.position, fow.transform.position + viewAngleB * fow.ViewRadius);

        Handles.color = Color.red;
        foreach (Transform visibleTarget in fow.visibleTargets)
        {
            Handles.DrawLine(fow.transform.position, visibleTarget.position);
        }
    }
}
