using UnityEngine;

public class Crosshair : MonoBehaviour
{
    [SerializeField] private Transform _crosshair;
    [SerializeField] private float _scaleOnDistance = 150;
    [SerializeField] private float _scaleSpeed = 10;
    [SerializeField] private float _moveSpeed = 100;
    [SerializeField] private float _maxDistance = 2000f;

    private void FixedUpdate()
    {
        RaycastHit hit;
        Vector3 targetPoint;
        float distance;

        if (Physics.Raycast(transform.position, transform.forward, out hit, Mathf.Infinity))
        {
            targetPoint = hit.point - transform.forward;
            distance = hit.distance;
        }
        else
        {
            targetPoint = transform.forward * _maxDistance;
            distance = _maxDistance;
        }

        UpdatePosition(targetPoint);
        UpdateScale(distance);
    }

    private void UpdatePosition(Vector3 targetPoint)
    {
        _crosshair.transform.position = Vector3.Lerp(_crosshair.transform.position, targetPoint, _moveSpeed * Time.deltaTime);
    }

    private void UpdateScale(float distance)
    {
        Vector3 previousScale = _crosshair.transform.localScale;
        Vector3 targetScale = Vector3.one * distance / _scaleOnDistance;
        _crosshair.transform.localScale = Vector3.Lerp(previousScale, targetScale, _scaleSpeed * Time.deltaTime);
    }
}
