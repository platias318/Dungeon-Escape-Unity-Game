using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Passcode : MonoBehaviour
{
    string Code = "1357911246810"; // the code that the user has to input
    string Number = null;
    int NumberIndex = 0;
    string alpha;
    [SerializeField]public TextMeshProUGUI UiText = null; // the current text that shows what numbers the user is pressing
    [SerializeField] private Canvas KeyPadObj; // the keypad canvas itself
    [SerializeField] private GameObject door;
    private bool correct = false;

    public void CodeFunction(string Numbers)
    {
        NumberIndex++;
        Number = Number + Numbers;
        UiText.text = Number;
    }

    public void Enter() // the button that is pressed to see if the user has inserted the correct password
    {
        if(Number == Code)
        {
            correct = true;
        }
        else
        {
            correct = false;
        }
        if (KeyPadObj.enabled)
        {
            KeyPadObj.enabled = false;
            // logic to go to the next stage
        }
        GameObject.FindGameObjectWithTag("Player").GetComponent<FirstPersonController>().lockCursor = true;
        Cursor.lockState = CursorLockMode.Locked;
    }
    public void Delete() // the button that deletes the numbers because the user might have done a mistake along the way
    {
        NumberIndex++;
        Number = null;
        UiText.text = Number;
    }

    public bool isCorrect()
    {
        return correct;
    }


}
