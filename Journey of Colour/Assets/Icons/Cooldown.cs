using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Cooldown : MonoBehaviour
{
    [SerializeField] Texture cooldown;
    [SerializeField] float cooldownTime;

    private CustomTimer timer;

    // Start is called before the first frame update
    void Start()
    {
        timer = new CustomTimer(cooldownTime);
    }

    // Update is called once per frame
    void Update()
    {
        timer.Update();   
    }
}
