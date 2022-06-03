using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum HandAction
{
    IDLE,
    READY,
    PLAY,
    END
}

public class CPlayMusicBox : MonoBehaviour
{
    #region
    public GameObject musicBoxObject = null;

    #endregion
    Transform playerModel;
    Animator playerAnimator;
    Animator musicBoxAnimator;

    void Start()
    {
        playerModel = transform.GetChild(0);
        playerAnimator = playerModel.GetComponent<Animator>();
        musicBoxAnimator = musicBoxObject.GetComponent<Animator>();
    }

    void Update()
    {
        if (Input.GetKey(KeyCode.Mouse0))
        {
            playerAnimator.SetBool("aPlay", true);
        }
        else
        {
            playerAnimator.SetBool("aPlay", false);
        }

        if (Input.GetKeyDown(KeyCode.Mouse1))
        {
            playerAnimator.SetTrigger("aMouseRight");
        }
    }
}
