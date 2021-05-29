using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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


    void Start()
    {
        playerMove = GetComponent<PlayerMovement>();

        GetInteractables();
    }

    public void GetInteractables()
    {
        //Remove all interactables
        foreach(Transform obj in interactables.ToArray()) { interactables.Remove(obj); }

        //Find every interactable that is currently active
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
            StopInteraction();
    }

    void CheckForDistance()
    {
        float closestDistance = Mathf.Infinity;
        GameObject closestObject = null;

        //Find closest interaction
        foreach(Transform obj in interactables)
        {
            float distance = Vector3.Distance(transform.position, obj.position);

            if(distance < closestDistance) 
            { 
                closestDistance = distance;
                closestObject = obj.gameObject;
            }
        }

        //If interaction is close enough
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

        //Reference the contents of the interaction
        InteractContents iContents = interactingWith.GetComponent<InteractContents>();

        GameObject cinematic = iContents.cinematic;
        GameObject dialogUi = null;

        if (interactingWith.GetComponent<DialogueTrigger>() != null)
        {
            Manager.manager.dialogueManager.StartDialogue(interactingWith.GetComponent<DialogueTrigger>().dialogue);
            dialogUi = Manager.manager.dialogueManager.dialogueBox;
        }

        fadeManager.StartFade(cinematic, true, dialogUi);

        vCam = iContents.vCam;
        playerPos = iContents.playerPos;

        StartCoroutine(ChangePos(fadeManager.fadeTime));


    }

    public void StopInteraction() // puur debug
    {
        /*
        index++;

        if(index == 1)
            fadeManager.StartFade(vCam, true);
        else
            starManager.AddStar();
            */
    }

    public void FinishInteraction()
    {
        Cursor.lockState = CursorLockMode.Locked;
        playerMove.AllowMovement(true);
        canInteract = true;

        vCam.SetActive(false);
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
