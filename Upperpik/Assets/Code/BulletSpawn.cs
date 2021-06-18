using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletSpawn : MonoBehaviour
{

    [SerializeField] int minBullet = 5;
    [SerializeField] int maxBullet = 20;
    [SerializeField] GameObject arm;
    [SerializeField] GameObject platform;
    [SerializeField] GameObject finishLine;
    [SerializeField] GameObject bulletObject;
    // Start is called before the first frame update
    void Start()
    {
        BulletSpawner();
    }

    // spawn random number of bullet between platform borders and trying to not have clash with putting all locations of bullets a list.
    private void BulletSpawner()
    {
        float yLocation = arm.transform.position.y;
        float xMax = platform.GetComponent<BoxCollider>().bounds.max.x;
        float xMin = platform.GetComponent<BoxCollider>().bounds.min.x;
        float zMax = finishLine.GetComponent<BoxCollider>().bounds.min.z - 1;
        float zMin = platform.GetComponent<BoxCollider>().bounds.min.z;
        int numberOfBullet = (int)UnityEngine.Random.Range(minBullet, maxBullet);
        Vector3 newBullet;
        List<Vector3> bullets = new List<Vector3>();
        for (int i = 0; i < numberOfBullet; i++)
        {
            do
            {
                newBullet = new Vector3(UnityEngine.Random.Range(xMin, xMax), yLocation, UnityEngine.Random.Range(zMin, zMax));
            }
            while (bullets.Contains(newBullet));
            bullets.Add(newBullet);
            var newBulletSpawn = Instantiate(bulletObject, newBullet, Quaternion.identity);
        }
    }
}
