using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/*
 apply root motion üũ�Ǿ������� ��ũ��Ʈ���� �۵����� �츰 üũ�ϸ�ȵ�

has Exit Time ==

greater ũ��

�߷� ���߱�
ī�޶� ���
���� ����ֱ�
ĳ���� �̵�

LootRotation(0,0,0)
�Ű����� �� 0 3�� �ִ¼��� ������^^
ȸ���ϴ°ǵ� 0�̶��???
�׷��� ���ܸ� �ɾ������;;

����ȭ�� �� �ڵ带 �����ִ� ��.....���̵��� ����

������ �׵��� bool���ߴµ� ���⼭�� Ʈ���ŷ� �Ѵ� 

�ֵθ��� Ʈ�� ������ �޽�

��ī�� �������� ������ ���� Ʈ�������� �и���..

����>�� ���� >���̵� �� has Exit ���� ���� ���������°� ��ٷ�����
 */
public class CFollowCameraChan : MonoBehaviour
{
    #region ����
    //�߷�
    public float gravity = 10.0f;
    //�޸��� �ӵ�
    public float runSpeed = 4.0f;
    //���콺 ����
    public float mouseSensitivity = 2.0f;
    #endregion

    Transform oTransform;
    Transform unityChanModel;
    Transform cameraTransform;
    Transform cameraParentTransform;

    CharacterController chanController;
    Animator chanAnimator;

    Vector3 move;
    Vector3 mouseMove;

    void Awake()
    {
        oTransform = transform;
        unityChanModel = transform.GetChild(0);
        cameraTransform = Camera.main.transform;
        cameraParentTransform = cameraTransform.parent;
        chanController = GetComponent<CharacterController>();
        chanAnimator = unityChanModel.GetComponent<Animator>();
    }

    void Update()
    {
        Balance();
        CameraDistanceControll();
        //�� ����ִ�?
        if (chanController.isGrounded)
        {
            GroundChecking();
            MoveUnityChan(1.0f);
        }
        else
        {
            // ������ ������ �ش�.
            move.y -= gravity * Time.deltaTime;
            MoveUnityChan(0.01f);
        }
        chanController.Move(move * Time.deltaTime);

        if (Input.GetKeyDown(KeyCode.Space))
        {
            chanAnimator.SetTrigger("aJump");
        }

        else if (Input.GetKeyDown(KeyCode.Q))
        {
            chanAnimator.SetTrigger("aAttack");
        }
    }

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
        if (mouseMove.x < -5) mouseMove.x = -5;
        else if (50 < mouseMove.x) mouseMove.x = 50;

        cameraParentTransform.localEulerAngles = mouseMove;
    }

    void Balance()
    {
        //������ �����̳� ī�޶� ��鸲 �� ���� ���ٸ� �ٷ� ��´�.
        if (oTransform.eulerAngles.x != 0 || oTransform.eulerAngles.z != 0)
        {
            oTransform.eulerAngles = new Vector3(0, oTransform.eulerAngles.y, 0);
        }
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

    void MoveUnityChan(float rate)
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

                unityChanModel.rotation = Quaternion.Slerp
                        (
                        unityChanModel.rotation,
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
        chanAnimator.SetFloat("aSpeed", speed);

        // y�� ����(�޸� ������...�޸𸮴� �����ϴϱ�)
        move.y = tempMoveY;
    }

    void GroundChecking()//���̷� �� �˻�
    {
        if (Physics.Raycast(oTransform.position, Vector3.down, 0.5f))
        {
            move.y = -5;
        }
        else move.y = -1;
    }
   
}
