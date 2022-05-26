using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CPlayerCamera : MonoBehaviour
{
    #region º¯¼ö
    public Transform target;

    public float distance = 2.0f;
    public float height = 1.0f;

    public float X_RotateSpeed = 100.0f;
    public float Y_RotateSpeed = 100.0f;

    public float Y_MinLimit = -20.0f;
    public float Y_MaxLimit = 80.0f;
    #endregion

    private float x = 0.0f;
    private float y = 0.0f;
    
    float ClampAngle(float angle, float min, float max)
    {
        if (angle < -90) angle += 90;
        if (angle > 90) angle -= 90;

        return Mathf.Clamp(angle, min, max);
    }

    void Start()
    {
        //Cursor.lockState = CursorLockMode.Locked;
        Vector3 angles = transform.eulerAngles;

        x = angles.x;
        y = angles.y;
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

            y = ClampAngle(y, Y_MinLimit, Y_MaxLimit);

            Quaternion rotation = Quaternion.Euler(y, x, 0);
            Vector3 position = rotation * new Vector3(0, 0.0f, -distance) + target.position + new Vector3(0.0f, height, 0.0f);

            transform.rotation = rotation;
            transform.position = position;
        }
    }
}
