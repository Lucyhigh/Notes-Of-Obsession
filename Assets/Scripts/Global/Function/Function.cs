using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//���� '�Լ�'
public class Function : MonoBehaviour
{
    public static T FindComponent<T>(string a_oName) where T : Component
    {
        var oGameObject = GameObject.Find(a_oName);

        return oGameObject?.GetComponentInChildren<T>();
    }
    
    public static T AddComponent<T>(GameObject a_oGameObject) where T : Component
    {
        var oComponent = a_oGameObject.GetComponent<T>();

        if(oComponent == null)
        {
            oComponent = a_oGameObject.AddComponent<T>();
        }

        return oComponent;
    }

    //AsyncOperation
    //! �񵿱� �۾��� ����Ѵ�.
    public static IEnumerator WaitAsyncOperation(AsyncOperation a_oAsyncOperation,
        System.Action<AsyncOperation> a_oCallBack)
    {
        while(! a_oAsyncOperation.isDone)
        {
            yield return new WaitForEndOfFrame();
            a_oCallBack?.Invoke(a_oAsyncOperation);
        }
    }

    //! �Լ��� ���� ȣ���Ѵ�.
    private static IEnumerator DOLateCall(System.Action<object[]> a_oCallBack,
        float a_fDelay, object[] a_oParams)//������Ÿ��
    {
        yield return new WaitForSeconds(a_fDelay);

        a_oCallBack?.Invoke(a_oParams);
    }
}
