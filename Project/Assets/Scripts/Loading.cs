using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Loading : MonoBehaviour {

    private AsyncOperation async;

    public GameObject meteorite;

    void Start()
    {
        Screen.SetResolution(720, 1280, true, 0);
        StartCoroutine(LoadingScene());
        async = SceneManager.LoadSceneAsync(1);
    }

    IEnumerator LoadingScene()
    {
        SceneManager.LoadSceneAsync(1);
        yield return async;
    }

    void Update()
    {
        meteorite.transform.Rotate(0, 0, 1);
    }
}
