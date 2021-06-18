using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TargetScript : MonoBehaviour
{
    [SerializeField] int health = 3;
    [SerializeField] int delay = 2;

    private void Start()
    {
        RandomLocation();
    }

    private void RandomLocation()
    {
        //assign the random location for target
        float xLoc = UnityEngine.Random.Range(-2f, 1f);
        float yLoc = UnityEngine.Random.Range(0.5f, 3f);
        transform.position = new Vector3(xLoc, yLoc, transform.position.z);

    }

    private void OnTriggerEnter(Collider other)
    {
        health--;
        if (health == 0)
        {
            //after 3 success hit target moving back slowly and 2 seconds later scene starts over.
            this.GetComponent<Rigidbody>().velocity = new Vector3(0, 0, 2f);
            StartCoroutine(LoadLevelAfterDelay());
        }
    }

    IEnumerator LoadLevelAfterDelay()
    {
        yield return new WaitForSeconds(delay);
        SceneManager.LoadScene(0);
    }
}
