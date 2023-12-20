using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainStoryLoader : MonoBehaviour
{
   void OnEnable()
   {
        SceneManager.LoadScene("Stage3", LoadSceneMode.Single);
   }
}
