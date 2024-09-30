using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Character : MonoBehaviour
{
    private NavMeshAgent CharacterAgent;
    private LineRenderer CharacterLine;

    public bool CharacterSelected = false;
    public bool CharacterMoving = false;
    public bool CanClick = true;

    void Start()
    {
        CharacterAgent = GetComponent<NavMeshAgent>();

        CharacterLine = gameObject.AddComponent<LineRenderer>();
        CharacterLine.startWidth = 0.2f;
        CharacterLine.endWidth = 0.2f;
        CharacterLine.positionCount = 2;
        CharacterLine.enabled = false;
    }

    void Update()
    {
        Vector3 MousePosition = Input.mousePosition;
        Ray CastPoint = Camera.main.ScreenPointToRay(MousePosition);
        RaycastHit Hit = new RaycastHit();

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

        if (Physics.Raycast(CastPoint, out Hit, Mathf.Infinity) && CharacterMoving == false)
        {
            CharacterLine.SetPosition(1, Hit.point);
        }

        if (Input.GetMouseButtonDown(0) && CharacterSelected && CanClick)
        {
            CharacterMoving = true;
            CanClick = false;

            if (Physics.Raycast(CastPoint, out Hit, Mathf.Infinity))
            {
                Debug.Log("Clicked: " + Hit.point);
                CharacterAgent.SetDestination(Hit.point);
                CharacterLine.SetPosition(1, Hit.point);
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
