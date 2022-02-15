using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Renderer))]
public class ScrollingBackground : MonoBehaviour 
{
    [SerializeField] private float speed;
    [SerializeField] private float maxSpeed;
    [SerializeField] private float acceleration;

    private Renderer rendererComponent;

	void Start () 
    {
        rendererComponent = GetComponent<Renderer>();
	}

	void Update () 
    {
	    Vector2 offset  = new Vector2(0, Time.time  * speed);
        rendererComponent.material.mainTextureOffset = offset;

        if (speed < maxSpeed)
        {
            speed += acceleration;
        }
	}
}
