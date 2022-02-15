using UnityEngine;
using System.Collections;

public class FadeManager : MonoBehaviour 
{
    public PlanetObject skin;

    private void FixedUpdate()
    {
        gameObject.SetActive(!skin.avability);
    }
}
