using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ticket : MonoBehaviour
{
    public Vector3 Rotation;                   // Which direction the object rotates.
    public float RotationSpeed;                // How fast the object rotates.
    public bool CanRotate = true;              // Tells whether or not the object can rotate.

    private Character CharacterWithTicket;     // Keeps track of which character currently has picked up a ticket.

    void Update()
    {
        if (CanRotate == true)
            transform.Rotate(Rotation * RotationSpeed * Time.deltaTime);
    }

    private void OnTriggerEnter(Collider other)
    {
        Character Character = other.gameObject.GetComponent<Character>();

        if (Character.name == "Character1" || Character.name == "Character2" || Character.name == "Character3")
        {
            Debug.Log("Ticket Collected!");
            this.gameObject.SetActive(false);
            Character.CollectTicket();

            Character = CharacterWithTicket;
        }
    }

    public void TicketDropped()
    {
        this.gameObject.transform.position = CharacterWithTicket.transform.position;
        this.gameObject.SetActive(true);
    }
}
