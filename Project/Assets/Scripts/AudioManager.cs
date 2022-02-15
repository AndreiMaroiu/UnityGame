using UnityEngine;
using System.Collections;
using UnityEngine.Audio;
using UnityEngine.UI;

public class AudioManager : MonoBehaviour 
{
    public AudioMixer mixer;
    private float masterVol;
    private float musicVol;
    private float sfxVol;

    public Slider masterSlider;
    public Slider musicSlider;
    public Slider sfxSlider;

    public AudioSource source;
    public AudioClip clip;
    private bool availability = true;

    private void Awake()
    {
        masterVol = PlayerPrefs.GetFloat("MasterVol");
        musicVol = PlayerPrefs.GetFloat("MusicVol");
        sfxVol = PlayerPrefs.GetFloat("SfxVol");
        masterSlider.value = masterVol;
        musicSlider.value = musicVol;
        sfxSlider.value = sfxVol;
    }

    public void SetMasterVol(float vol)
    {
        masterVol = Mute(vol);
        mixer.SetFloat("MasterVol", masterVol);

        PlayerPrefs.SetFloat("MasterVol", masterVol);
    }

    public void SetMusicVol(float vol)
    {
        musicVol = Mute(vol);
        mixer.SetFloat("musicVol", musicVol);

        PlayerPrefs.SetFloat("MusicVol", musicVol);
    }

    public void SetSFXVol(float vol)
    {
        sfxVol = Mute(vol);
        mixer.SetFloat("sfxVol", sfxVol);

        PlayerPrefs.SetFloat("SfxVol", sfxVol);
    }

    private static float Mute(float volume)
    {
        if (volume <= -40f)
        {
            volume = -80;
        }
        
        return volume;
    }

    public void PlaySound()
    {
        if (availability)
        {
            StartCoroutine(PlayOnShot());
        }
    }

    IEnumerator PlayOnShot()
    {
        source.Play();
        yield return new WaitForSeconds(0.01f);
        availability = false;
        yield return new WaitForSeconds(0.1f);
        availability = true;
    }
}
