using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonHandler : MonoBehaviour
{
    //This class is full of fuctions that menu buttons invoke


    //This function exits the game when invoked
    public void Exit()
    {
        Application.Quit();
    }

    //This function goes to the levelselect scene
    public void LevelSelect()
    {
        SceneManager.LoadScene(1);
    }

    //This function brings you back to the main menu
    public void StartScreen()
    {
        SceneManager.LoadScene(0);
    }

    //This function goes to wichever level is put in, it skips past the menu scenes
    public void GoToLevel(int index)
    {
        SceneManager.LoadScene(1 + index);
    }

}
