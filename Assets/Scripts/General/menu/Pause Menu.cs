using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

// Class to handle the pause menu
public class PauseMenu : MonoBehaviour
{
    
    [SerializeField] private GameObject pauseMenu; // Serialized field for the pause menu game object
    public static bool isPaused; // Static boolean to check if the game is paused
    // Start is called before the first frame update
    void Start()
    {
        // Initially, the pause menu is not active
        pauseMenu.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        // Check if the Escape key is pressed
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            
            if (isPaused) // If the game is already paused, resume the game
            {
                ResumeGame();
            }
            else  // If the game is not paused, pause the game
            {
                PauseGame();
            }
        }
    }
    // Method to pause the game
    public void PauseGame()
    {
        pauseMenu.SetActive(true); // Activate the pause menu
        Time.timeScale = 0f; // Stop the time in the game
        isPaused = true; // Set the isPaused flag to true
        GameObject.FindGameObjectWithTag("Player").GetComponent<FirstPersonController>().lockCursor = false;  // Unlock the cursor
        Cursor.lockState = CursorLockMode.None;
    }
    // Method to resume the game
    public void ResumeGame()
    {
        pauseMenu.SetActive(false);  // Deactivate the pause menu
        Time.timeScale = 1f; // Resume the time in the game
        isPaused = false; // Set the isPaused flag to false
        GameObject.FindGameObjectWithTag("Player").GetComponent<FirstPersonController>().lockCursor = true;
        Cursor.lockState = CursorLockMode.Locked; // Lock the cursor
    }
    // Method to go back to the main menu
    public void BackToMainMenu()
    {
        Time.timeScale = 1f; // Resume the time in the game
        isPaused = false; // Set the isPaused flag to false
        SceneManager.LoadSceneAsync("Starting menu");  // Load the starting menu scene asynchronously
    }
    // Method to quit the game
    public void Quit()
    {
        Application.Quit();
    }
}
