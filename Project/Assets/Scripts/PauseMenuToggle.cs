using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class PauseMenuToggle : MonoBehaviour {

    public GameObject pauseMenu;
    public GameObject pauseButton;

    public Text text;
    public GameObject ButtonsPanel;
    public GameObject timer;

    private float cooldown = 3;
    private bool cooldownAvailability = false;


    void Start () {
        pauseMenu.SetActive(false);
        text.gameObject.SetActive(false);
        StartCoroutine(PauseCooldownAvailability());
	}

    public void Toggle(bool activity)
    {
        pauseMenu.SetActive(activity);
    }

    public void TimeControl(int timeScale)
    {
        Time.timeScale = timeScale;
    }
	
	public void pauseButtonToggle(bool trueOrFalse)
    {
        pauseButton.SetActive(trueOrFalse);
        Effect.Iterations = 10;
        Effect.DownRes = 2;
    }

    public void pauseMenuCooldown()
    {
        Effect.Iterations = 0;
        Effect.DownRes = 0;
        if (cooldownAvailability == true)
        {
            Time.timeScale = 0;
            StartCoroutine(Pause());
            text.text = "Ready?";
            ButtonsPanel.SetActive(false);
        }
        else
        {
            Time.timeScale = 1;
        }
    }

    private IEnumerator Pause()
    {
        pauseButton.SetActive(false);
        text.gameObject.SetActive(true);
        yield return new WaitForSecondsRealtime(1.5f);
        text.text = "Go!";
        yield return new WaitForSecondsRealtime(0.5f);
        Debug.Log(cooldown);
        text.gameObject.SetActive(false);
        ButtonsPanel.SetActive(true);
        pauseButton.SetActive(true);
        Time.timeScale = 1;
        yield return new WaitForSeconds(2);
    }

    IEnumerator PauseCooldownAvailability()
    {
        yield return new WaitForSeconds(3.0f);
        cooldownAvailability = true;
    }
}
