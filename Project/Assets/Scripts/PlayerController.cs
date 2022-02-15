using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using UnityEngine.Advertisements;
using UnityEngine.UI;
using Random = UnityEngine.Random;

[System.Serializable]
public class PlayerController : MonoBehaviour
{
    public static float moveV;
    public  float speed;
    public float max;
    public float accelerate;
   
    public PlanetObject[] skins;
    public int currentSkin;

    public GameObject rotatingMeteorite;

    public AudioClip clip;
    private AudioSource source;

    public GameObject revivePanel;
    private float revivePanelAvailability;
    private int minR = 0;
    private int maxR = 10;
    public GameObject pauseButton;
    public int cost;
    public Text text;
    public GameObject moveButtons;
    private bool deathAvailability;
    private bool remainTimeAvailability;
    public AudioClip deathClip;
    private GameObject enemyPlanet;
    private GameObject enemyMeteorite;

    public GameObject mainCamera;
    private bool repeat;
    public static bool cameraRepeat = true;
    private float randomX, randomY, randomTime;

    public GameObject failedAdPanel;
    public GameObject button;
    public GameObject button2;
    public Text messageText;

    private float cooldown;
    public static bool reviveCooldownFroScore;
    public static bool enemyMeteoriteControl;

    public Slider slider;
    private float remainTime;
    public Text sliderText;
    private Rigidbody2D playerRigidbody;

    public IEnumerator WaitForStart()
    {
        deathAvailability = false;
        reviveCooldownFroScore = true;
        enemyMeteoriteControl = true;
        speed = 0;
        accelerate = 0;
        yield return new WaitForSeconds(3f);
        speed = 0.1f;
        accelerate = 0.000005f;
        deathAvailability = true;
        reviveCooldownFroScore = false;
    }

    private void Awake()
    {
        source = GetComponent<AudioSource>();
    }

    private void Start()
    {
        playerRigidbody = GetComponent<Rigidbody2D>();
        moveV = 0;
        Blur(false);
        
        if (GameManagerScript.randomSkin)
        {
            currentSkin = Random.Range(0, skins.Length);
        }
        else
        {
            currentSkin = GameManagerScript.currentSkinNumber;
        }
        
        rotatingMeteorite.GetComponent<SpriteRenderer>().sprite = skins[currentSkin].skin;
        StartCoroutine(WaitForStart());
        revivePanel.SetActive(false);
        revivePanelAvailability = Random.Range(minR,maxR);
        
        Debug.Log(revivePanelAvailability.ToString());
    }

    public void MultiplySpeed(float scalar)
    {
        speed *= scalar;
    }

    private void FixedUpdate()
    {
        Move(moveV);
        
        if (skins[currentSkin].rotating)
        {
            rotatingMeteorite.transform.Rotate(0, 0, 1);
        }
        
        if (speed < max)
        {
            speed += accelerate;
        }
        
        if ((cooldown - Time.realtimeSinceStartup) > 0.5f)
        {
            text.text = Mathf.RoundToInt(cooldown - Time.realtimeSinceStartup).ToString();
        }
        else
        {
            text.text = "GO!";
        }

        if (repeat)
        {
            StartCoroutine(Shake());
            mainCamera.transform.Translate(new Vector3(Random.Range(-0.1f, 0.1f), Random.Range(-0.1f, 0.1f), 0));
        }
    }

    private void Update()
    {
        sliderText.text = Mathf.RoundToInt(remainTime - Time.realtimeSinceStartup).ToString();
        slider.value = remainTime - Time.realtimeSinceStartup;
    }

    private IEnumerator Shake()
    {
        while (repeat)
        {
            randomTime = Random.Range(0.02f, 0.04f);
            randomX = Random.Range(-0.075f, 0.075f);
            randomY = Random.Range(-0.075f, 0.075f);
            mainCamera.transform.Translate(new Vector2(randomX, randomY));
            yield return new WaitForSecondsRealtime(randomTime);
            mainCamera.transform.Translate(new Vector2(-randomX, -randomY));
        }
    }

    public void Move(float moveHorizontal)
    {
        transform.Translate(moveHorizontal, speed, 0);
        moveV = moveHorizontal;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("EnemyPlanet") || other.CompareTag("Destroyer") || other.CompareTag("Meteorite"))
        {
            try
            {
                Handheld.Vibrate();
            }
            catch {} //nu face nimic daca nu vibreaza

            if (revivePanelAvailability >= 5)
            {
                StartCoroutine(ShakeTime());
            }
            else if (revivePanelAvailability < 5 && deathAvailability)
            {
                repeat = true; //Fa ceva cu valorile
                StartCoroutine(Shake());
                cameraRepeat = false;
                moveButtons.SetActive(false);
                pauseButton.SetActive(false);
                Time.timeScale = 0;
                source.PlayOneShot(deathClip);
                moveV = 0;
                GetComponent<Rigidbody2D>().Sleep();
                StartCoroutine(RevivePanelCooldown());
                enemyPlanet = GameObject.FindWithTag("EnemyPlanet");
                enemyMeteorite = GameObject.FindGameObjectWithTag("Meteorite");
                enemyMeteoriteControl = false;
            }
            else
            {
                StartCoroutine(ShakeTime());
            }
            GameManagerScript.Save();
        }

