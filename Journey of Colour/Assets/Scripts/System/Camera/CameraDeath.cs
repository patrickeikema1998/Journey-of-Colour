using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class CameraDeath : MonoBehaviour
{
    [SerializeField] Vector2 playerSize, deathLimitsY;
    [SerializeField] int damage;
    [SerializeField] float firstTimeCheck, secondTimeCheck;

    GameObject player;
    float corrector = 2f;
    Rect screenSize;
    Vector2 screenSizeCalc;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player");
        screenSize = GetComponent<Camera>().pixelRect;
        screenSizeCalc = GetComponent<Camera>().WorldToViewportPoint(new Vector3(screenSize.width, screenSize.height, 0f)); 
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 currentCameraPos;
        currentCameraPos = GetComponent<Camera>().ViewportToWorldPoint(new Vector3(0.5f, 0.5f, 0.5f));

        if (player.transform.position.x < (currentCameraPos.x - screenSizeCalc.x / 2) - (playerSize.x * corrector)
            || player.transform.position.x > (currentCameraPos.x + screenSizeCalc.x /2) + (playerSize.x * corrector))
        {
            player.GetComponent<Health>().health = 0;
        }

        if (!player.GetComponent<PlayerMovement>().isGrounded)
        {
            if (player.transform.position.y < deathLimitsY.x || player.transform.position.y > deathLimitsY.y)
            {
                player.GetComponent<Health>().health -= damage;
            }
        }
    }
}
