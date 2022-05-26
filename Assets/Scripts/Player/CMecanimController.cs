using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
    ㅁ -> ㅅ        ㅣ-  ㅅ
    ㅁ -> ㅅ      ㅁㅣ-  ㅅ
    ㅁ -> ㅅ        ㅣ-  ㅅ
    -레거시        메카님

메카님 애니메이션 - 상태 머신 시스템
- 기본적으로 상태를 만들고 애니메이션 재생을 관리하며 트랜지션 로직을 사용해 다른 상태로 전이하며 동작시키는 방식
- 메카님은 "리타켓팅" 시스템을 완벽지원한다.
   ㄴ 스키닝 애니메이션은 특정 캐릭터 한정해 굉정히 효율적으로 애니메이션 재생 및 관리가 가능한다.
     하지만 본 구조가 약간만 달라져도 별도의 애니메이션 제작을 해야한다...
     이를 위한 애니메이션 작업을 줄이기 위해 리타켓팅이라는 기법이 나왔고 
     휴머노이드 한정으로 애니메이션을 공유할 수 있다.
 */
public class CMecanimController : MonoBehaviour
{
    CharacterController chanController;

    Vector3 direction;
    #region 변수
    public float runSpeed = 4.0f;
    public float rotationSpeed = 4.0f;
    #endregion

    Animator animator;
    void Start()
    {
        chanController = GetComponent<CharacterController>();
        //animator = GetComponent<Animator>();
        //ㄴ 이거 안댐....왜?? 논리오류임....실행하면 바로 오류임 애니메이터는 모델에 들어가있고 계층구조가 나눠져있어서 내 자식인 유니티짱에게 해줘야함
        animator = GetComponentInChildren<Animator>();
    }

    void Update()
    {
        animator.SetFloat("aSpeed", chanController.velocity.magnitude);

        chanControl_Slerp();
    }

    void chanControl_Slerp()
    {
        Vector3 direction = new Vector3
            (
            Input.GetAxis("Horizontal"),
            0,
            Input.GetAxis("Vertical")
            );

        if (direction.magnitude > 0.01f)
        {
            Vector3 forward = Vector3.Slerp
            (
                transform.forward,
                direction,
                rotationSpeed * Time.deltaTime / Vector3.Angle(transform.forward, direction)
            );

            transform.LookAt(transform.position + forward);
        }

        else
        {
            // 아직 애니메이션이 없음...
        }

        chanController.Move(direction * runSpeed * Time.deltaTime);
    }
}