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
                //�ƴϸ� �������� ĳ���� �ڽ����� �ް��ִٰ� setActive ���ֱ�?
                playerMusicBoxObject.SetActive(true);

                //���� ��� �� �ִϸ��̼� �۵�
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
