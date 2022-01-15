using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuPanels : MonoBehaviour
{
    public GameObject menuPanel;
    public GameObject controlPanel;
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape) && !menuPanel.activeInHierarchy && !controlPanel.activeInHierarchy)
        {
            menuPanel.SetActive(true);
        }
        else if (Input.GetKeyDown(KeyCode.Escape) && !menuPanel.activeInHierarchy && controlPanel.activeInHierarchy)
        {
            menuPanel.SetActive(false);
            controlPanel.SetActive(false);
        }
        else if (Input.GetKeyDown(KeyCode.Escape) && menuPanel.activeInHierarchy && !controlPanel.activeInHierarchy)
        {
            menuPanel.SetActive(false);
        }
    }
}
