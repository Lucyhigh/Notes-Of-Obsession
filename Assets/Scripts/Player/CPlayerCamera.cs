using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CPlayerCamera : MonoBehaviour
{
    #region 변수
    public Transform target;

    public float distance = 2.5f;
    public float height = 1.0f;

    public float X_RotateSpeed = 100.0f;
    public float Y_RotateSpeed = 100.0f;

    public float X_MinLimit = 60.0f;
    public float X_MaxLimit = 60.0f;

    public float Y_MinLimit = -20.0f;
    public float Y_MaxLimit = 45.0f;
    #endregion

    private float x = 0.0f;     //카메라 각도 x
    private float y = 0.0f;     //카메라 각도 y
    
    float ClampAngle(float angle, float min, float max)
    {
        if (angle < -90) angle += 90;
        if (angle > 90) angle -= 90;

        return Mathf.Clamp(angle, min, max);
    }

    Transform oTransform;

    void Awake()
    {
        //Cursor.lockState = CursorLockMode.Locked;
        Vector3 angles = transform.eulerAngles;

        x = angles.x;
        y = angles.y;
    }

    void Update()
    {
        Balance();

        if (chanController.isGrounded)
        {
            GroundChecking();
            MoveUnityChan(1.0f);
        }
        else
        {
            // 관성에 영향을 준다.
            move.y -= gravity * Time.deltaTime;
            MoveUnityChan(0.01f);
        }
        chanController.Move(move * Time.deltaTime);

        if (Input.GetKeyDown(KeyCode.Space))
        {
            chanAnimator.SetTrigger("aJump");
        }
    }

    void LateUpdate()
    {
        if(target)
        {
            distance -= 1 * Input.mouseScrollDelta.y;

            if (distance < 2) distance = 2.0f;
            if (distance >= 3) distance = 3.0f;

            x += Input.GetAxis("Mouse X") * X_RotateSpeed * 0.015f;
            y -= Input.GetAxis("Mouse Y") * Y_RotateSpeed * 0.015f;

            x = ClampAngle(x, X_MinLimit, X_MaxLimit);
            y = ClampAngle(y, Y_MinLimit, Y_MaxLimit);

            Quaternion rotation = Quaternion.Euler(y, x, 0);
            Vector3 position = rotation * new Vector3(0, 0.0f, -distance) + target.position + new Vector3(0.0f, height, 0.0f);

            transform.rotation = rotation;
            transform.position = position;
        }
    }
}
