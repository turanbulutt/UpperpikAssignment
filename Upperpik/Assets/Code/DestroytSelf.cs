using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class DestroytSelf : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        DestroyAfter3Sec();
    }

    //it should destroy it self after three second in order to save the memory but that script is not showing in the game.
    private void DestroyAfter3Sec()
    {
        Destroy(this,3);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
