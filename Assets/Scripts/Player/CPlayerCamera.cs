using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class CPlayerCamera : MonoBehaviour
{
    #region ����
    public float cameraDistance = 2.5f;
    public float cameraHeight = 0.1f;
    //�޸��� �ӵ�
    public float runSpeed = 4.0f;
    //���콺 ����
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
        //Cursor.lockState = CursorLockMode.Locked;

        oTransform = transform;
        playerModel = transform.GetChild(0);
        cameraTransform = Camera.main.transform; //maincamera
        cameraParentTransform = cameraTransform.parent; //cameraset 
        playerController = GetComponent<CharacterController>();
        playerAnimator = playerModel.GetComponent<Animator>();
    }

    void Start()
    {
    }

    void Update()
    {
        // Balance();
        MovePlayer(1.0f);
        playerController.Move(move * Time.deltaTime);
    }

    #region camera 
    void LateUpdate()
    {
        //����� ����
        cameraParentTransform.position = oTransform.position + Vector3.up * cameraHeight + Vector3.forward * cameraDistance;

        //���콺 ������ ȸ���̶� ��ȸ���������� x,y�� ��ġ�� �޶���
        mouseMove += new Vector3(-Input.GetAxisRaw("Mouse Y") * mouseSensitivity,
                                  Input.GetAxisRaw("Mouse X") * mouseSensitivity,
                                  0
                                  );
        //�׽�Ʈ �ʿ�
        if (mouseMove.x < -60) mouseMove.x = -60;
        else if (85 < mouseMove.x) mouseMove.x = 85;

        cameraParentTransform.localEulerAngles = mouseMove;
    }

    void MovePlayer(float rate)
    {
        float tempMoveY = move.y;

        move.y = 0;

        Vector3 inputMoveXZ = new Vector3
            (
                Input.GetAxis("Horizontal"),
                0,
                Input.GetAxis("Vertical")
            );
        // �밢�� �̵� ��ü�� Ű 2�� ���°Ŷ� ������ 2������.
        // ��Ʈ 2���� �ӵ��� ���� ������ �̸� ���ƾ��Ѵ�.
        // �׷��� �ӵ��� 1�̻� �ö󰡸� �븻������ ��Ų��.

        //sqrMagnitude ������ �ι� ���ϰ� ���� ������
        float inputMoveXZMgnitude = inputMoveXZ.sqrMagnitude;

        inputMoveXZ = oTransform.TransformDirection(inputMoveXZ);

        if (inputMoveXZMgnitude <= 1)
        {
            inputMoveXZ *= runSpeed;
        }
        else
        {
            inputMoveXZ = inputMoveXZ.normalized * runSpeed;
        }

        //�����߿��� ī�޶��� ���⿡ ��������� ĳ���Ͱ� �����̵���
        if (Input.GetButton("Horizontal") || Input.GetButton("Vertical"))
        {
            Quaternion cameraRotation = cameraParentTransform.rotation;

            //�������� ������ ��������
            cameraRotation.x = cameraRotation.z = 0;

            oTransform.rotation = Quaternion.Slerp
                (
                    oTransform.rotation,
                    cameraRotation,
                    10.0f * Time.deltaTime
                );

            //Quaternion.LookRotation�� (0,0,0)�� ���� ���-->> ������!!

            if (move != Vector3.zero)
            {
                Quaternion characterRotation = Quaternion.LookRotation(move);
                characterRotation.x = characterRotation.z = 0;
                //�ʿ���� ������ �������� ������ �ڵ带 §��.

                playerModel.rotation = Quaternion.Slerp
                        (
                        playerModel.rotation,
                        characterRotation,
                        20.0f * Time.deltaTime
                        );
            }
            //MoveTowards : ����(������ �̵��Ҷ� ��� -> ������ �ӵ��� �̵�)
            //���� ��ġ, ��ǥ ��ġ, �ӵ�
            move = Vector3.MoveTowards(move, inputMoveXZ, rate * runSpeed);
        }
        else
        {
            //������ ������ ������ �����.
            move = Vector3.MoveTowards(move, Vector3.zero, (1 - inputMoveXZMgnitude) * runSpeed * rate);
        }
        float speed = move.sqrMagnitude;
        playerAnimator.SetFloat("aSpeed", speed);

        // y�� ����(�޸� ������...�޸𸮴� �����ϴϱ�)
        move.y = tempMoveY;
    }
    #endregion
}