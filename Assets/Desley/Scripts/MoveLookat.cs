using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveLookat : MonoBehaviour
{
    [SerializeField] Cinematics cinematics;

    [Space, SerializeField] Transform lookatEnd;
    [SerializeField] float speed;

    void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position, lookatEnd.position, speed * Time.deltaTime);

        if (transform.position == lookatEnd.position)
        {
            cinematics.DisableCinematic();
        }
    }
}
