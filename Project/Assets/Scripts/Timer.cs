using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Timer : MonoBehaviour
{

    public Text text;
    private float time = 3.0f;

    void Start()
    {
        StartCoroutine(WaitTime());
    }

    void Update()
    {
        time -= Time.deltaTime;
        if (time <= 3)
        {
            text.text = "" + Mathf.Round(time);
        }
        if (time < 1)
        {
            text.text = "GO!";
        }
    }

    IEnumerator WaitTime()
    {
        yield return new WaitForSeconds(2.9f);
        text.gameObject.SetActive(false);
    }

    public void TextVisibility(bool value)
    {
        if (time > 0)
        {
            text.gameObject.SetActive(value);
        }
    }
}

