using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadGameMode : MonoBehaviour {

	public void LoadScene()
    {
        SceneManager.LoadScene(PlayerPrefs.GetInt("CurrentScene"));
    }
}
