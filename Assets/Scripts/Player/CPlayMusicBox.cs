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

public class CPlayMusicBox : CComponent
{
    #region
    public GameObject musicBoxObject = null;
    #endregion

    public event EventHandler m_oEventHandler = null;
    [HideInInspector] public delegate void EventHandler(string a_oString);

    Transform playerModel;
    Animator playerAnimator;
    Animator musicBoxAnimator;

    private CEventDispatcher<string> oEventDispatcher = null;

    //���� ���⼭ ���׸� ���̰� Ȯ��޼��� ���� ���� ���ϰ� �� �� �ִ�~
    private void Awake()
    {
        //oEventDispatcher = new CEventDispatcher<string>();
        //oEventDispatcher.m_oEventHandler += PrintString;

        //var a = new CEventDispatcher<string>.A();
        //oEventDispatcher.Example(a);
        //var b = new CEventDispatcher<string>.B();
        //oEventDispatcher.Example(b);//��Ӱ����� B�� ����

        //m_oEventHandler = null;
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
            musicBoxAnimator.SetBool("aPlay", true);

            //WaitAsyncAnimation();
            //playerAnimator.SetTrigger("aPlaying");
            //oEventDispatcher.DispatchEvent("���ߴ�");
        }
        else
        {
            playerAnimator.SetBool("aPlay", false);
            musicBoxAnimator.SetBool("aPlay", false);
        }

        if (Input.GetKeyDown(KeyCode.Mouse1))
        {
            //playerAnimator.SetTrigger("aMouseRight");
            //musicBoxAnimator.SetTrigger("aMouseRight");
            StartCoroutine(testCoroutine());
        }
    }

    void RotateAnimatorMusicBox()
    {

    }

    //public class CEventDispatcher ���� ���� Ŭ�����ȿ��� �Լ����׸� ��밡��
    //Ŭ���� ���׸�
   
    /*���׸� ����
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
    
    public static IEnumerator WaitAsyncAnimation(AsyncOperation a_oAsyncOperation,//����
    System.Action<AsyncOperation> a_oCallBack)//������?
    {
        while (!a_oAsyncOperation.isDone)
        {
            yield return new WaitForEndOfFrame();
            a_oCallBack?.Invoke(a_oAsyncOperation);
        }
    }

    private IEnumerator testCoroutine()
    {
        bool isTest =true;
        while (isTest)
        {
            playerAnimator.SetTrigger("aMouseRight");
            musicBoxAnimator.SetTrigger("aMouseRight");

            Debug.Log("testCoroutine");

            yield return new WaitForEndOfFrame();
            break;
        }
        isTest = false; 
        yield break;
    }

}