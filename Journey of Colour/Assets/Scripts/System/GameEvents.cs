using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class GameEvents : MonoBehaviour
{
    public static GameEvents current;

    private void Awake()
    {
        current = this;
    }

    public Action onPlayerDeath;
    public void PlayerDeath()
    {
        if(onPlayerDeath != null) onPlayerDeath();
    }

    public Action onRespawnPlayer;

    public void RespawnPlayer()
    {
        if (onRespawnPlayer != null) onRespawnPlayer();
    }
}
