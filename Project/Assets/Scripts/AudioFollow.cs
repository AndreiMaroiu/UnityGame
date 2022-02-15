using UnityEngine;

public class AudioFollow : MonoBehaviour 
{
	void Awake () 
    {
        DontDestroyOnLoad(gameObject);

        if (FindObjectsOfType(GetType()).Length > 1)
        {
            Destroy(gameObject);
        }
    }
}
