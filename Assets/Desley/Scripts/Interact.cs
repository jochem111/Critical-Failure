using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class Interact : MonoBehaviour
{
    public bool canInteract;
    [SerializeField] float maxInteractDistance;

    [Space, SerializeField] List<Transform> interactables;
    GameObject interactingWith;
    GameObject cinematic;

    [Space, SerializeField] StarManager starManager;
    [SerializeField] FadeManager fadeManager;
    PlayerMovement playerMove;

    [Space, SerializeField] GameObject vCam;

    int index;

    void Start()
    {
        playerMove = GetComponent<PlayerMovement>();

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
            CheckForDistance();

        if (Input.GetButtonDown("Jump"))
            StopIntercation();
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
            interactingWith = closestObject;

            StartInteraction();
        }
    }

    void StartInteraction()
    {
        Cursor.lockState = CursorLockMode.None;
        playerMove.AllowMovement(false);
        canInteract = false;

        cinematic = interactingWith.GetComponentInChildren<PlayableDirector>().gameObject;
        fadeManager.StartFade(cinematic, true);
    }

    public void StopIntercation()
    {
        index++;
        if (index == 2)
        {
            FinishInteraction();
            return;
        }

        fadeManager.StartFade(vCam, true);
    }

    void FinishInteraction()
    {
        Cursor.lockState = CursorLockMode.Locked;
        playerMove.AllowMovement(true);
        canInteract = true;

        starManager.AddStar();

        vCam.SetActive(false);
        index = 0;
    }
}
