using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class CameraDeath : MonoBehaviour
{
    [SerializeField] Vector2 deathLimitsY;
    [SerializeField] int damage;

    GameObject player;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player");
    }

    // Update is called once per frame
    void Update()
    {
        //Checks if the player is visible from the main camera
        if (!Visible(player) && player.GetComponent<PlayerDeath>().waitTimer.finish) 
        {
            GetComponent<PlayerHealth>().health = 0;
        }
        
    }

    //Gets all playens and checks if all the colliders form the children of the object are visible in said planes
    private bool Visible(GameObject Object)
    {
        Plane[] planes = GeometryUtility.CalculateFrustumPlanes(Camera.main);
        if (GeometryUtility.TestPlanesAABB(planes, Object.GetComponentInChildren<Collider>().bounds))
            return true;
        else
            return false;
    }
}
