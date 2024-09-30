using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Goal : MonoBehaviour
{
    [Header("Goal\n")]
    public int CharacterGoal = 3;        // How many characters are required in order to win.
    public List<Character> CharacterList;     // Keeps track of whether or not a character(s) has reached the goal.

    [Header("Text\n")]
    public TextMeshProUGUI GoalText;          // Tells the player if they have all characters at the goal.

    void Update()
    {
        if (CharacterList.Count >= CharacterGoal)
        {

        }
    }

    private void OnTriggerEnter(Collider other)
    {
        Character Character = other.gameObject.GetComponent<Character>();

        if (Character.tag == "Red" || Character.tag == "Yellow" || Character.tag == "Blue")
        {
            CharacterList.Add(Character);
            GoalText.text = CharacterGoal.ToString() + " of 3";
        }
    }

    private void OnTriggerExit(Collider other)
    {
        Character Character = other.gameObject.GetComponent<Character>();

        if (Character.tag == "Red" || Character.tag == "Yellow" || Character.tag == "Blue")
        {
            CharacterList.Remove(Character);
            GoalText.text = CharacterGoal.ToString() + " of 3";
        }
    }
}
