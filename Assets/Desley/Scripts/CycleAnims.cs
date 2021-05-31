using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CycleAnims : MonoBehaviour
{
    [SerializeField] Animator animator;
    [SerializeField] float randomMin = 20, randomMax = 40;
    float randomTimer;
    bool cleaning = true;

    void Start()
    {
        randomTimer = Random.Range(randomMin, randomMax);
    }

    void Update()
    {
        randomTimer -= Time.deltaTime;

        if(randomTimer <= 0)
        {
            cleaning = !cleaning;

            animator.SetBool("Cleaning", cleaning);

            randomTimer = Random.Range(randomMin, randomMax);
        }
    }
}
