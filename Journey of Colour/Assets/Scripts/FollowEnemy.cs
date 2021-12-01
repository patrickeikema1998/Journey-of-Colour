using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowEnemy : MonoBehaviour
{
    public GameObject enemy;
    Vector3 offset;

    // Start is called before the first frame update
    private void Start()
    {
        offset = new Vector3(0, 0, -2);
    }

    // Update is called once per frame
    void Update()
    {
        if (enemy != null)
        {
            transform.position = enemy.transform.position + offset;
        }
        else
        {
            Destroy(gameObject);
        }
    }
}
