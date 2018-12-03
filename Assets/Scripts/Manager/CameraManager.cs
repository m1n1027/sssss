using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : MonoBehaviour {
    //-----------------------------------------------------------
    //
    //  プレイヤー関連
    //
    //-----------------------------------------------------------

    public GameObject _target;
    public bool isMoved = false;
    //*************************************************************

    public Vector3 shakePower;
    public bool isShake;
    public float startTime = 0.0f;
    public float shakeTime = 2.0f;

    ////////////////////////////////////////////////////////////////
    //
    // @ obj 移動後のポジション　(移動先)
    // @ target        このポジションの向きにカメラが回転する
    // @ length        カメラをプレイヤーからどれくらい離すか
    // @ height        カメラの高さ
    // @ speed         移動のスピード 0.1~1.0　の間で使うのがベスト
    ////////////////////////////////////////////////////////////////
    public void SetPosition(Vector3 pos, Vector3 target, float speed)
    {
        StartCoroutine(ResetPosition(pos, target, speed));
    }

    ////////////////////////////////////////////////////////////////
    //
    //  プレイヤーの背後にカメラを持っていくやつ
    //
    // @ obj 移動後のポジション　(移動先)
    // @ target        このポジションの向きにカメラが回転する
    // @ length        カメラをプレイヤーからどれくらい離すか
    // @ height        カメラの高さ
    // @ speed         移動のスピード 0.1~1.0　の間で使うのがベスト
    ////////////////////////////////////////////////////////////////
    public void SetBehindThePlayer(GameObject obj, float speed, float length, float height)
    {
        var pos = obj.transform.position;
        var dir = obj.transform.forward;
        dir *= -length;
        dir.y = height;
        pos += dir;
        isMoved = false;
        SetPosition(pos, obj.transform.position, speed);
    }

    public void SetBehindTheTarget(GameObject obj, Vector3 targetPos, float speed = 1.0f, float length = 5.0f, float height = 6.0f)
    {
        var pos = obj.transform.position;
        var dir = obj.transform.forward;
        dir *= -length;
        dir.y = height;
        pos += dir;
        // StartCoroutine(ResetPosition(pos, obj.transform.position, speed));
        SetPosition(pos, targetPos, speed);
    }

    public Vector3 GetBehindThePlayer(GameObject target, float speed, float length, float height)
    {

        var pos = target.transform.position;
        var dir = target.transform.forward;
        dir *= -length;
        dir.y = height;
        pos += dir;
        return pos;
    }

    ////////////////////////////////////////////////////////////////
    //
    //  位置補正用コルーチン
    //
    // @ afterMovement 移動後のポジション　(移動先)
    // @ target        このポジションの向きにカメラが回転する
    // @ speed         移動のスピード 0.1~1.0　の間で使うのがベスト
    ////////////////////////////////////////////////////////////////

    IEnumerator ResetPosition(Vector3 afterMovement, Vector3 target, float speed)
    {
        var value = 0.0f;
        var targetRotation = Quaternion.LookRotation(target - afterMovement);
        speed /= 10;
        var pos = transform.position;

        while (value <= 1.0f)
        {

            transform.position = Vector3.Lerp(transform.position, afterMovement, value);
            transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, value);
            value += speed;
            yield return null;
        }
        isMoved = true;
        yield return null;
    }

    public Vector3 Shake()
    {

        return Vector3.zero;    //ダミー
    }

    bool isRunning = false;
    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.Z))
        {
            if (!isRunning) 
            {
                isRunning = true;
                StartCoroutine(ShakeUpdate());
            }
        }
        
    }

    IEnumerator ShakeUpdate()
    {
        while(Input.GetKey(KeyCode.Z))
        {
            var param = CameraParamTable.GetInstance;
            var LR = transform.right * Random.Range(-param.shakeWidth, param.shakeWidth) * param.shakePower; //横揺れ
            var UD = transform.up * Random.Range(-param.shakeHeight, param.shakeHeight) * param.shakePower;  //縦揺れ

            transform.position += LR;
            transform.position += UD;

            yield return new WaitForSeconds(param.shakeInterval);
        }

        isRunning = false;
    }
}
