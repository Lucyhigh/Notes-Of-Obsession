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

    //이제 여기서 제네릭 붙이고 확장메서드 만들어서 쓰면 편하게 쓸 수 있다~
    private void Awake()
    {
        oEventDispatcher = new CEventDispatcher<string>();
        oEventDispatcher.m_oEventHandler += PrintString;

        var a = new CEventDispatcher<string>.A();
        oEventDispatcher.Example(a);
        var b = new CEventDispatcher<string>.B();
        oEventDispatcher.Example(b);//상속관계인 B가 찍힘
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
            oEventDispatcher.DispatchEvent("망했다");
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

    //public class CEventDispatcher 으로 쓰고 함수제네릭 사용가능
    //클래식 제네릭
    public class CEventDispatcher<T> 
    //public class CEventDispatcher<T> where T : class
        //string 은 클래스니까 string 대신 class로 T 형태 제한
        //int float 등은 struct

    //속성 값이 변경되면 이벤트를 발생하고 처리한다.
    {
        //이벤트를 쓰려면 울타리(클래스)와 엄마 아빠가 필요하다
        //엄마는 이벤트, 아빠는 델리게이트인 핸들러
        //엄마아빠도 쉬는상태 == null....
        //엄마에게 상희가 와서 일거리를 준다(이벤트 접수)
        //일거리를 할려면 A동작을 해야함(메소드)
        //엄마가 일거리를 다하면 아빠에게 가서 다 했다고 바깥에 나가서 말좀 해달라고 한다.
        //아빠 핸들러가 다 했다고 알려준다
        //ㅠ0ㅠ 이벤트 접수 후 아빠한테 이벤트 전달좀해달라고 함 (핸들러가 댈리게이트).. 인형이 반환값


        public event EventHandler m_oEventHandler = null;
        //public event EventHandler m_oEventHandler = null;
        //event 키워드를 통해서 선언하고 이벤트 대리자를 지정하면 된다.
       // public delegate void EventHandler(T a_oString);
        public delegate void EventHandler(T a_oString);
        //ㄴ 위에는 지정 안한 상태면 빨간줄뜸

        //초기화
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

        //함수 제네릭
        public void Example<K>(K param) where K : A 
        {
            param.Print();
        }

        //이벤트를 호출한다.
        public void DispatchEvent(T a_oEvent)
        {
            //null 체크
            //null 조건 연산자를 사용하여 스레드로부터 안전한 방식으로 대리자를 호출한다.
            //값이 예측할 수 없는게 들어가면 노트북 터질수있다.
            m_oEventHandler?.Invoke(a_oEvent);
        }
    }
    /*
     public class CArrayList<T> where T : struct
    {
        private T[] m_oValue = null;

        //생성자
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