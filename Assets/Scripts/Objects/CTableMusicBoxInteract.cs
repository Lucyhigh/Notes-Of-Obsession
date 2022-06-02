using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FMODUnity;



public class CTableMusicBoxInteract : MonoBehaviour
{
    #region
    [Header("Interact GameObject")]
    public GameObject textUI = null;
    public GameObject playerMusicBoxObject = null;
    public GameObject player = null;

    [Space(3.0f)]
    [Header("Sound")]
    #endregion

    StudioEventEmitter musicBoxSound;
    //GameObject musicBoxSound;
    Animator playerMusicBoxAnimator;
    Animator playerAnimator;



    private bool isNear = false;

    void Awake()
    {
        //musicBoxSound = transform.GetChild(2).gameObject;
        playerMusicBoxAnimator = playerMusicBoxObject.GetComponent<Animator>();
        playerAnimator = player.GetComponent<Animator>();
        musicBoxSound = GetComponent<StudioEventEmitter>();

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

            Play();

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
        //musicBoxSound.Event = "snapshot:/Snapshot3";
        Debug.Log("노래");

        musicBoxSound.Play();
    }
}
