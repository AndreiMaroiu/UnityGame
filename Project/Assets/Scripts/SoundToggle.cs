using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;

public class SoundToggle : MonoBehaviour {

     public GameObject[] buttons;
     public Animator[] animator;

     public static int availability = 1;

    private int currentAnimator;

     void Awake()
     {
         if(PlayerPrefs.GetInt("SoundToggle") == 1)
         {
             availability = 1;
             buttons[0].SetActive(true);
            currentAnimator = 0;
             AudioListener.pause = !true;
         }
         else
         {
             availability = 0;
            buttons[1].SetActive(true);
            currentAnimator = 1;
            AudioListener.pause = !false;
        }
        animator[currentAnimator].SetInteger("Toggle", availability);

         Debug.Log(availability);
     }

     public void Toggle()
     {
         GetAvailability();
         PlayerPrefs.SetInt("SoundToggle", availability);
         animator[currentAnimator].SetInteger("Toggle", availability);

         Debug.Log(availability);
     }

     void GetAvailability()
     {
         if (availability == 1)
         {
             availability = 0;
             AudioListener.pause = !false;
         }
         else
         {
             availability = 1;
             AudioListener.pause = !true;
         }
     } 
}
