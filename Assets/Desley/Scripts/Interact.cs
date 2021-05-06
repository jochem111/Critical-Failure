using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Interact : MonoBehaviour
{
    public bool canInteract;
    [SerializeField] float maxInteractDistance;

    [Space, SerializeField] List<Transform> interactables;
    GameObject interactingWith;

    PlayerMovement playerMove;

    [Space, SerializeField] StarManager starManager;

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

        interactingWith.GetComponent<InteractCinematic>().StartCinematic();
    }

    public void StopIntercation()
    {
        Cursor.lockState = CursorLockMode.Locked;
        playerMove.AllowMovement(true);

        interactingWith.GetComponent<InteractCinematic>().DisableCinematic();

        starManager.AddStar();
    }
}
