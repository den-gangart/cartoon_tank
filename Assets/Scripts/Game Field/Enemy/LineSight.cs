using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LineSight : MonoBehaviour
{
    [SerializeField] private Transform relativeTransform;
    public event EventHandler<Transform> DetectPlayer;
    public event EventHandler<Transform> LostPlayer;

    private bool _isPlayerVisible = false;

    private void OnTriggerStay(Collider other)
    {
        if (other.tag != GameTags.Player)
        {
            return;
        }

        CheckPlayerVisible(other);
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag != GameTags.Player || !_isPlayerVisible)
        {
            return;
        }

        OnLostPlayer(other.transform);
    }

    private void CheckPlayerVisible(Collider playerCollider)
    {
        RaycastHit hit;
        Vector3 relativePosition = playerCollider.transform.position - relativeTransform.position;
        bool hasObjectsOnWay = Physics.Raycast(relativeTransform.position, relativePosition, out hit);
        bool isPlayerOnWay = hasObjectsOnWay && hit.transform.tag == GameTags.Player;

        Debug.DrawRay(relativeTransform.position, relativePosition);

        if (isPlayerOnWay && !_isPlayerVisible)
        {
            OnDetectPlayer(playerCollider.transform);
        }
        else if(!isPlayerOnWay && _isPlayerVisible)
        {
            OnLostPlayer(playerCollider.transform);
        }
    }

    private void OnDetectPlayer(Transform player)
    {
        DetectPlayer?.Invoke(this, player);
        _isPlayerVisible = true;
    }

    private void OnLostPlayer(Transform player)
    {
        LostPlayer?.Invoke(this, player.transform);
        _isPlayerVisible = false;
    }
}
