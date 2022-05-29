using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CMusicBoxController : MonoBehaviour
{
    #region
    #endregion
    private bool isNear = false;

    void Start()
    {

    }

    void Update()
    {
        if (isNear && Input.GetKeyDown(KeyCode.E))
        {
            Destroy(this.gameObject);
            isNear = false;

        }
    }
    //디버그로 플레이어 넣고 확인 안들어오면 플레이어에 콜라이더 넣기

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("들어옴");
            isNear = true;
        }
    }
}
