using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    public void LoadMenu()
    {
        SceneManager.LoadScene("Menu");
    }

    public void LoadLevel()
    {
        SceneManager.LoadScene("Level");
    }

    public void Exit()
    {
        Application.Quit();
    }
}
