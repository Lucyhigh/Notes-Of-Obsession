using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CSceneManager : CComponent
{
    #region public 변수
    public float m_fPlaneDistance = KDefine.DEFAULT_PLANE_DISTANCE;
    #endregion
    
    //! UI 카메라 프로퍼티
    public static Camera UICamera
    {
        get
        {
            return Function.FindComponent<Camera>(KDefine.NAME_UI_CAMERA);
        }
    }

    //! 메인 카메라 프로퍼티
    public static Camera MainCamera
    {
        get
        {
            return Function.FindComponent<Camera>(KDefine.NAME_MAIN_CAMERA);
        }
    }

    //! UI 루트 프로퍼티
    public static GameObject UIRoot
    {
        get
        {
            return GameObject.Find(KDefine.NAME_UI_ROOT);
        }
    }

    //! 객체 루트 프로퍼티
    public static GameObject ObjectRoot
    {
        get
        {
            return GameObject.Find(KDefine.NAME_OBJECT_ROOT);
        }
    }

    //! 현재 씬 관리자 프로퍼티
    public static GameObject CurrentSceneManager
    {
        get
        {
            return GameObject.Find(KDefine.NAME_SCENE_MANAGER);
        }
    }

    //초기화
    public override void Start()
    {
        base.Awake();

        this.SetupUICamera();
        this.SetupMainCamera();

        //모니터 주파수에 맞춰 렌더링 퍼포먼스를 조절한다 -> 티어링 현상 방지
        QualitySettings.vSyncCount = 0;
        Application.targetFrameRate = 60;

        Screen.SetResolution(KDefine.SCREEN_WIDTH, KDefine.SCREEN_HEIGHT, false);
    }

    public override void Update()
    {
        
    }

    //UI 카메라 설정
    protected void SetupMainCamera()
    {
        if(CSceneManager.UICamera != null)
        {
            CSceneManager.UICamera.orthographic = true;
            CSceneManager.UICamera.orthographicSize = (KDefine.SCREEN_HEIGHT / 2.0f) * KDefine.UNIT_SCALE;
        }
    }

    //메인 카메라 설정
    protected void SetupUICamera()
    {
        if(CSceneManager.MainCamera != null)
        {
            float fPlaneHeight = (KDefine.SCREEN_HEIGHT / 2.0f) * KDefine.UNIT_SCALE;
            float fFieldofView = Mathf.Atan(fPlaneHeight / m_fPlaneDistance);

            CSceneManager.MainCamera.orthographic = false;
            CSceneManager.MainCamera.fieldOfView = (fFieldofView * 2.0f) / Mathf.Rad2Deg;
        }
    }
}
