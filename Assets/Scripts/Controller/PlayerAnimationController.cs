using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum PlayerAnimationNumber : int
{
    Idle      = 0,     //待機
    Run       = 1,     //移動
    Attack    = 2,     //攻撃
    Avoidance = 3      //回避
}
public class PlayerAnimationController : MonoBehaviour {
    #region 各アニメーションのハッシュ
    public readonly int IdleState = Animator.StringToHash("Base Layer.Idle");
    public readonly int AdvoidanceState = Animator.StringToHash("Base Layer.Avoidance");
    public readonly int RunState = Animator.StringToHash("Base Layer.Run");
    public readonly int Attack01State = Animator.StringToHash("Base Layer.Attack01");
    #endregion
    private Animator anim;
    public int currentState;

    void Start()
    {
        anim = GetComponent<Animator>();
    }

    //ベースレイヤーの現在のステイトを返す
    public AnimatorStateInfo CurrentBaseState
    {
        get { return anim.GetCurrentAnimatorStateInfo(0); }
    }

    //ベースレイヤーの現在のステイトのハッシュを返す
    public int CurrentStateHash
    {
        get { return CurrentBaseState.fullPathHash; }
    }

    //現在再生されているアニメーションの正規化された時間を返す
    public float AnimNormalizedTime
    {
        get{ return anim.GetCurrentAnimatorStateInfo(0).normalizedTime; }
    }

    //アニメーションが遷移途中かどうか
    //遷移中ならtrue
    public bool IsTransration
    {
        get { return anim.IsInTransition(0); }
    }

    public void ChangeAnimation(int animationNumber)
    {
        currentState = animationNumber;
        anim.SetInteger("State", animationNumber);
    }

    public void ChangeAnimation(PlayerAnimationNumber animationNumber)
    {
        ChangeAnimation((int)animationNumber);
    }
    

    	
}
