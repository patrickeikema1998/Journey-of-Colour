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
    SwapClass.playerClasses prevClass;
    private string disabled = "";
    private float cooldownTime;
    private bool disable;
    private bool onGround;
    private KeyCode key;        
    private CustomTimer timer;
    private GameObject player;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player");
        swapClass = player.GetComponent<SwapClass>();
        prevClass = swapClass.currentClass;

        PickAbility();

        timeText.gameObject.SetActive(false);
        timer = new CustomTimer(cooldownTime);
        timer.finish = true;
        cooldownImage.fillAmount = 0.0f;
        abilityText.enableAutoSizing = true;

        DisableCooldowns();
    }

    // Update is called once per frame
    void Update()
    {

        if (swapClass.currentClass != prevClass)
        {
            DisableCooldowns();
            prevClass = swapClass.currentClass;
        }

        switch (disable) 
        {
            case true:
                break;
            case false:
                switch (ability)
                {
                    case "DoubleJump":
                        onGround = player.GetComponentInChildren<DoubleJump>().landed;
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
                    case "Float":
                        switch (player.GetComponentInChildren<Float>().cooldownTimer.finish)
                        {
                            case false:
                                timeText.gameObject.SetActive(true);
                                timeText.text = (player.GetComponentInChildren<Float>().cooldownTimer.timeRemaining).ToString("F1");
                                cooldownImage.fillAmount = player.GetComponentInChildren<Float>().cooldownTimer.timeRemaining / cooldownTime;
                                break;
                            case true:
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
                break;
        }        

        timer.Update();   
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
                key = GameManager.GM.MeleeAbility;
                break;
            case "Counter":
                cooldownTime = cooldowns[1];
                key = GameManager.GM.CounterAbility;
                break;
            case "Fireball":
                cooldownTime = cooldowns[2];
                key = GameManager.GM.FireBallAbility;
                break;
            case "Dash":
                cooldownTime = cooldowns[3];
                key = GameManager.GM.DashAbility;
                break;
            case "DoubleJump":
                cooldownTime = cooldowns[4];
                key = KeyCode.Space;
                break;
            case "Float":
                cooldownTime = cooldowns[5];
                key = GameManager.GM.FloatAbility;
                break;
        }
        abilityText.text = key.ToString();
    }

    private void DisableCooldowns()
    {
        switch (swapClass.currentClass)
        {
            case SwapClass.playerClasses.Angel:
                switch (ability)
                {
                    case "Melee":
                        disable = true;
                        cooldownImage.fillAmount = 1f;
                        timeText.gameObject.SetActive(true);
                        timeText.text = disabled;
                        abilityText.text = disabled;
                        break;
                    case "Counter":
                        disable = true;
                        cooldownImage.fillAmount = 1f;
                        timeText.gameObject.SetActive(true);
                        timeText.text = disabled;
                        abilityText.text = disabled;
                        break;
                    case "Fireball":
                        disable = true;
                        cooldownImage.fillAmount = 1f;
                        timeText.gameObject.SetActive(true);
                        timeText.text = disabled;
                        abilityText.text = disabled;
                        break;
                    case "Dash":
                        disable = false;
                        cooldownImage.fillAmount = 0f;
                        key = GameManager.GM.DashAbility;
                        abilityText.text = key.ToString();
                        break;
                    case "DoubleJump":
                        disable = false;
                        cooldownImage.fillAmount = 0f;
                        abilityText.text = KeyCode.Space.ToString();
                        break;
                    case "Float":
                        disable = false;
                        cooldownImage.fillAmount = 0f;
                        key = GameManager.GM.FloatAbility;
                        abilityText.text = key.ToString();
                        break;
                }
                break;
            case SwapClass.playerClasses.Devil:
                switch (ability)
                {
                    case "Melee":
                        disable = false;
                        cooldownImage.fillAmount = 0f;
                        key = GameManager.GM.MeleeAbility;
                        abilityText.text = key.ToString();
                        break;
                    case "Counter":
                        disable = false;
                        cooldownImage.fillAmount = 0f;
                        key = GameManager.GM.CounterAbility;
                        abilityText.text = key.ToString();
                        break;
                    case "Fireball":
                        disable = false;
                        cooldownImage.fillAmount = 0f;
                        key = GameManager.GM.FireBallAbility;
                        abilityText.text = key.ToString();
                        break;
                    case "Dash":
                        disable = true;
                        cooldownImage.fillAmount = 1f;
                        timeText.gameObject.SetActive(true);
                        timeText.text = disabled;
                        abilityText.text = disabled;
                        break;
                    case "DoubleJump":
                        disable = true;
                        cooldownImage.fillAmount = 1f;
                        timeText.gameObject.SetActive(true);
                        abilityText.text = disabled;
                        break;
                    case "Float":
                        disable = true;
                        cooldownImage.fillAmount = 1f;
                        timeText.gameObject.SetActive(true);
                        timeText.text = disabled;
                        abilityText.text = disabled;
                        break;
                }
                break;
        }
    }
}
