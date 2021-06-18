using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arm : MonoBehaviour
{
    [SerializeField] float moveSpeedX = 2f;
    [SerializeField] float moveSpeedZ = 3f;
    [SerializeField] GameObject finishLine;
    [SerializeField] GameObject platform;
    [SerializeField] GameObject earnedBullet;
    [SerializeField] Camera cam;
    [SerializeField] GameObject bulletPrefab;
    [SerializeField] float projectileFirePeriod = 0.1f;

    float xMin, xMax, zMax;
    float numberOfBullet = 0, verticalLookRotation, horizontalLookRotation;
    private Coroutine firingCoroutine;
    // Start is called before the first frame update
    void Start()
    {
        SetUpMoveBoundaries();
    }

    // Update is called once per frame
    void Update()
    {
        Move();
        //if we are in the finish line, that means we can rotate the camera and fire.
        if (this.transform.position.z == zMax)
        {
            RotateCamera();
            if (Input.GetMouseButtonDown(0))
            {
                firingCoroutine = StartCoroutine(FireContinuously());
            }
            if (Input.GetButtonUp("Fire1"))
                StopCoroutine(firingCoroutine);
        }

    }

    //shifting arm horizontal according to input and variables. Arm moves forward constantly until the finish line which is zMax.
    private void Move()
    {
        float deltaX = Input.GetAxis("Horizontal");
        float newXPos = transform.position.x + deltaX * Time.deltaTime * moveSpeedX;
        if (newXPos > xMax)
            newXPos = xMax;
        else if (newXPos < xMin)
            newXPos = xMin;

        float newZPos = transform.position.z + Time.deltaTime * moveSpeedZ;

        if (newZPos > zMax)
        {
            newZPos = zMax;
            newXPos = transform.position.x;

        }

        cam.transform.position = new Vector3(transform.position.x, transform.position.y + 0.8f, transform.position.z - 1);
        transform.position = new Vector3(newXPos, transform.position.y, newZPos);
    }

    private IEnumerator FireContinuously()
    {
        //it should take middle of the screen and shoot that direction while we have bullet but somethings are wrong couldn't solve that
        while (numberOfBullet > 0)
        {
            float x = Screen.width / 2;
            float y = Screen.height / 2;
            Ray ray = cam.ViewportPointToRay(new Vector3(x, y, 0));
            GameObject laser = Instantiate(bulletPrefab, transform.position, Quaternion.identity) as GameObject;
            laser.GetComponent<Rigidbody>().velocity = ray.direction * 20;
            DeleteEarnedBullet();
            numberOfBullet--;
            yield return new WaitForSeconds(projectileFirePeriod);
        }
    }

    //delete shooted bullet which is end of the bullets list
    private void DeleteEarnedBullet()
    {
        GameObject[] bullets = GameObject.FindGameObjectsWithTag("EarnedBullet");
        Destroy(bullets[bullets.Length - 1]);
    }

    //rotating camera with some borders
    private void RotateCamera()
    {
        horizontalLookRotation += Input.GetAxisRaw("Mouse X");
        verticalLookRotation -= Input.GetAxisRaw("Mouse Y");
        horizontalLookRotation = Mathf.Clamp(horizontalLookRotation, -42f, 40f);
        verticalLookRotation = Mathf.Clamp(verticalLookRotation, -30f, 14f);
        cam.transform.localEulerAngles = new Vector3(verticalLookRotation, horizontalLookRotation, 0);
    }

    //setting move boundaries for the arm
    private void SetUpMoveBoundaries()
    {
        xMax = platform.GetComponent<BoxCollider>().bounds.max.x;
        xMin = platform.GetComponent<BoxCollider>().bounds.min.x;
        zMax = finishLine.transform.position.z - finishLine.GetComponent<BoxCollider>().size.z - (finishLine.GetComponent<BoxCollider>().center.z);
    }

    //when we have trigger before finish line that means we earn some bullet
    //the reason that I put if statement is when i fire bullet goes inside of the arm and trigger that function again and that makes
    //infinite bullet for us. Probably that problem happens the way i shoot the bullet which is wrong but i solved that problem in that way for now. 
    private void OnTriggerEnter(Collider other)
    {
        if (!(transform.position.z == zMax))
        {
            numberOfBullet++;
            Destroy(other);
            CreateEarnedBullet();
        }

    }

    //creating bullet near of the arm. We need to shift bullet little bit left because of existing of other earned bullets.
    private void CreateEarnedBullet()
    {
        Vector3 newEarnedBullet;
        float shiftedX = 0.17f + (numberOfBullet - 1.0f) * 0.08f;
        newEarnedBullet = new Vector3(transform.position.x - shiftedX, transform.position.y, transform.position.z + 0.5f);

        var newBullet = Instantiate(earnedBullet, newEarnedBullet, Quaternion.identity);
    }

}
