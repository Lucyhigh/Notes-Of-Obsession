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

    private CEventDispatcher oEventDispatcher = null;

    //���� ���⼭ ���׸� ���̰� Ȯ��޼��� ���� ���� ���ϰ� �� �� �ִ�~
    private void Awake()
    {
        oEventDispatcher = new CEventDispatcher();
        oEventDispatcher.m_oEventHandler += PrintString;
    }

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
            oEventDispatcher.DispatchEvent("���ߴ�");
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

    void PlayAnimatorMusicBox()
    {

    }

    public class CEventDispatcher
    //�Ӽ� ���� ����Ǹ� �̺�Ʈ�� �߻��ϰ� ó���Ѵ�.
    {
        //�̺�Ʈ�� ������ ��Ÿ��(Ŭ����)�� ���� �ƺ��� �ʿ��ϴ�
        //������ �̺�Ʈ, �ƺ��� ��������Ʈ�� �ڵ鷯
        //�����ƺ��� ���»��� == null....
        //�������� ���� �ͼ� �ϰŸ��� �ش�(�̺�Ʈ ����)
        //�ϰŸ��� �ҷ��� A������ �ؾ���(�޼ҵ�)
        //������ �ϰŸ��� ���ϸ� �ƺ����� ���� �� �ߴٰ� �ٱ��� ������ ���� �ش޶�� �Ѵ�.
        //�ƺ� �ڵ鷯�� �� �ߴٰ� �˷��ش�
        //��0�� �̺�Ʈ ���� �� �ƺ����� �̺�Ʈ �������ش޶�� �� (�ڵ鷯�� �����Ʈ).. ������ ��ȯ��


        public event EventHandler m_oEventHandler = null;
        //public event EventHandler m_oEventHandler = null;
        //event Ű���带 ���ؼ� �����ϰ� �̺�Ʈ �븮�ڸ� �����ϸ� �ȴ�.
        public delegate void EventHandler(string a_oString);
        //�� ������ ���� ���� ���¸� �����ٶ�

        //�ʱ�ȭ
        public void Init()
        {
            m_oEventHandler = null;
        }

        //�̺�Ʈ�� ȣ���Ѵ�.
        public void DispatchEvent(string a_oEvent)
        {
            //null üũ
            //null ���� �����ڸ� ����Ͽ� ������κ��� ������ ������� �븮�ڸ� ȣ���Ѵ�.
            //���� ������ �� ���°� ���� ��Ʈ�� �������ִ�.
            m_oEventHandler?.Invoke(a_oEvent);
        }
    }



    private void PrintString(string a_oString)
    {
        Debug.LogFormat("PrintString : {0}", a_oString);

    }
}