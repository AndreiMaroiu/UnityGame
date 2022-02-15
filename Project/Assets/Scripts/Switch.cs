using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Switch : MonoBehaviour
{

    public void Schimba(int numarulScenei)
    {
        SceneManager.LoadScene(numarulScenei, LoadSceneMode.Single);
        Stats.SaveStats();
    }
}