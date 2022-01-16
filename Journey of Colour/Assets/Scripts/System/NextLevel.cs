using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NextLevel : MonoBehaviour
{
    Scene currentScene;
    int nextSceneIndex;

    // Start is called before the first frame update
    void Start()
    {
        currentScene = SceneManager.GetActiveScene();
        nextSceneIndex = currentScene.buildIndex + 1;
    }

    // Update is called once per frame
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            GoToNextLevel();
        }
    }

    public void GoToNextLevel()
    {
        if (nextSceneIndex > SceneManager.sceneCount)
        {
            SceneManager.LoadScene(0);
        }
        else SceneManager.LoadScene(nextSceneIndex);
    }
}
