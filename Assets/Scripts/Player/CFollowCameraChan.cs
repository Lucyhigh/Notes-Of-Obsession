using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/*
 apply root motion 체크되어있으면 스크립트에서 작동안함 우린 체크하면안됨

has Exit Time ==

greater 크냐

중력 맞추기
카메라 기능
기울기 잡아주기
캐릭터 이동

LootRotation(0,0,0)
매개변수 저 0 3개 넣는순간 터진다^^
회전하는건데 0이라고???
그래서 예외를 걸어줘야함;;

최적화가 된 코드를 보여주는 중.....난이도가 높다

점프는 그동안 bool로했는데 여기서는 트리거로 한다 

휘두를때 트루 끝나면 펄스

메카님 한정으로 진리의 불이 트리거한테 밀린다..

점프>런 점프 >아이들 은 has Exit 끄면 ㄴㄴ 점프끝나는거 기다려야함
 */
public class CFollowCameraChan : MonoBehaviour
{
    #region 변수
    //중력
    public float gravity = 10.0f;
    //달리기 속도
    public float runSpeed = 4.0f;
    //마우스 감도
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
        //땅 밟고있니?
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

        else if (Input.GetKeyDown(KeyCode.Q))
        {
            chanAnimator.SetTrigger("aAttack");
        }
    }

    void LateUpdate()
    {
        //대상의 높이
        cameraParentTransform.position = oTransform.position + Vector3.up * 0.7f;

        //마우스 움직임 회전이라 축회전기준으로 x,y의 위치가 달라짐
        mouseMove += new Vector3(-Input.GetAxisRaw("Mouse Y") * mouseSensitivity,
                                  Input.GetAxisRaw("Mouse X") * mouseSensitivity,
                                  0
                                  );
        //높이 제한 / 슈팅게임의 경우는 >> 90도 가까이 맞춰라
        //테스트 필요
        if (mouseMove.x < -5) mouseMove.x = -5;
        else if (50 < mouseMove.x) mouseMove.x = 50;

        cameraParentTransform.localEulerAngles = mouseMove;
    }

    void Balance()
    {
        //지형의 영향이나 카메라 흔들림 등 기울어 진다면 바로 잡는다.
        if (oTransform.eulerAngles.x != 0 || oTransform.eulerAngles.z != 0)
        {
            oTransform.eulerAngles = new Vector3(0, oTransform.eulerAngles.y, 0);
        }
    }

    void CameraDistanceControll()
    {
        Camera.main.transform.localPosition += new Vector3(0, 0, Input.GetAxisRaw("Mouse ScrollWheel") * 2.0f);
        //최대 확대
        if (-2 < Camera.main.transform.localPosition.z)
        {
            Camera.main.transform.localPosition = new Vector3
                (
                    Camera.main.transform.localPosition.x,
                    Camera.main.transform.localPosition.y,
                    -2
                );
        }
        //최대 축소
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
        // 대각선 이동 자체가 키 2개 쓰는거라 연산이 2번들어간다.
        // 루트 2배의 속도를 갖기 때문에 이를 막아야한다.
        // 그래서 속도가 1이상 올라가면 노말라이즈 시킨다.

        //sqrMagnitude 연산을 두번 안하게 따로 저장중
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

        //조작중에만 카메라의 방향에 상대적으로 캐릭터가 움직이도록
        if (Input.GetButton("Horizontal") || Input.GetButton("Vertical"))
        {
            Quaternion cameraRotation = cameraParentTransform.rotation;

            //쓸데없는 연산은 빼버리기
            cameraRotation.x = cameraRotation.z = 0;

            oTransform.rotation = Quaternion.Slerp
                (
                    oTransform.rotation,
                    cameraRotation,
                    10.0f * Time.deltaTime
                );

            //Quaternion.LookRotation는 (0,0,0)이 들어가면 경고-->> 터진다!!

            if (move != Vector3.zero)
            {
                Quaternion characterRotation = Quaternion.LookRotation(move);
                characterRotation.x = characterRotation.z = 0;
                //필요없는 연산은 빼버리는 식으로 코드를 짠다.

                unityChanModel.rotation = Quaternion.Slerp
                        (
                        unityChanModel.rotation,
                        characterRotation,
                        10.0f * Time.deltaTime
                        );
            }
            //MoveTowards : 관성(서서히 이동할때 사용 -> 일정한 속도로 이동)
            //현재 위치, 목표 위치, 속도
            move = Vector3.MoveTowards(move, inputMoveXZ, rate * runSpeed);
        }
        else
        {
            //조작이 없으면 서서히 멈춘다.
            move = Vector3.MoveTowards(move, Vector3.zero, (1 - inputMoveXZMgnitude) * runSpeed * rate);
        }
        float speed = move.sqrMagnitude;
        chanAnimator.SetFloat("aSpeed", speed);

        // y값 복구(메모리 절약중...메모리는 소중하니까)
        move.y = tempMoveY;
    }

    void GroundChecking()//레이로 땅 검사
    {
        if (Physics.Raycast(oTransform.position, Vector3.down, 0.5f))
        {
            move.y = -5;
        }
        else move.y = -1;
    }
   
}
