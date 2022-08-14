using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEditor;

public class ObjectPool : MonoBehaviour
{
    [SerializeField] private PooledObject _prefab;
    [SerializeField] private bool _fixedSize;
    [SerializeField] private int _poolSize;
    [SerializeField] private List<PooledObject> _pooledObjectList;

    private Stack<PooledObject> _aviableObjects;
    private List<PooledObject> _activeObjects;

    private void Awake()
    {
        CreatePool();
    }

    public void CreatePool()
    {
        ClearPool();

        for(int i = 0; i < _poolSize; i++)
        {
           PooledObject pooledObject =  CreatePooledObject();
            _pooledObjectList.Add(pooledObject);
            _aviableObjects.Push(pooledObject);
        }
    }

    public void ClearPool()
    {
        for(int i = 0; i < _pooledObjectList.Count; i++)
        {
           DestroyImmediate(_pooledObjectList[i].gameObject);
        }

        _aviableObjects = new Stack<PooledObject>();
        _pooledObjectList = new List<PooledObject>();
        _activeObjects = new List<PooledObject>();
    }
    
    public T GetPooledObject<T>()
    {
        if(_pooledObjectList.Count == 0)
        {
            throw new InvalidOperationException();
        }

        int aviableCount = _aviableObjects.Count;
        PooledObject pooledObject;

        if (aviableCount == 0)
        {
            if (_fixedSize)
            {
                pooledObject = _activeObjects[0];
                _activeObjects.RemoveAt(0);
                _activeObjects.Add(pooledObject);
            }
            else
            {
                pooledObject = CreatePooledObject(true);
                _pooledObjectList.Add(pooledObject);
                _activeObjects.Add(pooledObject);
                _poolSize++;
            }
        }
        else
        {
            pooledObject = _aviableObjects.Pop();
            pooledObject.gameObject.SetActive(true); 
            _activeObjects.Add(pooledObject);
        }

        return pooledObject.GetComponent<T>();
    }

    public void ReturnPooledObject(PooledObject pooledObject)
    {
        pooledObject.gameObject.SetActive(false);
        _aviableObjects.Push(pooledObject);
        _activeObjects.Remove(pooledObject);
    }

    private PooledObject CreatePooledObject(bool isActive = false)
    {
        PooledObject pooledObject = Instantiate(_prefab, transform);
        pooledObject.Initialize(this);
        pooledObject.gameObject.name = _prefab.name;
        pooledObject.gameObject.SetActive(isActive);
        return pooledObject;
    }
}
