using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseCapture : MonoBehaviour
{
    [SerializeField] private float _captureLength;
    [SerializeField] private float _stepCount;
    [SerializeField] private float _captureSpeed;

    private float _stepLength;
    private float _captureAmount = 0;
    private Collider _invaderCollider;
    private IEnumerator _captureCoroutine;
    private bool _isCompleted = false;

    public event EventHandler<float> Capture;
    public event Action CaptureCompleed;
    public event Action CaptureFailed;

    void Start()
    {
        _stepLength = _captureLength / _stepCount;
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == GameTags.Player && !_isCompleted)
        {
            _invaderCollider = other;
            _captureCoroutine = Capturing();
            StartCoroutine(_captureCoroutine);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other == _invaderCollider && !_isCompleted)
        {
            CaptureFailed?.Invoke();
            _captureAmount = 0;
            StopCoroutine(_captureCoroutine);
        }
    }

    private IEnumerator Capturing()
    {
        for(int i = 0; i < _stepCount; i++)
        {
            _captureAmount += _stepLength;
            Capture?.Invoke(this, _captureAmount);
            yield return new WaitForSeconds(_captureSpeed);
        }

        _isCompleted = true;
        CaptureCompleed?.Invoke();
    }
}

