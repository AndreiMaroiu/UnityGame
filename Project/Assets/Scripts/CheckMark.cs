using UnityEngine;
using System.Collections;

public class CheckMark : MonoBehaviour 
{
	public PlanetObject[] skins;
    public int currentSkin;

	private void Update()
    {
        gameObject.SetActive(skins[currentSkin].avability);
	}
}
