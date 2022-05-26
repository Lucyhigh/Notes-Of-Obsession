using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
    �� -> ��        ��-  ��
    �� -> ��      ����-  ��
    �� -> ��        ��-  ��
    -���Ž�        ��ī��

��ī�� �ִϸ��̼� - ���� �ӽ� �ý���
- �⺻������ ���¸� ����� �ִϸ��̼� ����� �����ϸ� Ʈ������ ������ ����� �ٸ� ���·� �����ϸ� ���۽�Ű�� ���
- ��ī���� "��Ÿ����" �ý����� �Ϻ������Ѵ�.
   �� ��Ű�� �ִϸ��̼��� Ư�� ĳ���� ������ ������ ȿ�������� �ִϸ��̼� ��� �� ������ �����Ѵ�.
     ������ �� ������ �ణ�� �޶����� ������ �ִϸ��̼� ������ �ؾ��Ѵ�...
     �̸� ���� �ִϸ��̼� �۾��� ���̱� ���� ��Ÿ�����̶�� ����� ���԰� 
     �޸ӳ��̵� �������� �ִϸ��̼��� ������ �� �ִ�.
 */
public class CMecanimController : MonoBehaviour
{
    CharacterController chanController;

    Vector3 direction;
    #region ����
    public float runSpeed = 4.0f;
    public float rotationSpeed = 4.0f;
    #endregion

    Animator animator;
    void Start()
    {
        chanController = GetComponent<CharacterController>();
        //animator = GetComponent<Animator>();
        //�� �̰� �ȴ�....��?? ��������....�����ϸ� �ٷ� ������ �ִϸ����ʹ� �𵨿� ���ְ� ���������� �������־ �� �ڽ��� ����Ƽ¯���� �������
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
            // ���� �ִϸ��̼��� ����...
        }

        chanController.Move(direction * runSpeed * Time.deltaTime);
    }
}