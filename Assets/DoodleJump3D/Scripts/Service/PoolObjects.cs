using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoolObjects<T> where T : MonoBehaviour
{
    protected static List<T> _objects = new List<T>();

    public static int CountNoActiveObjects => _objects.FindAll(objectTemp => objectTemp.gameObject.activeSelf == false).Count;

    public static T GetObject(T prefabObject, Transform transformParent = null)
    {
        foreach (var currentObject in _objects)
        {
            if (currentObject != null && currentObject.gameObject != null && currentObject.gameObject.activeSelf == false)
            {
                currentObject.gameObject.SetActive(true);
                return currentObject;
            }
        }

        return AddObject(prefabObject, transformParent);
    }

    public static T GetObject(T prefabObject, Vector3 startPosition, Quaternion quaternion)
    {
        foreach (var currentObject in _objects)
        {
            if (currentObject != null && currentObject.gameObject != null && currentObject.gameObject.activeSelf == false)
            {
                currentObject.gameObject.SetActive(true);
                currentObject.gameObject.transform.position = startPosition;
                return currentObject;
            }
        }

        return AddObject(prefabObject, startPosition, quaternion);
    }

    protected static T AddObject(T prefabObject, Transform transformParent = null)
    {
        T createdObject;
        if (transformParent != null)
            createdObject = UnityEngine.Object.Instantiate(prefabObject, transformParent);
        else
            createdObject = UnityEngine.Object.Instantiate(prefabObject);

        _objects.Add(createdObject);
        return createdObject;
    }

    protected static T AddObject(T prefabObject, Vector3 position, Quaternion quaternion)
    {
        var createdObject = UnityEngine.Object.Instantiate(prefabObject, position, quaternion);
        _objects.Add(createdObject);
        return createdObject;
    }

    public static void SetActiveObjects(bool value)
    {
        foreach (var currentObject in _objects)
        {
            if (currentObject != null)
                currentObject.gameObject.SetActive(value);
        }
    }

    public static void Clear()
    {
        foreach (var currentObject in _objects)
        {
            if (currentObject != null)
                GameObject.Destroy(currentObject.gameObject);
        }

        _objects.Clear();
    }
}
