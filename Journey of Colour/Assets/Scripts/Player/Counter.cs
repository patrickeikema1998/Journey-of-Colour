using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Counter : MonoBehaviour
{
    [SerializeField] float thrownObjectForce, stationaryEnemyForce, counterAreaHeight, playerHeight, fadeTime;
    [SerializeField] float timeToInvisible, timeToVisible;
    private float alphaValue;
    private bool start, finished, deflected;

    private CharacterController controller;
    private CustomTimer fadeTimer;

    private GameObject player;
    private Material[] materials;
    //https://www.youtube.com/watch?v=_w7GU2NIxUE
    //https://answers.unity.com/questions/1100879/push-object-in-opposite-direction-of-collision.html

    private void Start()
    {
        alphaValue = -1;
        start = true;
        finished = false;

        materials = new Material[GetComponentInChildren<Renderer>().materials.Length];
        materials = GetComponentInChildren<Renderer>().materials;
        foreach (Material mat in materials)
        {
            MaterialUtils.SetupBlendMode(mat, MaterialUtils.BlendMode.Transparent);
            mat.SetFloat("_Alpha", alphaValue);
        }

        player = GameObject.FindGameObjectWithTag("Player");
        fadeTimer = new CustomTimer(fadeTime);
    }
    private void Update()
    {
        if (transform != null) 
        {
            transform.position = new Vector3(player.transform.position.x, player.transform.position.y + (counterAreaHeight / 2), player.transform.position.z);
        }

        if (start) 
        {
            if (alphaValue < 0)
            {
                alphaValue += Time.deltaTime * (1 / timeToVisible);
                ChangeVisibility(alphaValue);
            }
            else 
            { 
                start = false;
                fadeTimer.Reset();
            }
        }

        if (fadeTimer.finish)
        {
            if (alphaValue > -1)
            {
                alphaValue -= Time.deltaTime * (1 / timeToInvisible);
                ChangeVisibility(alphaValue);
            }
            else 
            { 
                finished = true;
            }
        }

        if (finished) 
        {
            Destroy(this.gameObject);
        }

        fadeTimer.Update();
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "Enemy" || other.gameObject.tag == "Bullet")
        {
            Redirect(other);
        }
        else { }
    }

    void Redirect(Collider collider)
    {
        GameObject gameObject = collider.gameObject;
        Vector3 dir = new Vector3(
            gameObject.transform.position.x - player.transform.position.x,
            0,
            gameObject.transform.position.z - player.transform.position.z
            ).normalized;

        // If the object we hit is the enemy
        if (gameObject.tag == "Enemy")
        {
            if (gameObject.GetComponent<Rigidbody>() == null)
            {
                controller = gameObject.GetComponent<CharacterController>();
                Debug.Log(controller);
                if (controller != null)
                {
                    gameObject.transform.forward = (new Vector3(
                    dir.x,
                    dir.y,
                    0)
                    );

                    controller.SimpleMove(gameObject.transform.forward * stationaryEnemyForce);
                }
            }
            else
            {
                dir = new Vector3(dir.x, dir.y, 0);

                gameObject.GetComponent<Rigidbody>().AddForce(dir * stationaryEnemyForce, ForceMode.Impulse);
            }

        }

        // If the object we hit is a bullet
        if (gameObject.tag == "Bullet")
        {
            if (gameObject.GetComponent<SpearBehavior>() != null && !deflected) 
            {
                gameObject.GetComponent<SpearBehavior>().deflected = true;
                gameObject.transform.rotation = new Quaternion(
                    gameObject.transform.rotation.x,
                    -gameObject.transform.rotation.y,
                    gameObject.transform.rotation.z,
                    -gameObject.transform.rotation.w
                    );
                deflected = true;
            }
            gameObject.GetComponent<Rigidbody>().AddForce(dir * thrownObjectForce, ForceMode.Impulse);
        }
    }

    void ChangeVisibility(float alpha)
    {
        foreach (Material mat in materials)
        {
            mat.SetFloat("_Alpha", alpha);
        }
    }
}