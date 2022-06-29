using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LineSight : MonoBehaviour
{
    public event EventHandler<Transform> DetectPlayer;

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag != GameTags.Player)
        {
            return;
        }

        Vector3 relativePosition = other.transform.position - transform.position;
        if (Physics.Raycast(transform.position, relativePosition, Mathf.Infinity))
        {
            DetectPlayer?.Invoke(this, other.transform);
        }
    }
}
