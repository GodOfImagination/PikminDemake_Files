using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Goal : MonoBehaviour
{
    private AudioSource VictorySound;         // Sound that plays when the player wins the game.

    [Header("Tally\n")]
    public int CharacterGoal = 3;             // How many characters are required in order to win.
    public List<Character> CharacterList;     // Keeps track of whether or not a character(s) has reached the goal.

    [Header("Text\n")]
    public TextMeshProUGUI GoalText;          // Tells the player how many characters are at the goal and how many are required.

    [Space(10)]
    public GameObject CharacterBox;           // Tells the player if they have a specific character selected or not.
    public GameObject GoalBox;                // Tells the player how many characters are at the goal and how many are required.
    public GameObject WinBox;                 // Tells the player if they have all the characters at the goal.

    void Start()
    {
        VictorySound = GetComponent<AudioSource>();

        WinBox.SetActive(false);
    }

    private void OnTriggerEnter(Collider other)
    {
        Character Character = other.gameObject.GetComponent<Character>();

        if (Character.tag == "Red" || Character.tag == "Yellow" || Character.tag == "Blue")
        {
            CharacterList.Add(Character);
            GoalText.text = CharacterList.Count.ToString() + " of 3";

            if (CharacterList.Count >= CharacterGoal)
            {
                StartCoroutine(Winner());
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        Character Character = other.gameObject.GetComponent<Character>();

        if (Character.tag == "Red" || Character.tag == "Yellow" || Character.tag == "Blue")
        {
            CharacterList.Remove(Character);
            GoalText.text = CharacterList.Count.ToString() + " of 3";
        }
    }

    IEnumerator Winner()
    {
        yield return new WaitForSeconds(1);

        VictorySound.Play();

        CharacterBox.SetActive(false);
        GoalBox.SetActive(false);
        WinBox.SetActive(true);
    }
}
