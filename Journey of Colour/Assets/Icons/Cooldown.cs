using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class Cooldown : MonoBehaviour
{
    [SerializeField] TMP_Text timeText, abilityText;
    [SerializeField] string ability;
    [SerializeField] float[] cooldowns;
    [SerializeField] Image cooldownImage;

    SwapClass swapClass;
    private string disabled = "X";
    private float cooldownTime;
    private bool onGround;
    private KeyCode key;        
    private CustomTimer timer;

    // Start is called before the first frame update
    void Start()
    {
        PickAbility(); 

        swapClass = GameObject.Find("Player").GetComponent<SwapClass>();

        timeText.gameObject.SetActive(false);
        timer = new CustomTimer(cooldownTime);
        timer.finish = true;
        cooldownImage.fillAmount = 0.0f;
    }

    // Update is called once per frame
    void Update()
    {
        switch (ability) 
        {
            case "DoubleJump":
                onGround = GameObject.Find("Player").GetComponentInChildren<DoubleJump>().landed;
                switch (onGround) 
                {
                    case false:
                        UseDoubleJump();
                        break;
                    default:
                        CooledDown();
                        break;
                }
                break;
            default:
                switch (timer.finish)
                {
                    case false:
                        timeText.text = (timer.timeRemaining).ToString("F1");
                        cooldownImage.fillAmount = timer.timeRemaining / cooldownTime;
                        break;
                    case true:
                        CooledDown();
                        if (Input.GetKeyDown(key))
                        {
                            UseAbility();
                        }
                        break;
                }
                break;
        }

        timer.Update();   
    }
    private void DisableCooldowns()
    {
        switch (swapClass.currentClass)
        {
            case SwapClass.playerClasses.Angel:
                switch (ability)
                {
                    case "Melee":
                        cooldownTime = cooldowns[0];
                        key = GameManager.GM.meleeAbility;
                        break;
                    case "Counter":
                        cooldownTime = cooldowns[1];
                        key = GameManager.GM.counterAbility;
                        break;
                    case "Fireball":
                        cooldownTime = cooldowns[2];
                        key = GameManager.GM.fireBallAbility;
                        break;                    
                }
                break;
            case SwapClass.playerClasses.Devil:
            //case "Dash":
            //    cooldownTime = cooldowns[3];
            //    key = GameManager.GM.dashAbility;
            //    abilityText.text = key.ToString();
            //    break;
            //case "DoubleJump":
            //    cooldownTime = cooldowns[4];
            //    key = KeyCode.Space;
            //    abilityText.text = key.ToString();
            //    break;
            //case "Float":
            //    cooldownTime = cooldowns[5];
            //    key = GameManager.GM.floatAbility;
            //    abilityText.text = key.ToString();
            //    break;
                break;
        }
    }

    public void UseAbility()
    {
        timeText.gameObject.SetActive(true);
        timer.Reset();
    }

    public void CooledDown()
    {
        timeText.gameObject.SetActive(false);
        cooldownImage.fillAmount = 0.0f;
    }
    public void UseDoubleJump()
    {
        cooldownImage.fillAmount = cooldownTime;
        timeText.gameObject.SetActive(true);
    }

    private void PickAbility()
    {
        switch (ability)
        {
            case "Melee":
                cooldownTime = cooldowns[0];
                key = GameManager.GM.meleeAbility;
                break;
            case "Counter":
                cooldownTime = cooldowns[1];
                key = GameManager.GM.counterAbility;
                break;
            case "Fireball":
                cooldownTime = cooldowns[2];
                key = GameManager.GM.fireBallAbility;
                break;
            case "Dash":
                cooldownTime = cooldowns[3];
                key = GameManager.GM.dashAbility;
                break;
            case "DoubleJump":
                cooldownTime = cooldowns[4];
                key = KeyCode.Space;
                break;
            case "Float":
                cooldownTime = cooldowns[5];
                key = GameManager.GM.floatAbility;
                break;
        }
        abilityText.text = key.ToString();
    }
}
