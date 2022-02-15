using UnityEngine;

public interface IObjectPool
{
    GameObject Get(Vector3 where);

    void Return(GameObject gameObject);
}
