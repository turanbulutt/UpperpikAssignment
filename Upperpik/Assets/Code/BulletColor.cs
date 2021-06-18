using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class BulletColor : MonoBehaviour
{
    void Start()
    {
        //setting bullet color to red
        gameObject.GetComponent<MeshRenderer>().material.color = Color.red;
    }
    /*
    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("CAlled");
        Arm myArm = (Arm)other.GetComponent(typeof(Arm));
        myArm.IncreaseBullet();
        Destroy(gameObject);
    }*/
}
