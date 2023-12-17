using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SelectLevelMenu : MonoBehaviour
{
    public void LoadStage1()
    {
        SceneManager.LoadSceneAsync("Stage1");
    }
    public void LoadStage2()
    {
        SceneManager.LoadSceneAsync("Stage2");
    }
    public void LoadStage3()
    {
        SceneManager.LoadSceneAsync("Stage3");
    }
}
