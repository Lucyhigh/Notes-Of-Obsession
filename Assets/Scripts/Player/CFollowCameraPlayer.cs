using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CFollowCameraPlayer : MonoBehaviour
{
    #region public º¯¼ö
    public float moveSpeed = 2.0f;
    public float mouseSensitivity = 2.0f;
    #endregion

    Transform oTransform;
    Transform playerModel;
    Transform cameraTransform;
    Transform cameraParentTransform;

    CharacterController playerController;
    Animator playerAnimator;

    Vector3 move;
    Vector3 mouseMove;
    void Awake()
    {
        
    }
    void Start()
    {
        
    }

    void Update()
    {
        
    }
}
