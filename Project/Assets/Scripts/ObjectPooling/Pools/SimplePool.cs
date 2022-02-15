using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class SimplePool : IObjectPool
{
    private GameObject[] objects;
    private int current;

    public SimplePool(int size, GameObject prefab)
    {
        objects = new GameObject[size];
        current = size - 1;

        for (int i = 0; i < size; ++i)
        {
            objects[i] = GameObject.Instantiate(prefab, Vector3.zero, Quaternion.identity);
            objects[i].SetActive(false);
        }
    }

    public GameObject Get(Vector3 where)
    {
        if(current == -1)
        {
            return null;
        }

        objects[current].SetActive(true);
        objects[current].transform.position = where;

        return objects[current--];
    }

    public void Return(GameObject gameObject)
    {
        if(current == objects.Length - 1)
        {
            return;
        }

        gameObject.SetActive(false);
        objects[++current] = gameObject;
    }
}
