using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public static class GameEvents
{

    public static Action onPlayerDeath;
    public static void PlayerDeath()
    {
        if(onPlayerDeath != null) onPlayerDeath();
    }

    public static Action onRespawnPlayer;
    public static void RespawnPlayer()
    {
        if (onRespawnPlayer != null) onRespawnPlayer();
    }

}
