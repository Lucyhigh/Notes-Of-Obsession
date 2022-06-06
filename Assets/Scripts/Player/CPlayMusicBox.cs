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

    private CEventDispatcher<string> oEventDispatcher = null;

    //���� ���⼭ ���׸� ���̰� Ȯ��޼��� ���� ���� ���ϰ� �� �� �ִ�~
    private void Awake()
    {
        oEventDispatcher = new CEventDispatcher<string>();
        oEventDispatcher.m_oEventHandler += PrintString;

        var a = new CEventDispatcher<string>.A();
        oEventDispatcher.Example(a);
        var b = new CEventDispatcher<string>.B();
        oEventDispatcher.Example(b);//��Ӱ����� B�� ����
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

    //public class CEventDispatcher ���� ���� �Լ����׸� ��밡��
    //Ŭ���� ���׸�
    public class CEventDispatcher<T> 
    //public class CEventDispatcher<T> where T : class
        //string �� Ŭ�����ϱ� string ��� class�� T ���� ����
        //int float ���� struct

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
       // public delegate void EventHandler(T a_oString);
        public delegate void EventHandler(T a_oString);
        //�� ������ ���� ���� ���¸� �����ٶ�

        //�ʱ�ȭ
        public void Init()
        {
            m_oEventHandler = null;
        }

        public class A
        {
            public int i;
            public virtual void Print() { Debug.Log("A"); }
        }
        public class B : A {
            public override void Print() { Debug.Log("B"); }
        }
        public class C : A {
            public override void Print() { Debug.Log("C"); }
        }

        //�Լ� ���׸�
        public void Example<K>(K param) where K : A 
        {
            param.Print();
        }

        //�̺�Ʈ�� ȣ���Ѵ�.
        public void DispatchEvent(T a_oEvent)
        {
            //null üũ
            //null ���� �����ڸ� ����Ͽ� ������κ��� ������ ������� �븮�ڸ� ȣ���Ѵ�.
            //���� ������ �� ���°� ���� ��Ʈ�� �������ִ�.
            m_oEventHandler?.Invoke(a_oEvent);
        }
    }
    /*
     public class CArrayList<T> where T : struct
    {
        private T[] m_oValue = null;

        //������
        public CArrayList(int a_nSizw)
        {
            m_oValue = new T[a_nSizw];
        }

        public T this[int a_nIndex]
        {
            get
            {
                return m_oValue[a_nIndex];
            }set
            {
                m_oValue[a_nIndex] = value;
            }
        }
    }
     */


    private void PrintString(string a_oString)
    {
        Debug.LogFormat("PrintString : {0}", a_oString);

    }
}