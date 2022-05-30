using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CRayExample : MonoBehaviour
{
    #region 변수
    [Header("RayCast")]
    public float rayDistance = 10.0f;
    public float raysWidth = 0.5f;
    public float raysHeight = 0.5f;
    #endregion

    private Ray ray;
    //RayCast로 구해진 결과 정보를 담는 구조체 (hit된 정보)
    private RaycastHit rayHit;
    private RaycastHit[] rayHitArr;

    void Start()
    {
        ray = new Ray
        {
            origin = this.transform.position,
            direction = this.transform.forward
        };
    }

    void Update()
    {
        RayCastTag();

        ray.origin = this.transform.position;
        ray.direction = this.transform.forward;
       
    }

    void RayCastTag()
    {
        rayHitArr = Physics.RaycastAll(ray, rayDistance);

        for (int i = 0; i < rayHitArr.Length; i++)
        {
            if (rayHitArr[i].collider.gameObject.CompareTag("MusicBox"))
            {
                Debug.Log(rayHitArr[i].collider.gameObject.tag + " 감지");

            }
        }
    }

    void OnDrawGizmos()
    {
        // 오브젝트 시각화
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(this.transform.position, 1);

        // 레이 시각화
        Debug.DrawRay(ray.origin, ray.direction * rayDistance, Color.red);

        Gizmos.color = Color.red;
        Gizmos.DrawSphere(ray.origin, 0.2f);

        DrawHitPoint();
    }

    void DrawHitPoint()
    {
        if (this.rayHitArr != null)
        {
            for (int i = 0; i < this.rayHitArr.Length; i++)
            {
                if (this.rayHitArr[i].collider != null)
                {
                    Gizmos.color = Color.red;

                    Gizmos.DrawSphere(this.rayHitArr[i].point, 0.1f);

                    Gizmos.color = Color.yellow;

                    Gizmos.DrawLine(
                        this.transform.position,
                        this.transform.position + this.transform.forward * rayHitArr[i].distance);
                }
            }
        }
    }
}