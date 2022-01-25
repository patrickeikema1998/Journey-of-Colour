using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonHandler : MonoBehaviour
{

    public void Exit()
    {
        Application.Quit();
    }

    public void LevelSelect()
    {
        SceneManager.LoadScene(1);
    }

    public void StartScreen()
    {
        SceneManager.LoadScene(0);
    }

    public void Options()
    {

    }

    public void GoToLevel(int index)
    {
        SceneManager.LoadScene(1 + index);
    }

}
