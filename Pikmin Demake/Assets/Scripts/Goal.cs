using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Goal : MonoBehaviour
{
    private AudioSource VictorySound;         // Sound that plays when the player wins the game.

    [Header("Tally\n")]
    public int CharacterGoal = 3;             // How many characters are required in order to win.
    public int TicketGoal = 3;                // How many tickets are required in order to win.
    public List<Character> CharacterList;     // Keeps track of whether or not a character(s) has reached the goal.
    public List<int> TicketList;              // Keeps track of whether or not a ticket(s) has reached the goal.

    [Header("Text\n")]
    public TextMeshProUGUI CharacterText;     // Tells the player how many characters are at the goal and how many are required.
    public TextMeshProUGUI TicketText;        // Tells the player how many tickets are at the goal and how many are required.

    [Space(10)]
    public GameObject CharacterBox;           // Tells the player if they have a specific character selected or not.
    public GameObject GoalBox1;               // Tells the player how many characters are at the goal and how many are required.
    public GameObject GoalBox2;               // Tells the player how many tickets are at the goal and how many are required.
    public GameObject WinBox;                 // Tells the player if they have all the characters at the goal.

    void Start()
    {
        VictorySound = GetComponent<AudioSource>();

        WinBox.SetActive(false);
    }

    private void OnTriggerEnter(Collider other)
    {
        Character Character = other.gameObject.GetComponent<Character>();

        if (Character.name == "Character1" || Character.name == "Character2" || Character.name == "Character3")
        {
            CharacterList.Add(Character);
            CharacterText.text = CharacterList.Count.ToString() + " of 3";
        }

        if (other.gameObject.name == "FakeTicket" || other.gameObject.name == "Ticket" || other.gameObject.CompareTag("Ticket"))
        {
            //TicketList.Add(other.gameObject);
            TicketText.text = TicketList.Count.ToString() + " of 3";
        }

        if (CharacterList.Count >= CharacterGoal && TicketList.Count >= TicketGoal)
        {
            StartCoroutine(Winner());
        }
    }

    private void OnTriggerExit(Collider other)
    {
        Character Character = other.gameObject.GetComponent<Character>();

        if (Character.name == "Character1" || Character.name == "Character2" || Character.name == "Character3")
        {
            CharacterList.Remove(Character);
            CharacterText.text = CharacterList.Count.ToString() + " of 3";
        }

        if (other.gameObject.name == "FakeTicket" || other.gameObject.name == "Ticket" || other.gameObject.CompareTag("Ticket"))
        {
            //TicketList.Remove(other.gameObject);
            TicketText.text = TicketList.Count.ToString() + " of 3";
        }
    }

    IEnumerator Winner()
    {
        yield return new WaitForSeconds(1);

        VictorySound.Play();

        CharacterBox.SetActive(false);
        GoalBox1.SetActive(false);
        GoalBox2.SetActive(false);
        WinBox.SetActive(true);
    }

    public void TicketCollected()
    {
        TicketList.Add(1);
        TicketText.text = TicketList.Count.ToString() + " of 3";
    }
}
