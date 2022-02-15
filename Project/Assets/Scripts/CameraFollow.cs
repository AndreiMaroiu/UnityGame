using UnityEngine;
using System.Collections;

public class CameraFollow : MonoBehaviour 
{
    [SerializeField] private Transform player;

    private void Update ()
    {
        if (PlayerController.cameraRepeat)
        {
            transform.position = new Vector3(0, player.position.y + 3, -10);
        }
    }
}

    
