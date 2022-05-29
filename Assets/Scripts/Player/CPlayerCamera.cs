using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;


public class CPlayerCamera : MonoBehaviour
{
    #region ����
    public float cameraDistance = 2.5f;
    public float cameraHeight = 1.0f;
    //�޸��� �ӵ�
    public float runSpeed = 4.0f;
    //���콺 ����
    public float mouseSensitivity = 2.0f;

    [Space(5.0f)]
    [Header("Spherecast")]
    public float sphereSize = 5.0f;
    public float rayDistance = 10.0f;
    //public float rayWidth = 5.0f;
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
        cameraTransform = Camera.main.transform; //maincamera
        cameraParentTransform = cameraTransform.parent; //cameraset 
        playerController = GetComponent<CharacterController>();
        playerAnimator = playerModel.GetComponent<Animator>();
    }

    void Start()
    {
        //for(int i = 0; i < 3; i++)
        //{
        //    ray[i] = new Ray[]
        //    {
        //        origin = this.transform.position, 
        //        direction = this.transform.forward * rayDistance
        //    };
        //}

    }
    void Update()
    {
        // Balance();
        //RaySample();
        CameraDistanceControll();

        //ObjectChecking();
        MovePlayer(1.0f);

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

        if (mouseMove.y < -60) mouseMove.y = -60;
        else if (60 < mouseMove.y) mouseMove.y = 60;

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

    #endregion
    #region raycasts
    void ObjectChecking()
    {
        Vector3 center = new Vector3(Camera.main.transform.position.x, Camera.main.transform.position.y, Camera.main.transform.position.z + rayDistance);
        Vector3 direction = Camera.main.transform.TransformDirection(Vector3.forward) * rayDistance;
        Ray ray = new Ray(
                        center,
                        direction
                        );
        //if (Physics.SphereCast(ray,sphereSize,rayDistance))
        //{
        //    if(Input.GetKeyDown(KeyCode.Q))
        //    {
        //        //������ �ִϸ��̼� �۵�- �ڷ�ƾ?
        //        Debug.Log("������Ʈ üũ");
        //    }
        //}

    }

    void RaySample()
    {
        RaycastHit hitInfo;
        if(Physics.Raycast(
            new Vector3(Camera.main.transform.position.x, Camera.main.transform.position.y, Camera.main.transform.position.z + rayDistance),
            cameraParentTransform.forward,
            out hitInfo,
            rayDistance))
        {
            Debug.LogFormat("���̸��� hitInfo {0}",hitInfo.transform.gameObject.tag);
        }

        //�±� ����
        //tag : ���� ������Ʈ�� �з��ϴ� ��� �� �ϳ�
        //for (int i = 0; i < rayHitArr.Length; i++)
        //{
        //    if (rayHitArr[i].collider.gameObject.CompareTag("MusicBox"))//���߿� �� ������ - �ɺ�,��,������ ��
        //    {
        //        Debug.Log(rayHitArr[i].collider.gameObject.name + " ����");
        //    }
        //}
    }

    private void OnDrawGizmos()
    {
        Vector3 center = new Vector3(Camera.main.transform.position.x, Camera.main.transform.position.y, Camera.main.transform.position.z + rayDistance); 
        Vector3 direction = Camera.main.transform.TransformDirection(Vector3.forward) * rayDistance;
        //Ray ray = new Ray(
        //                center,
        //                direction);
        Gizmos.color = Color.red;
        //    Gizmos.DrawRay(center, direction);
        Gizmos.DrawSphere(center, rayDistance);
    }

    #endregion
}//�ؽ�Ʈ ������ ���� ������ŭ ���ýÿ� �۵�
