using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FMODUnity;



public class CTableMusicBoxInteract : MonoBehaviour
{
    #region
    [Header("Interact GameObject")]
    public GameObject textUI = null;
    public GameObject musicBoxObject = null;
    public GameObject player = null;

    [Space(3.0f)]
    [Header("Sound")]
    #endregion

    StudioEventEmitter musicBoxSound;
    Animator musicBoxAnimator;
    Animator playerAnimator;



    private bool isNear = false;

    void Awake()
    {
        musicBoxAnimator = musicBoxObject.GetComponent<Animator>();
        playerAnimator = player.GetComponent<Animator>();
        musicBoxSound = GetComponent<StudioEventEmitter>();

    }

    void Update()
    {
        if (isNear)
        {
            textUI.SetActive(true);
            if (Input.GetKeyDown(KeyCode.E))
            {
                musicBoxAnimator.SetTrigger("aFirst");
                playerAnimator.SetTrigger("aFirst");

                SoundPlay();

                isNear = false;
                Destroy(this.gameObject);
            }
        }
        else
        {
            textUI.SetActive(false);
        }
    }
    public void openMusicBox()
    {
        isNear = true;
    }

    public void SoundPlay()
    {
        //musicBoxSound.Event = "snapshot:/Snapshot3";
        Debug.Log("³ë·¡");

        musicBoxSound.Play();
    }
}
