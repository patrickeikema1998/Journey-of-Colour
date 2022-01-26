using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ControlsMenu : MonoBehaviour
{
    public Transform controlPanel;
    Event keyEvent;
    Text buttonText;
    KeyCode newKey;

    bool waitingForKeys;

    void Start()
    {
        waitingForKeys = false;

        //We loopen hier door de childs van het panel. Dit zijn alle buttons die we willen aanpassen.
        //Deze hebben een text component die we willen veranderen. Dus zet deze code de text om naar wat het moet zijn,
        //namelijk de key die op de bepaalde keyCode staat. Dit is nog geen string, dus doen we een ToString() om het
        //om te zetten in text.
        for (int i = 0; i < controlPanel.childCount; i++)
        {
            switch (controlPanel.GetChild(i).name)
            {
                case "SwitchPlayerKey":
                    controlPanel.GetChild(i).GetComponentInChildren<Text>().text = GameManager.GM.SwitchPlayer.ToString();
                    break;
                case "DashKey":
                    controlPanel.GetChild(i).GetComponentInChildren<Text>().text = GameManager.GM.DashAbility.ToString();
                    break;
                case "FloatKey":
                    controlPanel.GetChild(i).GetComponentInChildren<Text>().text = GameManager.GM.FloatAbility.ToString();
                    break;
                case "FireBallKey":
                    controlPanel.GetChild(i).GetComponentInChildren<Text>().text = GameManager.GM.FireBallAbility.ToString();
                    break;
                case "MeleeKey":
                    controlPanel.GetChild(i).GetComponentInChildren<Text>().text = GameManager.GM.MeleeAbility.ToString();
                    break;
                case "CounterKey":
                    controlPanel.GetChild(i).GetComponentInChildren<Text>().text = GameManager.GM.CounterAbility.ToString();
                    break;
            }
        }
    }

    private void OnGUI()
    {
        keyEvent = Event.current;

        //Als de button is gepressed, wachten we op input.
        //De key die is ingedrukt wordt gezet naar keyEvent.keyCode.
        if (keyEvent.isKey && waitingForKeys)
        {
            newKey = keyEvent.keyCode;
            waitingForKeys = false;
        }
    }

    //when you are not waiting for a key, then assign the key.
    public void StartAssignment(string keyName)
    {
        if (!waitingForKeys)
        {
            StartCoroutine(AssignKey(keyName));
        }
    }

    //we can update the text of the button we are clicking on.
    public void SendText(Text text)
    {
        buttonText = text;
    }

    //this is a control statement.
    IEnumerator WaitForKey()
    {
        while (!keyEvent.isKey)
            yield return null;
    }

    public IEnumerator AssignKey(string keyName)
    {
        waitingForKeys = true;

        //stop the coroutine from executing until there is a key pressed on the keyboard.
        yield return WaitForKey();

        switch (keyName)
        {
            case "switchPlayer":
                GameManager.GM.SwitchPlayer = newKey;
                buttonText.text = GameManager.GM.SwitchPlayer.ToString();
                PlayerPrefs.SetString("switchPlayerKey", GameManager.GM.SwitchPlayer.ToString());
                break;
            case "dashAbility":
                GameManager.GM.DashAbility = newKey;
                buttonText.text = GameManager.GM.DashAbility.ToString();
                PlayerPrefs.SetString("dashAbilityKey", GameManager.GM.DashAbility.ToString());
                break;
            case "floatAbility":
                GameManager.GM.FloatAbility = newKey;
                buttonText.text = GameManager.GM.FloatAbility.ToString();
                PlayerPrefs.SetString("floatAbilityKey", GameManager.GM.FloatAbility.ToString());
                break;
            case "fireBallAbility":
                GameManager.GM.FireBallAbility = newKey;
                buttonText.text = GameManager.GM.FireBallAbility.ToString();
                PlayerPrefs.SetString("fireBallAbilityKey", GameManager.GM.FireBallAbility.ToString());
                break;
            case "meleeAbility":
                GameManager.GM.MeleeAbility = newKey;
                buttonText.text = GameManager.GM.MeleeAbility.ToString();
                PlayerPrefs.SetString("meleeAbilityKey", GameManager.GM.MeleeAbility.ToString());
                break;
            case "counterAbility":
                GameManager.GM.CounterAbility = newKey;
                buttonText.text = GameManager.GM.CounterAbility.ToString();
                PlayerPrefs.SetString("counterAbilityKey", GameManager.GM.CounterAbility.ToString());
                break;
        }

        yield return null;
    }
    //https://www.youtube.com/watch?v=iSxifRKQKAA
}