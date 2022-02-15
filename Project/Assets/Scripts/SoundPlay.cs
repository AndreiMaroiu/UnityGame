using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundPlay : MonoBehaviour {

    public AudioSource source;
    public AudioClip clip;

	void Awake ()
    {
        if (PlayerPrefs.GetInt("ShowedAd") == 0)
        {
            source.PlayOneShot(clip);       
        }
	}
}
