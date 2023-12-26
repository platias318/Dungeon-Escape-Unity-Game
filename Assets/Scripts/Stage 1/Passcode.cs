using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Passcode : MonoBehaviour
{
    string Code = "1357911246810";
    string Number = null;
    int NumberIndex = 0;
    string alpha;
    [SerializeField]public TextMeshProUGUI UiText = null;
    [SerializeField] private Canvas KeyPadObj;

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
            if (KeyPadObj.enabled)
            {
                KeyPadObj.enabled = false;
                // logic to go to the next stage
            }
        }
    }
    public void Delete()
    {
        NumberIndex++;
        Number = null;
        UiText.text = Number;
    }


}
