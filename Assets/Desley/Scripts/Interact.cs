using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class Interact : MonoBehaviour
{
    public bool canInteract;
    [SerializeField] float maxInteractDistance;

    [SerializeField] List<Transform> interactables;
    [SerializeField] Transform door;

    GameObject interactingWith;
    GameObject vCam;
    Transform playerPos;

    [Space, SerializeField] StarManager starManager;
    [SerializeField] TavernManager tavernManager;
    [SerializeField] FadeManager fadeManager;
    PlayerMovement playerMove;

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
        else
        {
            //No interaction found / Find distance to door
            float distance = Vector3.Distance(transform.position, door.position);
            if(distance <= maxInteractDistance) 
            {
                tavernManager.RotateDoor(0);
            }
        }
    }

    void StartInteraction()
    {
        Cursor.lockState = CursorLockMode.None;
        playerMove.AllowMovement(false);
        canInteract = false;

        InteractContents iContents = interactingWith.GetComponent<InteractContents>();

        GameObject cinematic = iContents.cinematic;
        fadeManager.StartFade(cinematic, true);

        vCam = iContents.vCam;
        playerPos = iContents.playerPos;

        StartCoroutine(ChangePos(fadeManager.fadeTime));
    }

    public void StopIntercation()
    {
        index++;
        if (index == 2)
        {
            starManager.AddStar();
            return;
        }

        fadeManager.StartFade(vCam, true);
    }

    public void FinishInteraction()
    {
        Cursor.lockState = CursorLockMode.Locked;
        playerMove.AllowMovement(true);
        canInteract = true;

        vCam.SetActive(false);
        index = 0;
    }

    IEnumerator ChangePos(float time)
    {
        yield return new WaitForSeconds(time);

        transform.position = playerPos.position;
        transform.rotation = playerPos.rotation;

        StopCoroutine(nameof(ChangePos));
    }

    void OnTriggerEnter(Collider other)
    {
        tavernManager.UpdateTaverns();
    }
}
