using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class PlayerController : MonoBehaviour
{
    public ParticleSystem ps;
    public float _RotateSpeed = 1.0f;
    private CameraManager cameraManager;
    private PlayerAnimationController pAnim;
    private Rigidbody rigidBody;
    private Vector2 touchDir = Vector2.zero;
    public BoxCollider boxCol;
    public bool isAttack = false;

    // Use this for initialization
    void Start()
    {
        cameraManager = FindObjectOfType<CameraManager>();
        pAnim = GetComponent<PlayerAnimationController>();
        rigidBody = GetComponent<Rigidbody>();
        boxCol = GetComponent<BoxCollider>();
        boxCol.enabled = true;

    }

    void Update()
    {
        Debug.Log("forward:" + transform.forward + " right:" + transform.right);
        //アニメーション遷移
        StateTransition();
        if (Input.GetKeyDown(KeyCode.A))pAnim.ChangeAnimation(PlayerAnimationNumber.Attack);
        if (pAnim.currentState == (int)PlayerAnimationNumber.Idle) { boxCol.enabled = false; isAttack = false; }
        //Debug.Log(pAnim.currentState);
    }

    void FixedUpdate()
    {

        //アニメーションステイトが回避ステイトにいる時のみ実行
        if (pAnim.CurrentStateHash == pAnim.AdvoidanceState)
        {
            Move(0.1f);
        }
        if (pAnim.CurrentStateHash == pAnim.Attack01State) Attack01();
        //アニメーションステイトが移動ステイトにいる時(遷移中含む)
        if (pAnim.currentState == (int)PlayerAnimationNumber.Run)
        {
            ps.Play();
            var direction = TouchParam.GetInstance.touchDirection;
            //画面にタッチ(スワイプ)している方向へプレイヤーを向ける
            Rotate(direction);
            //LookAtDirection(direction);
            Move(0.1f);
        }
        else ps.Stop();
    }

    //アニメーション遷移
    //タッチの入力がない時はIdle状態にする
    void StateTransition()
    {
        PlayerAnimationNumber animNumber = PlayerAnimationNumber.Idle;
        switch(TouchParam.GetInstance.touchState)
        {
            case TouchState.Move:         //移動
                animNumber = PlayerAnimationNumber.Run;
                break;
            case TouchState.Flick:        //回避
                var direction = TouchParam.GetInstance.touchDirection;
                LookAtDirection(direction);
                animNumber = PlayerAnimationNumber.Avoidance;
                break;
            case TouchState.Tap:          //攻撃
                animNumber = PlayerAnimationNumber.Attack;
                break;
            default:                      //待機

                break;

        }
        pAnim.ChangeAnimation(animNumber);
    }

    void Move(float speed)
    {
        transform.localPosition += transform.forward * speed;
    }

    void Attack01()
    {
        boxCol.enabled = true;
        isAttack = true;
    }

    //指定の方向にプレイヤーを回転させる
    void LookAtDirection(Vector2 vec)
    {
        Vector3 direction = new Vector3(vec.x*transform.right.x, 0, vec.y*transform.forward.z);
        direction += transform.position;
        transform.LookAt(direction);
    }

    void Rotate(Vector2 vec)
    {
        transform.Rotate(0, vec.x*_RotateSpeed, 0);
    }
}

