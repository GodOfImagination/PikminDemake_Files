using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Character : MonoBehaviour
{
    private NavMeshAgent CharacterAgent;       // The Character's Navigation.
    private LineRenderer CharacterLine;        // The Character's Indicator.

    public bool CharacterSelected = false;     // Tells whether or not the character has been selected.
    public bool CharacterMoving = false;       // Tells whether or not the character is moving.
    public bool CanClick = true;               // Tells whether or not the player is allowed to click.

    void Start()
    {
        CharacterAgent = GetComponent<NavMeshAgent>();
        CharacterLine = GetComponent<LineRenderer>();

        CharacterLine.startWidth = 0.2f;
        CharacterLine.endWidth = 0.2f;
        CharacterLine.positionCount = 2;
        CharacterLine.enabled = false;
    }

    void Update()
    {
        Vector3 MousePosition = Input.mousePosition;
        Ray CastPoint = Camera.main.ScreenPointToRay(MousePosition);
        RaycastHit HitInfo;

        CharacterLine.SetPosition(0, transform.position);

        if (!CharacterAgent.pathPending)
        {
            if (CharacterAgent.remainingDistance <= CharacterAgent.stoppingDistance)
            {
                if (!CharacterAgent.hasPath || CharacterAgent.velocity.sqrMagnitude == 0f)
                {
                    CharacterMoving = false;
                    CanClick = true;
                }
            }
        }

        if (Physics.Raycast(CastPoint, out HitInfo, Mathf.Infinity) && CharacterMoving == false)
        {
            CharacterLine.SetPosition(1, HitInfo.point);
        }

        if (Input.GetMouseButtonDown(0) && CharacterSelected && CanClick)
        {
            CharacterMoving = true;
            CanClick = false;

            if (Physics.Raycast(CastPoint, out HitInfo, Mathf.Infinity))
            {
                Debug.Log("Clicked: " + HitInfo.point);
                CharacterAgent.SetDestination(HitInfo.point);
                CharacterLine.SetPosition(1, HitInfo.point);
            }
        }
    }

    public void Selected()
    {
        CharacterSelected = true;
        CharacterLine.enabled = true;
    }

    public void Unselected()
    {
        CharacterSelected = false;
        CharacterLine.enabled = false;
    }
}
