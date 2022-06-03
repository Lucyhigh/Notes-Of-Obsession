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

    //이제 여기서 제네릭 붙이고 확장메서드 만들어서 쓰면 편하게 쓸 수 있다~
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

    public class CEventDispatcher
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
        public delegate void EventHandler(string a_oString);
        //ㄴ 위에는 지정 안한 상태면 빨간줄뜸

        //초기화
        public void Init()
        {
            m_oEventHandler = null;
        }

        //이벤트를 호출한다.
        public void DispatchEvent(string a_oEvent)
        {
            //null 체크
            //null 조건 연산자를 사용하여 스레드로부터 안전한 방식으로 대리자를 호출한다.
            //값이 예측할 수 없는게 들어가면 노트북 터질수있다.
            m_oEventHandler?.Invoke(a_oEvent);
        }
    }



    private void PrintString(string a_oString)
    {
        Debug.LogFormat("PrintString : {0}", a_oString);

    }
}