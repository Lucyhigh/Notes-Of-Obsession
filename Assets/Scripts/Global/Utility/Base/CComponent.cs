using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class CComponent : MonoBehaviour
{
    #region
    //���������� ���Ŷ� ���ܵ�
    [HideInInspector] public Transform m_oTransform = null;
    [HideInInspector] public Rigidbody m_oRigidbody = null;
    [HideInInspector] public Rigidbody2D m_oRigidbody2D = null; //�ܸ�üũ��

    #endregion
    //������� ����� ����ٰ� �߰��Ѵ�
    //����� �غ�� �������ؼ� NUll�� ��������
    public virtual void Awake()
    {
        m_oTransform = this.transform;
        m_oRigidbody = this.GetComponentInChildren<Rigidbody>();
        m_oRigidbody2D = this.GetComponentInChildren<Rigidbody2D>();
    }

    public virtual void Start()
    {
        //Do Nothing
    }

    public virtual void Update()
    {
        //Do Nothing
    }

    public virtual void LateUpdate()
    {
        //Do Nothing
    }
}
