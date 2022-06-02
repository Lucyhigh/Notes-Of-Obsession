using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FMODUnity;

public class SoundTest_A : MonoBehaviour
{
 
    StudioEventEmitter musicBoxSound;

    void Awake()
    {
        musicBoxSound = GetComponent<StudioEventEmitter>();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            SetSound_B();
        }

        if (Input.GetKeyDown(KeyCode.Q))
        {
            SetSound_B();
        }

    }

    public void SetSound_A()
    {
        musicBoxSound.Event = "snapshot:/Snapshot3";
        musicBoxSound.Play();
    }

    public void SetSound_B()
    {
        musicBoxSound.Event = "snapshot:/Snapshot2";
        musicBoxSound.Play();
    }

}
