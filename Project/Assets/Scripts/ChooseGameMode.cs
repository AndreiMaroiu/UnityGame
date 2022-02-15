using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ChooseGameMode : MonoBehaviour 
{
    public GameObject panel;
    public GameObject main;

    public void OpenPanel(bool value)
    {
        panel.SetActive(value);
        main.SetActive(!value);
    }

    public void ChooseMode(int scene)
    {
        SceneManager.LoadScene(scene, LoadSceneMode.Single);
    }
}
