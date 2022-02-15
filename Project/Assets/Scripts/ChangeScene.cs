using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class ChangeScene : MonoBehaviour 
{

    public float speed;
    public int sceneNumber;

    private Fading fade;

    private void Start()
    {
        fade = GetComponent<Fading>();
    }

	void Update()
    {
        StartCoroutine(Change());
    }

    IEnumerator Change()
    {
        yield return new WaitForSeconds(0.8f);
        float fadeTime = fade.BeginFade(1);
        yield return new WaitForSeconds(fadeTime- speed);
        SceneManager.LoadScene(sceneNumber);
    }
   
}
