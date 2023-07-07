using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(DayNightManager))]
public class DayNightEditor : Editor
{
    public override void OnInspectorGUI()
    {
        DayNightManager dayNightManager = (DayNightManager)target;


        if (DrawDefaultInspector())
        {
            if (dayNightManager.autoRefresh)
            {
                dayNightManager.SetNewSetting();

            }
        }

        if (GUILayout.Button("Refresh"))
        {
            dayNightManager.SetNewSetting();


        }
    }

}
