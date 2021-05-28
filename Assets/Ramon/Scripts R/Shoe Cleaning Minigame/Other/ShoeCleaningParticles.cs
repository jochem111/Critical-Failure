using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShoeCleaningParticles : MonoBehaviour
{
    public ParticleSystem system;

    public void TriggerParticles()
    {
        system.Play();
    }
}
