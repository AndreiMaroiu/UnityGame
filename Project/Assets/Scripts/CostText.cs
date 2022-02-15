using UnityEngine;
using UnityEngine.UI;

public class CostText : MonoBehaviour
{
    public Text text;
    public PlanetObject[] skins;

    public GameObject coinImage;

    private void Update()
    {
        text.text = skins[0].cost.ToString();

        if (skins[0].avability)
        {
            text.gameObject.SetActive(false);
            coinImage.SetActive(false);
        }
    }
}