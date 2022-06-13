using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class CComponent : MonoBehaviour
{
    #region
    //내부적으로 쓸거라서 숨겨둠
    [HideInInspector] public Transform m_oTransform = null;
    [HideInInspector] public Rigidbody m_oRigidbody = null;
    [HideInInspector] public Rigidbody2D m_oRigidbody2D = null; //단면체크용

    #endregion
    //쓰고싶은 기능을 여기다가 추가한다
    //사용할 준비로 나가야해서 NUll로 나가야함
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
