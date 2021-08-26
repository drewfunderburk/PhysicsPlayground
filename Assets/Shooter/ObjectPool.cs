using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Pre-Instantiated list of objects.
/// 
/// All ObjectPools must be manually initialized using ObjectPool.Initialize() if not created with the constructor before being used!
/// </summary>
[System.Serializable]
public class ObjectPool
{
    [Tooltip("Object to pool")]
    [SerializeField] private GameObject _object;

    [Tooltip("Size of the pool")]
    [SerializeField] [Min(0)] private int _size = 0;

    private Queue<GameObject> _objectQueue = new Queue<GameObject>();
    private bool _initialized = false;

    /// <summary>
    /// Queue of objects stored in this pool
    /// </summary>
    public Queue<GameObject> ObjectQueue { get => _objectQueue; private set => _objectQueue = value; }

    /// <summary>
    /// Create a new ObjectPool with the given size and object
    /// </summary>
    /// <param name="obj">Object to pool</param>
    /// <param name="size">Size of pool</param>
    public ObjectPool(GameObject obj, int size)
    {
        // Ensure obj is valid
        if (obj)
            _object = obj;
        else
        {
            Debug.LogWarning("Object was null!");
            _object = new GameObject();
        }

        // Ensure _size is not negative
        _size = Mathf.Max(0, size);

        Initialize();
    }

    public GameObject GetNext()
    {
        if (!_initialized)
        {
            Debug.LogWarning("ObjectPool not Initialized! If this pool was not created with a constructor, use ObjectPool.Initialize() before using the pool!");
            return null;
        }

        // If nothing in queue, return null
        if (ObjectQueue.Count == 0)
            return null;

        // Put obj in the back of the queue
        GameObject obj = ObjectQueue.Dequeue();
        ObjectQueue.Enqueue(obj);

        // Return obj
        if (!obj.activeInHierarchy)
            obj.SetActive(true);

        return obj;
    }

    /// <summary>
    /// Destroy all objects in the queue and clear it
    /// </summary>
    public void Clear()
    {
        if (!_initialized || ObjectQueue.Count == 0)
            return;

        foreach(GameObject obj in ObjectQueue)
            Object.Destroy(obj);

        ObjectQueue.Clear();
    }

    /// <summary>
    /// Clears queue, then Initialize the queue with objects
    /// </summary>
    public void Initialize()
    {
        // Ensure the queue is not null
        if (ObjectQueue == null)
            ObjectQueue = new Queue<GameObject>();

        for (int i = 0; i < _size; i++)
        {
            GameObject obj = Object.Instantiate(_object);
            obj.SetActive(false);
            ObjectQueue.Enqueue(obj);
        }

        _initialized = true;
    }
}
