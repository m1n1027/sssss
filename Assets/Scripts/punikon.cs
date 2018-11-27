using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using puniState;
namespace puniState
{
    public enum TouchState
    {
        NONE = -1,
        Tap = 0,
        Move = 1,
        Flick = 2,
        Charge = 3,
        ChargeMove = 4
    }
}

public class punikon : MonoBehaviour {
    public bool isTouch;
    private float touchLength;
    private Vector2 touchDirection;
    private Vector2 startPosition;
    private Vector2 touchPosition;
    private float startTime;
    private float touchTime;
    private float chargeTime;
    private int State;
    private TouchPhase touchPhase;
    private Touch touch;


    public bool GetTouchFlg() { return isTouch; }
    public int GetState() { return State; }
    public Vector2 GetDirection() { return touchDirection.normalized; }
    public float GetTouchTime() { return touchTime; }
    public float GetLength() { return touchDirection.magnitude; }
    public TouchPhase GetPhase() { return touchPhase; }
    public Touch GetTouch() { return touch; }

    public static readonly int NONE = -1, Tap = 0, Move = 1, Flick = 2, Charge = 3, ChargeMove = 4;

    // Use this for initialization
    void Awake () {
        isTouch = false;
        startTime = 0;
        StartCoroutine(TouchUpdate());
        Input.multiTouchEnabled = false;
	}
    public void Reset()
    {
        touchLength = 0.0f;
        touchTime   = 0.0f;
        chargeTime  = 0.0f;          
        touchDirection = Vector2.zero;
        State = (int)TouchState.NONE;
    }
    private void Update()
    {
        //touchCount:タッチした指の数
        if (Input.touchCount > 0)
        {

            touch = Input.GetTouch(0);
            touchPhase = touch.phase;
            switch (touch.phase)
            {
                case TouchPhase.Began:
                    //
                    startTime = Time.time;
                    startPosition = touch.position;
                    isTouch = true;
                    break;
                case TouchPhase.Moved:

                    touchTime = Time.time - startTime;
                    touchPosition = touch.position;
                    touchDirection = touchPosition - startPosition;
                    touchLength = Vector2.Distance(startPosition, touchPosition);

                    if (touchLength < 50.0f)
                    {

                    }
                    if (touchTime >= 0.3f)
                    {
                        State = Move;
                    }

                    break;
                case TouchPhase.Stationary:
                    //TODO :白猫のスキルを使うときみたいなアレを作る

                    chargeTime = Time.time - startTime;

                    break;
                case TouchPhase.Ended:
                    touchPosition = touch.position;

                    touchDirection = touchPosition - startPosition;
                    touchLength = Vector2.Distance(startPosition, touchPosition);

                    //タップかフリックかの判定
                    if (touchTime < 0.3f)
                    {
                        State = (touchLength < 100.0f) ? Tap : Flick;
                    }
                    isTouch = false;
                    break;
            }
        }else Reset();
    }

    IEnumerator TouchUpdate()
    {
        yield return null;  //ダミー
        //while (true)
        //{
        //    //touchCount:タッチした指の数
        //    if (Input.touchCount > 0)
        //    {

        //        touch = Input.GetTouch(0);
        //        touchPhase = touch.phase;
        //        switch(touch.phase)
        //        {
        //            case TouchPhase.Began:
        //                //
        //                startTime = Time.time;
        //                startPosition = touch.position;
        //                isTouch = true;
        //                break;
        //            case TouchPhase.Moved:

        //                touchTime = Time.time - startTime;
        //                touchPosition = touch.position;
        //                touchDirection = touchPosition - startPosition;
        //                touchLength = Vector2.Distance(startPosition, touchPosition);

        //                if(touchLength < 50.0f)
        //                {

        //                }
        //                if ( touchTime >= 0.3f )
        //                {
        //                    State = Move;
        //                }

        //                break;
        //            case TouchPhase.Stationary:
        //                //TODO :白猫のスキルを使うときみたいなアレを作る

        //                chargeTime = Time.time - startTime;
                        
        //                break;
        //            case TouchPhase.Ended:
        //                touchPosition = touch.position;

        //                touchDirection = touchPosition - startPosition;
        //                touchLength = Vector2.Distance(startPosition, touchPosition);

        //                //タップかフリックかの判定
        //                if (touchTime < 0.3f)
        //                {
        //                    State = (touchLength < 100.0f) ? Tap : Flick;
        //                }
        //                isTouch = false;
        //                break;
        //        }
        //        ////触れた時
        //        //if (touch.phase == TouchPhase.Began)
        //        //{
        //        //    startTime = Time.time;
        //        //    startPosition = touch.position;
        //        //    Reset();
        //        //    touchFlg = true;
        //        //    yield return null;
        //        //}

        //        //touchTime = Time.time - startTime;
        //        //touchDirection = touch.position - startPosition;

        //        //if (touch.phase == TouchPhase.Ended)
        //        //{
        //        //    State = (GetLength() < 20.0f) ?
        //        //        (int)TouchState.Tap : (touchTime < 0.5f) ?
        //        //                       (int)TouchState.Flick : (int)TouchState.NONE;
        //        //    touchFlg = false;
        //        //    yield return null;
        //        //    continue;
        //        //}

        //        //if( touch.phase == TouchPhase.Moved)
        //        //{

        //        //}

        //        //if(touch.phase == TouchPhase.Stationary)
        //        //{

        //        //}
        //        //else if (GetLength() > 20.0f && State == (int)TouchState.Charge)
        //        //{
        //        //    State = (int)TouchState.ChargeMove;
        //        //}
        //        //else if (GetLength() > 20.0f && State != (int)TouchState.ChargeMove)
        //        //{
        //        //    State = (int)TouchState.Move;
        //        //    touchLength = GetLength();
        //        //}
        //        //else if (touchTime > 0.15f)
        //        //{
        //        //    chargeTime = Time.time - startTime;
        //        //    if (GetLength() < 20.0f && State != (int)TouchState.Move)
        //        //    {
        //        //        State = (State != (int)TouchState.Tap) ?
        //        //            (int)TouchState.Charge : (int)TouchState.Tap;

        //        //    }
        //        //}

        //    }
        //    else Reset();
        //    yield return null;
        //}

    }
}
