using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

//퍼포먼스에서 가장 중요한 클래스!
//객체지향 은닉화 시켜야한다~게임노드와 메인게임과 유사성이 잇음
//씬을 불러올 준비가 되었다
public class CSceneLoader : CSingleton<CSceneLoader>
{
    public void LoadScene(int a_nIndex)
    {
        string oScenePath = SceneUtility.GetScenePathByBuildIndex(a_nIndex);
        this.LoadScene(oScenePath);
    }

    public void LoadScene(string a_oName)
    {
        SceneManager.LoadScene(a_oName, LoadSceneMode.Single);
    }

    public void LoadSceneAsync(int a_nIndex,
        System.Action<AsyncOperation> a_oCallBack,
        float a_fDelay = 0.0f,
        LoadSceneMode a_eLoadSceneMode = LoadSceneMode.Single)
    {
        string oScenePath = SceneUtility.GetScenePathByBuildIndex(a_nIndex);

        this.LoadSceneAsync(oScenePath, a_oCallBack, a_fDelay, a_eLoadSceneMode);
    }

    public void LoadSceneAsync(string a_oName,
    System.Action<AsyncOperation> a_oCallBack,
    float a_fDelay = 0.0f,
    LoadSceneMode a_eLoadSceneMode = LoadSceneMode.Single)
    {
        //원할때 씬을 준비시킨다.
        StartCoroutine(this.DoLoadSceneAsync(a_oName, a_oCallBack, a_fDelay, a_eLoadSceneMode));
    }

    private IEnumerator DoLoadSceneAsync(string a_oName,
    System.Action<AsyncOperation> a_oCallBack,
    float a_fDelay,
    LoadSceneMode a_eLoadSceneMode)
    {
        yield return new WaitForSeconds(a_fDelay);

        var oAsyncOperation = SceneManager.LoadSceneAsync(a_oName, a_eLoadSceneMode);

        yield return Function.WaitAsyncOperation(oAsyncOperation, a_oCallBack);
    }
}
