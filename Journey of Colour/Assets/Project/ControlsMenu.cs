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
            if (controlPanel.GetChild(i).name == "SwitchPlayerKey")
                controlPanel.GetChild(i).GetComponentInChildren<Text>().text = GameManager.GM.switchPlayer.ToString();
            else if (controlPanel.GetChild(i).name == "DashKey")
                controlPanel.GetChild(i).GetComponentInChildren<Text>().text = GameManager.GM.dashAbility.ToString();
            else if (controlPanel.GetChild(i).name == "FloatKey")
                controlPanel.GetChild(i).GetComponentInChildren<Text>().text = GameManager.GM.floatAbility.ToString();
            else if (controlPanel.GetChild(i).name == "FireBallKey")
                controlPanel.GetChild(i).GetComponentInChildren<Text>().text = GameManager.GM.fireBallAbility.ToString();
            else if (controlPanel.GetChild(i).name == "MeleeKey")
                controlPanel.GetChild(i).GetComponentInChildren<Text>().text = GameManager.GM.meleeAbility.ToString();
            else if (controlPanel.GetChild(i).name == "CounterKey")
                controlPanel.GetChild(i).GetComponentInChildren<Text>().text = GameManager.GM.counterAbility.ToString();
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
                GameManager.GM.switchPlayer = newKey;
                buttonText.text = GameManager.GM.switchPlayer.ToString();
                PlayerPrefs.SetString("switchPlayerKey", GameManager.GM.switchPlayer.ToString());
                break;
            case "dashAbility":
                GameManager.GM.dashAbility = newKey;
                buttonText.text = GameManager.GM.dashAbility.ToString();
                PlayerPrefs.SetString("dashAbilityKey", GameManager.GM.dashAbility.ToString());
                break;
            case "floatAbility":
                GameManager.GM.floatAbility = newKey;
                buttonText.text = GameManager.GM.floatAbility.ToString();
                PlayerPrefs.SetString("floatAbilityKey", GameManager.GM.floatAbility.ToString());
                break;
            case "fireBallAbility":
                GameManager.GM.fireBallAbility = newKey;
                buttonText.text = GameManager.GM.fireBallAbility.ToString();
                PlayerPrefs.SetString("fireBallAbilityKey", GameManager.GM.fireBallAbility.ToString());
                break;
            case "meleeAbility":
                GameManager.GM.meleeAbility = newKey;
                buttonText.text = GameManager.GM.meleeAbility.ToString();
                PlayerPrefs.SetString("meleeAbilityKey", GameManager.GM.meleeAbility.ToString());
                break;
            case "counterAbility":
                GameManager.GM.counterAbility = newKey;
                buttonText.text = GameManager.GM.counterAbility.ToString();
                PlayerPrefs.SetString("counterAbilityKey", GameManager.GM.counterAbility.ToString());
                break;
        }

        yield return null;
    }
    //https://www.youtube.com/watch?v=iSxifRKQKAA
}