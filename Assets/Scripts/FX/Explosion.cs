using System.Collections;
using UnityEngine;

public class Explosion : MonoBehaviour
{
    [SerializeField] private float _maxLiveTime = 1.5f;

    private void Start()
    {
        StartCoroutine(DestroyOnEnd());
    }

    private IEnumerator DestroyOnEnd()
    {
        yield return new WaitForSeconds(_maxLiveTime);
        Destroy(gameObject);
    }
}
