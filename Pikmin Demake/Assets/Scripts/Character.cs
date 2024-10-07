using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Character : MonoBehaviour
{
    private NavMeshAgent Agent;                // The Character's Navigation.
    private LineRenderer Indicator;            // The Character's Indicator.

    public GameObject FaceSelected;            // The Character's Selected Face.
    public GameObject FaceUnselected;          // The Character's Unselected Face.
    public GameObject FakeTicket;              // The Character's Fake Ticket.

    public AudioSource SelectSound;            // The Character's Select / Unselect Sound.
    public AudioSource ClickSound;             // The Character's Click Sound.
    private AudioSource MoveSound;             // The Character's Moving Sound.

    private Ticket TicketScript;

    public bool CharacterSelected = false;     // Tells whether or not the character has been selected.
    public bool CharacterMoving = false;       // Tells whether or not the character is moving.
    public bool CanClick = true;               // Tells whether or not the player is allowed to click.
    public bool HasTicket = false;             // Tells whether or not the character has a ticket.

    void Start()
    {
        Agent = GetComponent<NavMeshAgent>();
        Indicator = GetComponent<LineRenderer>();
        MoveSound = GetComponent<AudioSource>();

        Indicator.startWidth = 0.2f;
        Indicator.endWidth = 0.2f;
        Indicator.positionCount = 2;
        Indicator.enabled = false;

        FaceSelected.SetActive(false);
        FaceUnselected.SetActive(true);
        FakeTicket.SetActive(false);

        TicketScript = GameObject.FindObjectOfType<Ticket>();
    }

    void Update()
    {
        Vector3 MousePosition = Input.mousePosition;
        Ray CastPoint = Camera.main.ScreenPointToRay(MousePosition);
        RaycastHit HitInfo;

        Indicator.SetPosition(0, transform.position);

        if (!Agent.pathPending)
        {
            if (Agent.remainingDistance <= Agent.stoppingDistance)
            {
                if (!Agent.hasPath || Agent.velocity.sqrMagnitude == 0f)
                {
                    MoveSound.Pause();
                    CharacterMoving = false;
                    CanClick = true;
                }
            }
        }

        if (Physics.Raycast(CastPoint, out HitInfo, Mathf.Infinity) && CharacterMoving == false)
        {
            Indicator.SetPosition(1, HitInfo.point);
        }

        if (Input.GetMouseButtonDown(0) && CharacterSelected && CanClick)
        {
            ClickSound.Play();
            MoveSound.Play();
            CharacterMoving = true;
            CanClick = false;

            if (Physics.Raycast(CastPoint, out HitInfo, Mathf.Infinity))
            {
                Debug.Log("Clicked: " + HitInfo.point);
                Agent.SetDestination(HitInfo.point);
                Indicator.SetPosition(1, HitInfo.point);
            }
        }
    }

    public void Selected()
    {
        CharacterSelected = true;
        Indicator.enabled = true;
        SelectSound.Play();

        FaceSelected.SetActive(true);
        FaceUnselected.SetActive(false);
    }

    public void Unselected()
    {
        CharacterSelected = false;
        Indicator.enabled = false;
        SelectSound.Play();

        FaceSelected.SetActive(false);
        FaceUnselected.SetActive(true);
    }

    public void CollectTicket()
    {
        FakeTicket.SetActive(true);

        HasTicket = true;
    }

    public void DropTicket()
    {
        FakeTicket.SetActive(false);

        HasTicket = false;

        TicketScript.TicketDropped();
    }
}
