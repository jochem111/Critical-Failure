using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FxSelfDestroy : MonoBehaviour
{
    public float secondsBeforeDestroyed = 1f;

    private void Start()
    {
        StartCoroutine(destroyTimer());
    }

    private IEnumerator destroyTimer()
    {
        yield return new WaitForSeconds(secondsBeforeDestroyed);
        Destroy(gameObject);
    }
}
