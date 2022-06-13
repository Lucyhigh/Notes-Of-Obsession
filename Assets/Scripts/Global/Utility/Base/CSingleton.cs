using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class CSingleton<T> : CComponent where T : CComponent
{
    private static T m_oInstance = null;

    //인스턴스 프로퍼티
    public static T Instance
    {
        get
        {
            if(m_oInstance == null)
            {
                var oGameObject = new GameObject(typeof(T).ToString());
                m_oInstance = Function.AddComponent<T>(oGameObject);

                DontDestroyOnLoad(oGameObject);
            }
            return m_oInstance;
        }
    }

    public static T Create()
    {
        return CSingleton<T>.Instance;
    }
}
