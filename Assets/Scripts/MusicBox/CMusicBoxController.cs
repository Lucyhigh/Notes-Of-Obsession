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
    //����׷� �÷��̾� �ְ� Ȯ�� �ȵ����� �÷��̾ �ݶ��̴� �ֱ�

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("����");
            isNear = true;
        }
    }
}
