using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FMODUnity;

interface interact
{
     void Play();
}

public class CTableMusicBoxInteract : MonoBehaviour, interact
{
    #region
    [Header("Interact GameObject")]
    public GameObject textUI = null;
    public GameObject playerMusicBoxObject = null;
    public GameObject player = null;
    #endregion

    GameObject musicBoxSound;
    Animator playerMusicBoxAnimator;
    Animator playerAnimator;

    private bool isNear = false;

    void Awake()
    {
        musicBoxSound = transform.GetChild(2).gameObject;
        playerMusicBoxAnimator = playerMusicBoxObject.GetComponent<Animator>();
        playerAnimator = player.GetComponent<Animator>();

    }

    void Update()
    {
        if (isNear)
        {
            textUI.SetActive(true);
            //애니메이션 맞추기 용으로 잠시 제거
            //if (Input.GetKeyDown(KeyCode.E))
            //{
                playerMusicBoxAnimator.SetTrigger("aFirst");
                playerAnimator.SetTrigger("aFirst");

                //musicBoxSound.GetComponent<StudioEventEmitter>().Play();

                //isNear = false;
               // Destroy(this.gameObject);
            //}
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

    public void Play()
    {
        openMusicBox();
    }
}
