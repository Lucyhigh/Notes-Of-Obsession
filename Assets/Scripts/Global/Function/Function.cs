using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//전역 '함수'
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
    //! 비동기 작업을 대기한다.
    public static IEnumerator WaitAsyncOperation(AsyncOperation a_oAsyncOperation,
        System.Action<AsyncOperation> a_oCallBack)
    {
        while(! a_oAsyncOperation.isDone)
        {
            yield return new WaitForEndOfFrame();
            a_oCallBack?.Invoke(a_oAsyncOperation);
        }
    }

    //! 함수를 지연 호출한다.
    private static IEnumerator DOLateCall(System.Action<object[]> a_oCallBack,
        float a_fDelay, object[] a_oParams)//딜레이타임
    {
        yield return new WaitForSeconds(a_fDelay);

        a_oCallBack?.Invoke(a_oParams);
    }
}
