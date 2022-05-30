using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CTableMusicBoxInteract : MonoBehaviour
{
    #region
    [Header("Interact GameObject")]
    public GameObject textUI = null;
    public GameObject playerMusicBoxObject = null;
    public GameObject player = null;
    #endregion

    public void setIsNearMusicBox(bool state)
    {
        isNear = state;
    }

    private GameObject musicBoxSound;
    private static bool isNear = false;

    void Start()
    {
        
    }

    void Update()
    {
        if (isNear)
        {
            textUI.SetActive(true);

            if (Input.GetKeyDown(KeyCode.E))
            {
                //아니면 오르골을 캐릭터 자식으로 받고있다가 setActive 켜주기?
                playerMusicBoxObject.SetActive(true);

                //사운드 재생 및 애니메이션 작동
                GetComponent<FMODUnity.StudioEventEmitter>().Play();

                Destroy(this.gameObject);

                isNear = false;


                //textUI.SetActive(false);
                //rayHitArr[i].collider.gameObject.SetActive(false);
                //GetComponent<FMODUnity.StudioEventEmitter>().Play();

                //playerMusicBoxObject.GetComponent<Animator>().SetTrigger("aFirst");
            }
        }
        else
        {
            textUI.SetActive(false);
        }
    }
}
