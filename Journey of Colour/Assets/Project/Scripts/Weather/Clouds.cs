using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Clouds : MonoBehaviour
{

    [SerializeField] float startPosX, maxPosX, moveSpeed;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        MoveAndResetPos();
    }

    void MoveAndResetPos()
    {
        for(int i = 0; i < transform.childCount; i++)
        {
            Transform cloudTransform = this.gameObject.transform.GetChild(i);
            //moves clouds
            cloudTransform.position = new Vector3(cloudTransform.position.x + moveSpeed, cloudTransform.position.y, cloudTransform.position.z);

            if(cloudTransform.position.x > maxPosX)
            {
                //resets cloud X pos
                cloudTransform.position = new Vector3(startPosX, cloudTransform.position.y, cloudTransform.position.z);
            }
        }
    }
}
