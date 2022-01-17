using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NextLevel : MonoBehaviour
{
    [SerializeField] GameObject winScreen;
    Scene currentScene;
    int nextSceneIndex;
    bool levelBeat = false;

    // Start is called before the first frame update
    void Start()
    {
        currentScene = SceneManager.GetActiveScene();
        nextSceneIndex = currentScene.buildIndex + 1;
        winScreen.SetActive(false);
    }

    
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
    public void GoToNextLevel()
    {
        if (nextSceneIndex > SceneManager.sceneCount)
        {
            SceneManager.LoadScene(0);
        }
        else SceneManager.LoadScene(nextSceneIndex);
    }
}
