using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CPlayerCamera : MonoBehaviour
{
    #region ����
    public float distance = 2.5f;
    public float height = 1.0f;
    //�޸��� �ӵ�
    public float runSpeed = 4.0f;
    //���콺 ����
    public float mouseSensitivity = 2.0f;
    //public float X_MinLimit = 60.0f;
    //public float X_MaxLimit = 60.0f;
    //
    //public float Y_MinLimit = -20.0f;
    //public float Y_MaxLimit = 45.0f;

    //=======================
    public float rayDistance = 10.0f;
    #endregion

    Transform oTransform;
    Transform playerModel;
    Transform cameraTransform;
    Transform cameraParentTransform;

    CharacterController playerController;
    Animator playerAnimator;

    Vector3 move;
    Vector3 mouseMove;

    private Ray ray;
    //RayCast�� ������ ��� ������ ��� ����ü (hit�� ����)
    private RaycastHit rayHit;

    private RaycastHit[] rayHitArr;

    void Awake()
    {
        //Cursor.lockState = CursorLockMode.Locked;

        oTransform = transform;
        playerModel = transform.GetChild(0);
        cameraTransform = Camera.main.transform;
        cameraParentTransform = cameraTransform.parent;
        playerController = GetComponent<CharacterController>();
        playerAnimator = playerModel.GetComponent<Animator>();
    }

    void Update()
    {
        // Balance();
        CameraDistanceControll();
        if (playerController.isGrounded)
        {
            ObjectChecking();
            MovePlayer(1.0f);
        }
        else
        {
            // ������ ������ �ش�.
            //move.y -= gravity * Time.deltaTime;
            MovePlayer(0.01f);
        }
        playerController.Move(move * Time.deltaTime);

        if (Input.GetKeyDown(KeyCode.Space))
        {
            playerAnimator.SetTrigger("aFirst");
        }
    }

    #region camera 
    void LateUpdate()
    {
        //����� ����
        cameraParentTransform.position = oTransform.position + Vector3.up * 0.7f;

        //���콺 ������ ȸ���̶� ��ȸ���������� x,y�� ��ġ�� �޶���
        mouseMove += new Vector3(-Input.GetAxisRaw("Mouse Y") * mouseSensitivity,
                                  Input.GetAxisRaw("Mouse X") * mouseSensitivity,
                                  0
                                  );
        //���� ���� / ���ð����� ���� >> 90�� ������ �����
        //�׽�Ʈ �ʿ�
        if (mouseMove.x < -20) mouseMove.x = -20;
        else if (45 < mouseMove.x) mouseMove.x = 45;

        cameraParentTransform.localEulerAngles = mouseMove;
    }

    void CameraDistanceControll()
    {
        Camera.main.transform.localPosition += new Vector3(0, 0, Input.GetAxisRaw("Mouse ScrollWheel") * 2.0f);
        //�ִ� Ȯ��
        if (-2 < Camera.main.transform.localPosition.z)
        {
            Camera.main.transform.localPosition = new Vector3
                (
                    Camera.main.transform.localPosition.x,
                    Camera.main.transform.localPosition.y,
                    -2
                );
        }
        //�ִ� ���
        else if (Camera.main.transform.localPosition.z < -5)
        {
            Camera.main.transform.localPosition = new Vector3
           (
                Camera.main.transform.localPosition.x,
                Camera.main.transform.localPosition.y,
                -5
           );
        }
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
                        10.0f * Time.deltaTime
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

    void ObjectChecking()
    {
        if(Physics.Raycast(oTransform.position,Vector3.forward,0.5f))
        {
            if(Input.GetKeyDown(KeyCode.Q))
            {
                //������ �ִϸ��̼� �۵�- �ڷ�ƾ?
            }
        }

    }

    #endregion
    #region raycast
    // ������Ʈ ����(��ü : ���� ĳ��Ʈ�� �ɸ��� ��� ������Ʈ)
    void RaySample()
    {
        // RaycastAll : ã�Ƴ� ��� ������Ʈ ��ȯ
        rayHitArr = Physics.RaycastAll(ray, rayDistance);

        for (int i = 0; i < rayHitArr.Length; i++)
        {
            Debug.Log(rayHitArr[i].collider.gameObject.name + " ����");
        }
    }
    #endregion
}//�ؽ�Ʈ ������ ���� ������ŭ ���ýÿ� �۵�
