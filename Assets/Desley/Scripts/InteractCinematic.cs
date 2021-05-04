using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractCinematic : MonoBehaviour
{
    [SerializeField] GameObject cinematic;
    public FadeManager fadeManager;

    void Start()
    {
        fadeManager = GameObject.FindGameObjectWithTag("FadeManager").GetComponent<FadeManager>();
    }

    public void StartCinematic()
    {
        StopAllCoroutines();

        StartCoroutine(Cinematic(.85f));
    }

    IEnumerator Cinematic(float time)
    {
        fadeManager.StartFade();

        yield return new WaitForSeconds(time);

        cinematic.SetActive(true);

        StopCoroutine(nameof(Cinematic));
    }

    public void DisableCinematic() { cinematic.SetActive(false); }
}
