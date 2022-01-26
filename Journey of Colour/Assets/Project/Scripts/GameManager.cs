using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager GM;

    public KeyCode SwitchPlayer { get; set; }
    public KeyCode DashAbility { get; set; }
    public KeyCode FloatAbility { get; set; }
    public KeyCode FireBallAbility { get; set; }
    public KeyCode MeleeAbility { get; set; }
    public KeyCode CounterAbility { get; set; }

    const string switchPlayerDefaultKey = "switchPlayerKey";
    const string dashAbilityDefaultKey = "dashAbilityKey";
    const string floatAbilityDefaultKey = "floatAbilityKey";
    const string fireBallAbilityKey = "fireBallAbilityKey";
    const string meleeAbilityKey = "meleeAbilityKey";
    const string CounterAbilityKey = "CounterAbilityKey";

    //Maakt een singleton van deze class
    private void Awake()
    {
        if (GM == null)
        {
            DontDestroyOnLoad(gameObject);
            GM = this;
        }
        else if (GM != this)
        {
            Destroy(gameObject);
        }

        // We gebruiken PlayerPrefs zodat het wordt opgeslagen op het systeem waar dit spel wordt opgestart.

        // De string van de genoemde PlayerPrefs.GetString wordt omgezet in een enum waarde. Deze wordt weer omgezet naar een KeyCode.
        // Als de waarde uit de string in de lijst van de KeyCodes staat, wordt deze eruit gepakt.

        SwitchPlayer = (KeyCode)System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString(switchPlayerDefaultKey, "W"));
        DashAbility = (KeyCode)System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString(dashAbilityDefaultKey, "E"));
        FloatAbility = (KeyCode)System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString(floatAbilityDefaultKey, "X"));
        FireBallAbility = (KeyCode)System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString(fireBallAbilityKey, "E"));
        MeleeAbility = (KeyCode)System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString(meleeAbilityKey, "Q"));
        CounterAbility = (KeyCode)System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString(CounterAbilityKey, "C"));
    }
    // Ik heb deze code geschreven aan de hand van dit filmpje:
    // https://www.youtube.com/watch?v=iSxifRKQKAA
}
