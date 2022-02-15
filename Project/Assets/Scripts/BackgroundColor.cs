using UnityEngine;
using System;

[Serializable]
public class BackgroundColor : MonoBehaviour 
{
	public Material[] backgrounds;
    public GameObject quad;

	private void Start ()
	{
        if (GameManagerScript.randomColor)
        {
            quad.GetComponent<MeshRenderer>().material = backgrounds[UnityEngine.Random.Range(0 , backgrounds.GetLength(0))];
        }
        else
        {
            quad.GetComponent<MeshRenderer>().material = backgrounds[GameManagerScript.bgColor];
        }
	}
}
