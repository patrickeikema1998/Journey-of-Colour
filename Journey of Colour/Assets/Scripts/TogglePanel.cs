using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TogglePanel : MonoBehaviour
{
    public GameObject Panel;
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if(Panel != null)
            {
                bool isActive = Panel.activeSelf;

                Panel.SetActive(!isActive);
            }
        }
    }
}
