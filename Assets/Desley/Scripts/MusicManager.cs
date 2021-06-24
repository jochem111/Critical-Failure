using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;

public class MusicManager : MonoBehaviour
{
    [SerializeField] AudioMixer audioMixer;
    [SerializeField] GameObject cinematic, inTavern, minigame, star;
    [SerializeField] Slider slider;
    [SerializeField] float fadeOutTime;

    public float gameVolume;

    void Start()
    {
        gameVolume = slider.value;

        CinematicTrack(true);
    }

    public void SetVolume(float volume)
    {
        gameVolume = volume;
        audioMixer.SetFloat("Volume", volume);
    }

    public void CinematicTrack(bool play)
    {
        if (play)
        {
            cinematic.GetComponent<Animator>().SetTrigger("FadeIn");
            cinematic.GetComponent<AudioSource>().Play();
        }
        else
        {
            cinematic.GetComponent<Animator>().SetTrigger("FadeOut");
            StartCoroutine(StopTrack(cinematic.GetComponent<AudioSource>()));
        }
    }

    public void InTavernTrack(bool play)
    {
        if (play)
        {
            inTavern.GetComponent<Animator>().SetTrigger("FadeIn");
            inTavern.GetComponent<AudioSource>().Play();
        }
        else
        {
            inTavern.GetComponent<Animator>().SetTrigger("FadeOut");
            StartCoroutine(StopTrack(inTavern.GetComponent<AudioSource>()));
        }
    }

    public void MinigameTrack(bool play)
    {
        if (play)
        {
            minigame.GetComponent<Animator>().SetTrigger("FadeIn");
            minigame.GetComponent<AudioSource>().Play();
        }
        else
        {
            minigame.GetComponent<Animator>().SetTrigger("FadeOut");
            StartCoroutine(StopTrack(minigame.GetComponent<AudioSource>()));
        }
    }

    IEnumerator StopTrack(AudioSource audioSource)
    {
        yield return new WaitForSeconds(fadeOutTime);

        audioSource.Stop();
    }

    public void StarSound()
    {
        star.GetComponent<AudioSource>().Play();
    }
}
