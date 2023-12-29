using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

// Class to handle the level selection menu
public class SelectLevelMenu : MonoBehaviour
{
    // Method to load the first stage
    public void LoadStage1()
    {
        // Load the scene asynchronously named "Stage1"
        SceneManager.LoadSceneAsync("Stage1");
    }
    // Method to load the second stage
    public void LoadStage2()
    {
        // Load the scene asynchronously named "Stage2"
        SceneManager.LoadSceneAsync("Stage2");
    }
    // Method to load the third stage
    public void LoadStage3()
    {
        // Load the scene asynchronously named "Stage3"
        SceneManager.LoadSceneAsync("Stage3");
    }
}