        if(other.CompareTag("Coin"))
        {
            float random = Random.Range(0.5F, 1F);
            source.PlayOneShot(clip, random);
        }
    }

    private IEnumerator ShakeTime()
    {
        Time.timeScale = 0;
        repeat = true; //Fa ceva cu valorile
        StartCoroutine(Shake());
        cameraRepeat = false;
        yield return new WaitForSecondsRealtime(0.2f);

        Blur(true);

        SceneManager.LoadScene(4);
        PlayerPrefs.SetInt("CurrentScene", SceneManager.GetActiveScene().buildIndex);
        PlayerPrefs.SetInt("ShowedAd", 0);
        Time.timeScale = 1;
        cameraRepeat = true;
    }

    private void RevivePanel()
    {
        Blur(true);
        revivePanel.SetActive(true);
        remainTime = Time.realtimeSinceStartup + 5f;
        slider.gameObject.SetActive(true);
        StartCoroutine(RemainTime());
        remainTimeAvailability = true;
        PlayerPrefs.SetInt("ShowedAd", 1); 
    }

    private IEnumerator RevivePanelCooldown()
    {
        yield return new WaitForSecondsRealtime(0.2f);
        repeat = false;
        cameraRepeat = true;
        yield return new WaitForSecondsRealtime(0.3f);
        RevivePanel();
    }

    public void ShowRewardedAd()
    {
        if (Advertisement.IsReady("rewardedVideo"))
        {
            var options = new ShowOptions { resultCallback = HandleShowResult };
            Advertisement.Show("rewardedVideo", options);
        }
        else
        {
            if (InternetHelper.IsInternetOpen)
            {
                failedAdPanel.SetActive(true);
                button.SetActive(false);
                button2.SetActive(false);
                messageText.text = "Sorry! No more ads for now, try again later.";
            }
            else
            {
                failedAdPanel.SetActive(true);
                button.SetActive(false);
                button2.SetActive(false);
                messageText.text = "Failed to show the ad. Check your internet connection and try again later.";
            }
        }
    }

    private void HandleShowResult(ShowResult result)
    {
        switch (result)
        {
            case ShowResult.Finished:
                Debug.Log("The ad was successfully shown.");
                Blur(false);
                transform.position = new Vector2(0, transform.position.y - 4);
                Time.timeScale = 1;
                revivePanel.SetActive(false);
                pauseButton.SetActive(true);
                text.gameObject.SetActive(true);
                StartCoroutine(ReviveCooldown());
                revivePanelAvailability = 10;
                cooldown = Time.realtimeSinceStartup + 3f;
                reviveCooldownFroScore = true;
                moveButtons.SetActive(true);
                StopCoroutine(RemainTime());
                remainTimeAvailability = false;
                enemyMeteoriteControl = true;
                break;
            case ShowResult.Skipped:
                Debug.Log("The ad was skipped before reaching the end.");
                failedAdPanel.SetActive(true);
                button.SetActive(false);
                button2.SetActive(false);
                messageText.text = "Sorry nut the ad was skipped.";
                break;
            case ShowResult.Failed:
                Debug.LogError("The ad failed to be shown.");
                failedAdPanel.SetActive(true);
                button.SetActive(false);
                button2.SetActive(false);
                messageText.text = "Sorry but the ad failed to be shown, try again later.";
                break;
        }
    }

    public void Revive()//Aici umbli daca vrei sa faci ceva fix cand s-a inchis panoul de revive si a un pic mai sus umbli
    {
        if (GameManagerScript.playerMoney >= 500)
        {
            GameManagerScript.playerMoney -= cost;
            Blur(false);
            revivePanel.SetActive(false);
            pauseButton.SetActive(true);
            text.gameObject.SetActive(true);
            Time.timeScale = 1;
            StartCoroutine(ReviveCooldown());
            revivePanelAvailability = 10;
            cooldown = Time.realtimeSinceStartup + 3f;
            reviveCooldownFroScore = true;
            moveButtons.SetActive(true);
            StopCoroutine(RemainTime());
            remainTimeAvailability = false;
            transform.position = new Vector2(0, transform.position.y - 4);
            enemyMeteoriteControl = true;
            GameManagerScript.Save();
        }
        else
        {
            failedAdPanel.SetActive(true);
            messageText.text = "You don't have enough money";
            button.SetActive(false);
            button2.SetActive(false);
        }
    }

    private IEnumerator ReviveCooldown()
    {
        moveButtons.SetActive(true);
        playerRigidbody.IsAwake();
        StopCoroutine(RemainTime());
        
        if (!ReferenceEquals(enemyPlanet, null))
        {
            if((transform.position.y - enemyPlanet.transform.position.y) >= -2.5)
            {
                enemyPlanet.SetActive(false);
                // Debug.Log("S-a distrus o planeta");
            }
        }
        
        if(!ReferenceEquals(enemyMeteorite, null))
        {
            if ((transform.position.y - enemyMeteorite.transform.position.y) >= -2.5)
            {
                enemyMeteorite.SetActive(false);
                // Debug.Log("S-a dstrus un meteorit");
            }      
        }
        
        reviveCooldownFroScore = true;
        speed = 0;
        accelerate = 0;
        yield return new WaitForSeconds(3f);
        speed = 0.1f;
        accelerate = 0.000005f;
        text.gameObject.SetActive(false);
        reviveCooldownFroScore = false;
    }

    public void ClosedPanel()
    {
        failedAdPanel.SetActive(false);
        button.SetActive(true);
        button2.SetActive(true);
    }

    private IEnumerator RemainTime()
    {
        yield return new WaitForSecondsRealtime(5.0f);
        
        if (remainTimeAvailability)
        {
            Time.timeScale = 1;
            SceneManager.LoadScene(4);
        }   
    }

    public void Blur(bool yes)
    {
        if (yes)
        {
            Effect.Iterations = 10;
            Effect.DownRes = 2;
        }
        else
        {
            Effect.Iterations = 0;
            Effect.DownRes = 0;
        }
    }
}