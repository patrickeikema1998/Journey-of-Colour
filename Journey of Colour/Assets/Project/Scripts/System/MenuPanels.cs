using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuPanels : MonoBehaviour
{
    public GameObject menuPanel;
    public GameObject controlPanel;
    public GameObject settingsPanel;

    AutomaticScrolling automaticScrolling;

    private void Start()
    {
        automaticScrolling = GameObject.Find("Main Camera").GetComponent<AutomaticScrolling>();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (!menuPanel.activeInHierarchy && !controlPanel.activeInHierarchy && !settingsPanel.activeInHierarchy)
            {
                menuPanel.SetActive(true);
                automaticScrolling.moving = false;
            }
            else if (menuPanel.activeInHierarchy)
            {
                menuPanel.SetActive(false);
                automaticScrolling.moving = true;
            }
            else if (controlPanel.activeInHierarchy)
            {
                controlPanel.SetActive(false);
                automaticScrolling.moving = true;
            }
            else if (settingsPanel.activeInHierarchy)
            {
                settingsPanel.SetActive(false);
                automaticScrolling.moving = true;
            }
        }
    }
}
