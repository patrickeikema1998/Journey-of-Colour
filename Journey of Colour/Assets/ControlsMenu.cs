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

        //Here we loop through the children of the panel. These are the buttons, they all have a text component
        //so in here we change the text component to the GameManager.GM.KeyCode put into a String
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

        //if the button is pressed, we are waiting for input
        //if the user presses a key, the new key will be assigned to newKey
        //we will assign the newKey to the key we are modifying
        if (keyEvent.isKey && waitingForKeys)
        {
            newKey = keyEvent.keyCode;
            waitingForKeys = false;
        }
    }

    //when you are not waiting for a key, then assign the key
    public void StartAssignment(string keyName)
    {
        if (!waitingForKeys)
        {
            StartCoroutine(AssignKey(keyName));
        }
    }

    //we can update the text of the button we are clicking on
    public void SendText(Text text)
    {
        buttonText = text;
    }

    //this is a control statement
    IEnumerator WaitForKey()
    {
        while (!keyEvent.isKey)
            yield return null;
    }

    public IEnumerator AssignKey(string keyName)
    {
        waitingForKeys = true;

        //stop the coroutine from executing until there is a key pressed on the keyboard
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