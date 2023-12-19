using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Passcode : MonoBehaviour
{
    string Code = "12345";
    string Number = null;
    int NumberIndex = 0;
    string alpha;
    [SerializeField]public TextMeshProUGUI UiText = null;

    public void CodeFunction(string Numbers)
    {
        NumberIndex++;
        Number = Number + Numbers;
        UiText.text = Number;
    }

    public void Enter()
    {
        if(Number == Code)
        {
            Debug.Log("KeypadWorking");
        }
    }
    public void Delete()
    {
        NumberIndex++;
        Number = null;
        UiText.text = Number;
    }


}
