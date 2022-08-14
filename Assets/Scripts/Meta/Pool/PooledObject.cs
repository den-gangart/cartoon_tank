using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PooledObject : MonoBehaviour
{
    private ObjectPool _parentPool;

    public void Initialize(ObjectPool objectPool)
    {
        _parentPool = objectPool;
    }

    public void ReturnToPool()
    {
        _parentPool.ReturnPooledObject(this);
    }
}
