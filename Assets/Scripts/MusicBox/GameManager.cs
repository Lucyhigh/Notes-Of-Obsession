using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;



//public class CEventDispatcher<T> where T : class
//string �� Ŭ�����ϱ� string ��� class�� T ���� ����
//int float ���� struct

//�Ӽ� ���� ����Ǹ� �̺�Ʈ�� �߻��ϰ� ó���Ѵ�.
public class CEventDispatcher<T> : MonoBehaviour
{
    //�̺�Ʈ�� ������ ��Ÿ��(Ŭ����)�� ���� �ƺ��� �ʿ��ϴ�
    //������ �̺�Ʈ, �ƺ��� ��������Ʈ�� �ڵ鷯
    //�����ƺ��� ���»��� == null....
    //�������� ���� �ͼ� �ϰŸ��� �ش�(�̺�Ʈ ����)
    //�ϰŸ��� �ҷ��� A������ �ؾ���(�޼ҵ�)
    //������ �ϰŸ��� ���ϸ� �ƺ����� ���� �� �ߴٰ� �ٱ��� ������ ���� �ش޶�� �Ѵ�.
    //�ƺ� �ڵ鷯�� �� �ߴٰ� �˷��ش�
    //��0�� �̺�Ʈ ���� �� �ƺ����� �̺�Ʈ �������ش޶�� �� (�ڵ鷯�� �����Ʈ).. ������ ��ȯ��

    //event Ű���带 ���ؼ� �����ϰ� �̺�Ʈ �븮�ڸ� �����ϸ� �ȴ�.

    //�� ������ ���� ���� ���¸� �����ٶ�

    //�ʱ�ȭ
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

    //�Լ� ���׸�
    public static void Example<K>(K param) where K : A
    {
        param.Print();
    }

    //�̺�Ʈ�� ȣ���Ѵ�.
    //public static void DispatchEvent(this event a_oEvent,delegate a_oHandler)
    //{
    //    //null üũ
    //    //null ���� �����ڸ� ����Ͽ� ������κ��� ������ ������� �븮�ڸ� ȣ���Ѵ�.
    //    //���� ������ �� ���°� ���� ��Ʈ�� �������ִ�.
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
    //���ӳ���� ����
    //������ٵ� ���ø� �޼ҵ� �� �����ڰ� ����...
    //�ڵ�� �����ؼ� �����ϰڴ�..
    //ĸ��ȭ���ѹ����� 

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

        //���̾��Ű���� �ҷ�����
        //a = FindObjectsOfType<ClassA>();
        LStart = new List<IStartable>();

    }

    
    void Start()
    {
        //a.GameStart();
        //������ ��ü���� �̱����Ű�°�~ ������Ƽ ��� -������ٵ� �� ������Ƽ������

        //�𸣰����� ���ӸŴ��� �̱��� ����
    }   //10�� ����

    void Update()
    {
        if (true)
        {
            //MusicBox.GameUpdate();
            //player.GameUpdate();
            //�߻�Ŭ�������ؼ� 
           
        }
    }

   
}
//�������̽�ȭ ���ѹ��� start update
public interface IStartable
{
    void GameStart();
}

interface Iupdateable
{
    void GameUpdate();
}