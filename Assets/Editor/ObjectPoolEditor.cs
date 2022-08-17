using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(ObjectPool))]
public class ObjectPoolEditor : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        ObjectPool objectPool = (ObjectPool)target;

        if(GUILayout.Button("Create"))
        {
            objectPool.CreatePool();
        }

        if(GUILayout.Button("Clear"))
        {
            objectPool.ClearPool();
        }
    }
}
