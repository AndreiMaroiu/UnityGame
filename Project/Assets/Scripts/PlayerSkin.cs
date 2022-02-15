using UnityEngine;
using System.Collections;

public class PlayerSkin : MonoBehaviour {

    public PlanetObject[] skins;
    public int currentSkin;

    void Update()
    {
        GetComponent<SpriteRenderer>().sprite = skins[currentSkin].skin;

        if (skins[currentSkin].rotating == true)
        {
            transform.Rotate(0, 0, 1);
        }
        if (skins[currentSkin].rotating == false)
        {
            transform.rotation = new Quaternion();
        }
    }

    void FixedUpdate()
    {
        currentSkin = PlayerPrefs.GetInt("CurrnetSkin");
    }
}
