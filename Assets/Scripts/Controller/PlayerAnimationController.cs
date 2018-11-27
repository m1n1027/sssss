using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PlayerAnimationController : MonoBehaviour {

    punikon Punicon;
    int state = 0;

    public readonly  int  IdleState = 0;        //待機
    public readonly int MoveState = 1;        //移動
    public readonly int AttackState = 2;      //攻撃
    public readonly int AvoidanceState = 3;   //回避

    Animator anim;
    public float AnimNormalizeTime
    {
        get
        {
            return anim.GetCurrentAnimatorStateInfo(0).normalizedTime;
        }
    }

    public float foo() { return anim.GetCurrentAnimatorStateInfo(0).length; }
    public int State
    {
        get { return state; }
        set {
            state = value;
            anim.SetInteger("State", state);
        }
    }
    private void Awake()
    {
        Punicon = FindObjectOfType<punikon>();
        anim = GetComponent<Animator>();
    }
    // Use this for initialization
    IEnumerator Start () {

        while(true)
        {
            //switch (Punicon.GetState())
            //{
            //    case 1:  //Move(Run)
            //        state = MoveState;
            //        anim.SetInteger("State", state);
            //        break;
            //    default:
            //        state = 0;
            //        anim.SetInteger("State", state);
            //        break;
            //}
            yield return null;
        }

	}
	
	// Update is called once per frame
	void Update () {
       
	}
}
