using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using UnityEngine.UI;

public class GetAllPowerUps : MonoBehaviour
{
    [SerializeField] private Text text;
    
    private PowerUpObject[] powerUps;
    
    private void Start()
    {
        powerUps = ScriptablesManager.GetAllScriptables<PowerUpObject>();

        StringBuilder builder = new StringBuilder($"{powerUps.Length.ToString()}\n");

        for (int i = 0; i < powerUps.Length; ++i)
        {
            builder.Append($"{powerUps[i].powerUpName}\n");
        }

        text.text = builder.ToString();
    }
}
