using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CreditsToMenuLoader : MonoBehaviour
{
    void OnEnable()
    {
        SceneManager.LoadSceneAsync("Starting menu");
    }
}
