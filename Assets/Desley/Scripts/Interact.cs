using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interact : MonoBehaviour
{
    public bool canInteract;
    [SerializeField] float maxInteractDistance;

    [Space, SerializeField] List<Transform> interactables;

    void Start()
    {
        GetInteractables();
    }

    public void GetInteractables()
    {
        foreach(Transform obj in interactables) { interactables.Remove(obj); }

        foreach(GameObject obj in GameObject.FindGameObjectsWithTag("Interactable"))
        {
            if (!interactables.Contains(obj.transform))
            {
                interactables.Add(obj.transform);
            }
        }
    }

    void Update()
    {
        if (Input.GetButtonDown("Interact") && canInteract)
        {
            CheckForDistance();
        }
    }

    void CheckForDistance()
    {
        float closestDistance = Mathf.Infinity;
        GameObject closestObject = null;

        foreach(Transform obj in interactables)
        {
            float distance = Vector3.Distance(transform.position, obj.position);

            if(distance < closestDistance) 
            { 
                closestDistance = distance;
                closestObject = obj.gameObject;
            }
        }

        if(closestDistance <= maxInteractDistance)
        {
            StartInteraction(closestObject);
        }
    }

    void StartInteraction(GameObject interactable)
    {
        //start the interaction
    }
}
