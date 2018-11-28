using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using puniState;

public class PlayerController : MonoBehaviour {
    private bool isAvoidance = false;

    //private InputManager inputManager;
    private punikon ogtInput;
    public Text dtext;
    /// <summary>
    /// NULL = Idle 
    /// </summary>
    private Coroutine ActionState;
    private PlayerAnimationController pAnim;
    Rigidbody rigidBody;
    int state = 0;

    BoxCollider boxCol;
    public bool isAttack = false;
    // Use this for initialization
    void Start () {
        ogtInput = FindObjectOfType<punikon>();
        pAnim = GetComponent<PlayerAnimationController>();
        rigidBody = GetComponent<Rigidbody>();
        boxCol = GetComponent<BoxCollider>();
        boxCol.enabled = false;
  
	}

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.A)) { ActionState = null; ActionState = StartCoroutine(Attack01()); }
        //if (Input.touchCount < 1) return;
        int state = ogtInput.GetState();
        switch (state)
        {
            // Idle
            case (int)TouchState.NONE:
                //state = pAnim.IdleState;
                break;
            // Attack
            case (int)TouchState.Tap:
                if (state == 1)
                {
                    StopCoroutine(ActionState);
                    ActionState = null;
                }
                if (ActionState == null) ActionState = StartCoroutine(Attack01());
                break;
            // Move
            case (int)TouchState.Move:
                if (ActionState == null) ActionState = StartCoroutine(Move());
                break;

            // Avoidance
            case (int)TouchState.Flick:
                if (ActionState == null && state != pAnim.AvoidanceState)
                {
                    isAvoidance = false;
                    pAnim.State = pAnim.AvoidanceState;
                    //ActionState = StartCoroutine(Avoidance());
                }

                break;

        }

        switch(pAnim.State)
        {
            case 3:
                Avoidancee();
                break;
        }
    }

    IEnumerator Move()
    {
        state = pAnim.MoveState;
        pAnim.State = pAnim.MoveState; while (true)
        {
            int state = ogtInput.GetState();
            if (state == 1)
            {
                var vec = ogtInput.GetDirection();
                Vector3 dir = new Vector3(vec.x, 0, vec.y);
                dir += transform.position;
                transform.LookAt(dir);
                transform.position += transform.forward * 0.1f;

            }
            else break;

            yield return true;
        }

        rigidBody.velocity = Vector3.zero;
        pAnim.State = pAnim.IdleState;
        ActionState = null;
    }

    IEnumerator Attack01()
    {
        state = pAnim.AttackState;
        pAnim.State = pAnim.AttackState;
        boxCol.enabled = true;
        isAttack = true;
        // TODO:攻撃した時の処理
        float value = 0;
        while (value < 0.7f)
        {
            value = pAnim.AnimNormalizeTime;
            yield return null;
        }

        boxCol.enabled = false;
        isAttack = false;

        pAnim.State = pAnim.IdleState;
        ActionState = null;
        yield return null;
    }

    bool Avoidancee()
    {
        if (!isAvoidance)
        {
            pAnim.State = pAnim.AvoidanceState;
            isAvoidance = true;
            //フリックした方向にプレイヤーを回転
            var vec = ogtInput.GetDirection();
            Vector3 dir = new Vector3(vec.x, 0, vec.y)*3;
            dir += transform.position;
            transform.LookAt(dir);
            return false;
        }

        var val = pAnim.AnimNormalizeTime;
        if (val >= 0.6f)        //終了
        {
            isAvoidance = false;
            pAnim.State =(ogtInput.GetState() == (int)TouchState.Move)?pAnim.MoveState:pAnim.IdleState;

            return true;
        }


        return false;

    }
    //IEnumerator Avoidance()
    //{
    //    bool isPlaying = true;
    //    while(!isPlaying)
    //    {
    //        yield return null;
    //    }

    //}

    IEnumerator MoveCoroutine(Vector3 movePower)
    {
        Vector3 beforePos = new Vector3(transform.position.x, transform.position.y, transform.position.z);
        Vector3 afterPos = beforePos + movePower;
        for (int i = 0; i < 6; i++)
        {
            transform.position = Vector3.Lerp(beforePos, afterPos, (float)i * 0.2f);
            yield return null;
        }

        yield return null;
    }
}
