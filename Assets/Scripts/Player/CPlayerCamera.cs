using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CPlayerCamera : MonoBehaviour
{
    #region 변수
    public float distance = 2.5f;
    public float height = 1.0f;
    //달리기 속도
    public float runSpeed = 4.0f;
    //마우스 감도
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
    //RayCast로 구해진 결과 정보를 담는 구조체 (hit된 정보)
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
            // 관성에 영향을 준다.
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
        //대상의 높이
        cameraParentTransform.position = oTransform.position + Vector3.up * 0.7f;

        //마우스 움직임 회전이라 축회전기준으로 x,y의 위치가 달라짐
        mouseMove += new Vector3(-Input.GetAxisRaw("Mouse Y") * mouseSensitivity,
                                  Input.GetAxisRaw("Mouse X") * mouseSensitivity,
                                  0
                                  );
        //높이 제한 / 슈팅게임의 경우는 >> 90도 가까이 맞춰라
        //테스트 필요
        if (mouseMove.x < -20) mouseMove.x = -20;
        else if (45 < mouseMove.x) mouseMove.x = 45;

        cameraParentTransform.localEulerAngles = mouseMove;
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

                playerModel.rotation = Quaternion.Slerp
                        (
                        playerModel.rotation,
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
        playerAnimator.SetFloat("aSpeed", speed);

        // y값 복구(메모리 절약중...메모리는 소중하니까)
        move.y = tempMoveY;
    }

    void ObjectChecking()
    {
        if(Physics.Raycast(oTransform.position,Vector3.forward,0.5f))
        {
            if(Input.GetKeyDown(KeyCode.Q))
            {
                //오르골 애니메이션 작동- 코루틴?
            }
        }

    }

    #endregion
    #region raycast
    // 오브젝트 검출(전체 : 레이 캐스트에 걸리는 모든 오브젝트)
    void RaySample()
    {
        // RaycastAll : 찾아낸 모든 오브젝트 반환
        rayHitArr = Physics.RaycastAll(ray, rayDistance);

        for (int i = 0; i < rayHitArr.Length; i++)
        {
            Debug.Log(rayHitArr[i].collider.gameObject.name + " 감지");
        }
    }
    #endregion
}//텍스트 생성은 일정 구간만큼 들어올시에 작동
