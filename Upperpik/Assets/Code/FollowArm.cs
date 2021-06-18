using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//this script for earnedbullet which follows arm  near of them
public class FollowArm : MonoBehaviour
{
    GameObject Arm;
    float differencesX, differencesZ = 0.5f;
    void Start()
    {
        Arm = GameObject.Find("Arm_with_100_bones");
        differencesX = Arm.transform.position.x - transform.position.x;
    }
    // Update is called once per frame
    void Update()
    {
        Move();
    }

    private void Move()
    {
        transform.position = new Vector3(Arm.transform.position.x - differencesX, Arm.transform.position.y, Arm.transform.position.z + differencesZ);
    }
}
