using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CMusicBoxController : MonoBehaviour
{
    #region
    public Transform[] musicboxObjects; //spring hourHand ballerina
    public float currentAngle = 0.0f;
    public float startAngle = 0.0f;
    #endregion

    Animator musicBoxColltroller;

    float moveAngle = 30.0f;

    float startLerpTime = 0.0f;
    float goalLerpTime = 1.0f;

    bool isPlay = false;

    void Update()
    {
        play();
    }

    void play()
    {
        if(Input.GetKey(KeyCode.Mouse0))
        {
            isPlay = true;
            startLerpTime = Time.time;
            Debug.LogFormat("before:{0} cur:{1}", currentAngle, moveAngle);
            StartCoroutine(playMusicBox());
        }

        if (!isPlay && currentAngle > startAngle)
        {
            currentAngle -= 0.1f;
            musicboxObjects[0].rotation = Quaternion.Euler(currentAngle,180.0f, 0.0f);//spring hourHand ballerina
            musicboxObjects[1].rotation = Quaternion.Euler(0.0f, -currentAngle, 0.0f);
            musicboxObjects[2].rotation = Quaternion.Euler(0.0f,currentAngle, 0.0f);//

            if(currentAngle < startAngle)
            {
                musicboxObjects[0].rotation = Quaternion.identity;
                musicboxObjects[1].rotation = Quaternion.identity;
                musicboxObjects[2].rotation = Quaternion.identity;
            }
        }
    }

    private IEnumerator playMusicBox()
    {
        while (true)
        {
            yield return null;

            float fracComlete = (Time.time - startLerpTime) / goalLerpTime;

            musicboxObjects[0].rotation = Quaternion.Euler(currentAngle + (moveAngle * fracComlete), 180.0f, 0.0f);//spring hourHand ballerina
            musicboxObjects[1].rotation = Quaternion.Euler(0.0f, currentAngle + (-moveAngle * fracComlete), 0.0f);
            musicboxObjects[2].rotation = Quaternion.Euler(0.0f, currentAngle + (moveAngle * fracComlete), 0.0f);//

            //if(fracComlete >= goalLerpTime)
            {
                //Debug.Log("=====break======");
                //currentAngle += moveAngle;
                //isPlay = false;
                //yield break;
            }
        }
    }
}