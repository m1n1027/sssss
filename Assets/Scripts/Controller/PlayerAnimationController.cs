using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum PlayerAnimationNumber : int
{
    Idle      = 0,     //待機
    Move      = 1,     //移動
    Attack    = 2,     //攻撃
    Avoidance = 3      //回避
}
public class PlayerAnimationController : MonoBehaviour {

    int state = 0;

    Animator anim;
    public float AnimNormalizeTime
    {
        get
        {
            return anim.GetCurrentAnimatorStateInfo(0).normalizedTime;
        }
    }

    public int State
    {
        get { return state; }
        set {
            state = value;
            anim.SetInteger("State", state);
        }
    }

    public void ChangeAnimation(int animationNumber)
    {
        State = animationNumber;
    }

    public void ChangeAnimation(PlayerAnimationNumber animationNumber)
    {
        State = (int)animationNumber;
    }

    private void Awake()
    {
        anim = GetComponent<Animator>();
    }
    	
}
