using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;



//public class CEventDispatcher<T> where T : class
//string 은 클래스니까 string 대신 class로 T 형태 제한
//int float 등은 struct

//속성 값이 변경되면 이벤트를 발생하고 처리한다.
public class CEventDispatcher<T> : MonoBehaviour
{
    //이벤트를 쓰려면 울타리(클래스)와 엄마 아빠가 필요하다
    //엄마는 이벤트, 아빠는 델리게이트인 핸들러
    //엄마아빠도 쉬는상태 == null....
    //엄마에게 상희가 와서 일거리를 준다(이벤트 접수)
    //일거리를 할려면 A동작을 해야함(메소드)
    //엄마가 일거리를 다하면 아빠에게 가서 다 했다고 바깥에 나가서 말좀 해달라고 한다.
    //아빠 핸들러가 다 했다고 알려준다
    //ㅠ0ㅠ 이벤트 접수 후 아빠한테 이벤트 전달좀해달라고 함 (핸들러가 댈리게이트).. 인형이 반환값

    //event 키워드를 통해서 선언하고 이벤트 대리자를 지정하면 된다.

    //ㄴ 위에는 지정 안한 상태면 빨간줄뜸

    //초기화
    public static void Init()
    {
        
    }

    public class A
    {
        public int i;
        public virtual void Print() { Debug.Log("A"); }
    }
    public class B : A
    {
        public override void Print() { Debug.Log("B"); }
    }
    public class C : A
    {
        public override void Print() { Debug.Log("C"); }
    }

    //함수 제네릭
    public static void Example<K>(K param) where K : A
    {
        param.Print();
    }

    //이벤트를 호출한다.
    //public static void DispatchEvent(this event a_oEvent,delegate a_oHandler)
    //{
    //    //null 체크
    //    //null 조건 연산자를 사용하여 스레드로부터 안전한 방식으로 대리자를 호출한다.
    //    //값이 예측할 수 없는게 들어가면 노트북 터질수있다.
    //    //m_oEventHandler?.Invoke(a_oEvent);
    //    a_oEvent?.Invoke(a_oHandler);
    //}
}


static class extendMethod
{
    public static void AddBasicComponent(this GameObject obj)
    {
        obj.AddComponent<Rigidbody>();
        obj.AddComponent<Collider>();
    }
}

public abstract class GameManager : MonoBehaviour
{
    [HideInInspector]
    //게임노드의 역할
    //리지드바디 퍼플릭 메소드 다 때려박고 시작...
    //코드로 접근해서 시작하겠다..
    //캡슐화시켜버리고 

    private static GameManager gameManager = null;
    public static GameManager getInstance { get { return gameManager;  } }

    //public ClassA a { get; set; }
    public IStartable iStartable { get; set; }


    public List<IStartable> LStart { get; set; }
   // public List<ClassA> LStartClassA { get; set; }



    void Awake()
    {
        if (gameManager == null)
        {
            gameManager = this;
        }

        DontDestroyOnLoad(this.gameObject);

        //하이어락키에서 불러오기
        //a = FindObjectsOfType<ClassA>();
        LStart = new List<IStartable>();

    }

    
    void Start()
    {
        //a.GameStart();
        //들어오는 객체들을 싱글톤시키는것~ 프로퍼티 사용 -리지드바디등도 다 프로퍼티가능함

        //모르겟으면 게임매니저 싱글톤 ㄱㄱ
    }   //10줄 쉽다

    void Update()
    {
        if (true)
        {
            //MusicBox.GameUpdate();
            //player.GameUpdate();
            //추상클래스통해서 
           
        }
    }

   
}
//인터페이스화 시켜버림 start update
public interface IStartable
{
    void GameStart();
}

interface Iupdateable
{
    void GameUpdate();
}