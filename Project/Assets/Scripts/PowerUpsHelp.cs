using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PowerUpsHelp : MonoBehaviour
{
    public GameObject buttons;
    public Slider slider;
    public Animator animator;

    private GameObject powerUpObject;
    private Color color;

    private int time;
    private float remainingTime;

    public void SetPowerUp(int number, GameObject powerUp)
    {
        time = SavePowerUps.time[number];
        powerUpObject = powerUp;
        color = powerUpObject.GetComponent<SpriteRenderer>().color;
    }

    public void Activate()
    {
        buttons.SetActive(true);
        slider.gameObject.SetActive(true);
        StartCoroutine(Cooldown());
        powerUpObject.GetComponent<SpriteRenderer>().color = new Color(color.r, color.g, color.b, 0);
        remainingTime = time + Time.realtimeSinceStartup;

        StartCoroutine(ShowSliderData());
    }

    IEnumerator ShowSliderData()
    {
        while (remainingTime > 0)
        {
            slider.value = remainingTime - Time.realtimeSinceStartup;
            yield return null;
        }
    }

    IEnumerator Cooldown()
    {
        yield return new WaitForSeconds(time);
        buttons.SetActive(true);
        powerUpObject.GetComponent<SpriteRenderer>().color = new Color();
        animator.SetBool("Fade", true);
        yield return new WaitForSeconds(1f);
        animator.SetBool("Fade", false);
        powerUpObject.SetActive(false);
        slider.gameObject.SetActive(false);
    }
}