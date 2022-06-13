using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CSceneManager : CComponent
{
    #region public ����
    public float m_fPlaneDistance = KDefine.DEFAULT_PLANE_DISTANCE;
    #endregion
    
    //! UI ī�޶� ������Ƽ
    public static Camera UICamera
    {
        get
        {
            return Function.FindComponent<Camera>(KDefine.NAME_UI_CAMERA);
        }
    }

    //! ���� ī�޶� ������Ƽ
    public static Camera MainCamera
    {
        get
        {
            return Function.FindComponent<Camera>(KDefine.NAME_MAIN_CAMERA);
        }
    }

    //! UI ��Ʈ ������Ƽ
    public static GameObject UIRoot
    {
        get
        {
            return GameObject.Find(KDefine.NAME_UI_ROOT);
        }
    }

    //! ��ü ��Ʈ ������Ƽ
    public static GameObject ObjectRoot
    {
        get
        {
            return GameObject.Find(KDefine.NAME_OBJECT_ROOT);
        }
    }

    //! ���� �� ������ ������Ƽ
    public static GameObject CurrentSceneManager
    {
        get
        {
            return GameObject.Find(KDefine.NAME_SCENE_MANAGER);
        }
    }

    //�ʱ�ȭ
    public override void Start()
    {
        base.Awake();

        this.SetupUICamera();
        this.SetupMainCamera();

        //����� ���ļ��� ���� ������ �����ս��� �����Ѵ� -> Ƽ� ���� ����
        QualitySettings.vSyncCount = 0;
        Application.targetFrameRate = 60;

        Screen.SetResolution(KDefine.SCREEN_WIDTH, KDefine.SCREEN_HEIGHT, false);
    }

    public override void Update()
    {
        
    }

    //UI ī�޶� ����
    protected void SetupMainCamera()
    {
        if(CSceneManager.UICamera != null)
        {
            CSceneManager.UICamera.orthographic = true;
            CSceneManager.UICamera.orthographicSize = (KDefine.SCREEN_HEIGHT / 2.0f) * KDefine.UNIT_SCALE;
        }
    }

    //���� ī�޶� ����
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
