using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager GM;

    public KeyCode switchPlayer { get; set; }
    public KeyCode dashAbility { get; set; }
    public KeyCode floatAbility { get; set; }
    public KeyCode fireBallAbility { get; set; }
    public KeyCode meleeAbility { get; set; }
    public KeyCode counterAbility { get; set; }

    //Maakt een singleton van deze class
    private void Awake()
    {
        if(GM == null)
        {
            DontDestroyOnLoad(gameObject);
            GM = this;
        } else if(GM != this)
        {
            Destroy(gameObject);
        }

        // De System.Enum.Parse zorgt ervoor dat we de KeyCode kunnen zetten naar een bepaalde waarde.
        // In dit geval is de standaardwaarde van de KeyCode 'switchPlayer': W. Dit staat in de PlayerPrefs.
        // We gebruiken PlayerPrefs zodat het wordt opgeslagen op het systeem waar dit spel wordt opgestart.
        switchPlayer = (KeyCode)System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("switchPlayerKey", "W"));
        dashAbility = (KeyCode)System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("dashAbilityKey", "E"));
        floatAbility = (KeyCode)System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("floatAbilityKey", "X"));
        fireBallAbility = (KeyCode)System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("fireBallAbilityKey", "E"));
        meleeAbility = (KeyCode)System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("meleeAbilityKey", "Q"));
        counterAbility = (KeyCode)System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("CounterAbilityKey", "C"));

    }
    // Ik heb deze code geschreven aan de hand van dit filmpje:
    // https://www.youtube.com/watch?v=iSxifRKQKAA
}
