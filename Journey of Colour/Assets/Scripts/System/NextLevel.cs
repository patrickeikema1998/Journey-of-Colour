using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NextLevel : MonoBehaviour
{
    [SerializeField] GameObject winScreen;
    private Scene currentScene;
    private int nextSceneIndex;
    private bool levelBeat = false;

    // Start is called before the first frame update
    void Start()
    {
        currentScene = SceneManager.GetActiveScene();
        nextSceneIndex = currentScene.buildIndex + 1;
        winScreen.SetActive(false);
    }

    //checks if player is at the level finish
    private void OnTriggerStay(Collider collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            levelBeat = true;
            winScreen.SetActive(true);
        }
    }

    // Update is called once per frame
    private void Update()
    {
        if (levelBeat == true && Input.GetKeyDown(KeyCode.Space))
        {
            GoToNextLevel();
        }
    }

    //Function that loads the next scene in the build index
    public void GoToNextLevel()
    {
        if (nextSceneIndex > SceneManager.sceneCount)
        {
            SceneManager.LoadScene(0);
        }
        else SceneManager.LoadScene(nextSceneIndex);
    }
}
