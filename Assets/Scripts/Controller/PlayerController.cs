using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class PlayerController : MonoBehaviour
{
    private bool isPlaying = false;
    private punikon ogtInput;
    public Text dtext;
    /// <summary>
    /// NULL = Idle 
    /// </summary>
    //private Coroutine ActionState;
    private PlayerAnimationController pAnim;
    Rigidbody rigidBody;
    private TouchState actionState = TouchState.NONE;

    BoxCollider boxCol;
    public bool isAttack = false;
    // Use this for initialization
    void Start()
    {
        ogtInput = FindObjectOfType<punikon>();
        pAnim = GetComponent<PlayerAnimationController>();
        rigidBody = GetComponent<Rigidbody>();
        boxCol = GetComponent<BoxCollider>();
        boxCol.enabled = false;

    }

    void Update()
    {
        actionState = TouchParam.GetInstance.touchState;
        //if (pAnim.State == (int)PlayerAnimationNumber.Idle)
        //{

        //    rigidBody.velocity = Vector3.zero;
        //    pAnim.ChangeAnimation(PlayerAnimationNumber.Idle);
        //    isPlaying = false;
        //}
        StateTransition();
        //if (TouchParam.GetInstance.touchState == TouchState.Move) pAnim.ChangeAnimation(PlayerAnimationNumber.Move);
        switch (pAnim.State)
        {
            case (int)PlayerAnimationNumber.Attack:
                break;
            case (int)PlayerAnimationNumber.Avoidance:
                Avoidance();
                break;
        }
    }

    private void FixedUpdate()
    {
        if (pAnim.State == (int)PlayerAnimationNumber.Move)
        {
            Move();
        }

    }

    void StateTransition()
    {
        PlayerAnimationNumber animNumber = PlayerAnimationNumber.Idle;
        switch(TouchParam.GetInstance.touchState)
        {
            case TouchState.Move:
                animNumber = PlayerAnimationNumber.Move;
                break;
            case TouchState.Flick:
                animNumber = PlayerAnimationNumber.Avoidance;
                break;
            case TouchState.Tap:
                animNumber = PlayerAnimationNumber.Attack;
                break;

        }
        pAnim.ChangeAnimation(animNumber);
    }

    void Move()
    {
        if (!isPlaying) isPlaying = true;
        var vec = TouchParam.GetInstance.touchDirection;
        Vector3 dir = new Vector3(vec.x, 0, vec.y);
        dir += transform.position;
        transform.LookAt(dir);
        transform.position += transform.forward * 0.1f;
    }

    void Attack01()
    {
        if (!isPlaying)
        {
            boxCol.enabled = true;
            isPlaying = true;
        }

        float value = pAnim.AnimNormalizeTime;
        if (value >= 0.6f)
        {
            boxCol.enabled = false;
            isPlaying = false;
            pAnim.ChangeAnimation(PlayerAnimationNumber.Idle);
        }
    }

    void Avoidance()
    {
        if (!isPlaying)
        {
            isPlaying = true;
            //フリックした方向にプレイヤーを回転
            var vec = TouchParam.GetInstance.touchDirection;
            Vector3 dir = new Vector3(vec.x, 0, vec.y) * 3;
            dir += transform.position;
            transform.LookAt(dir);
            
        }

        var val = pAnim.AnimNormalizeTime;
        if (val >= 0.6f)        //終了
        {
            isPlaying = false;
            pAnim.ChangeAnimation(PlayerAnimationNumber.Idle);
        }

    }
    
}

