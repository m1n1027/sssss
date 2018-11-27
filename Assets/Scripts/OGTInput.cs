using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OGTInput : MonoBehaviour
{
    private bool isTouch = false;       //タッチしてるかどうか

    //時間
    private float startTime = 0.0f;
    private float touchTime = 0.0f;

    //ポジション
    private Vector2 startPosition = Vector2.zero;
    private Vector2 touchPosition = Vector2.zero;
    private Vector2 touchDir = Vector2.zero;

    private float touchLength = 0.0f;

    //状態
    public int touchState = 0;
    public static readonly int NONE = -1, Tap = 0, Move = 1, Flick = 2, Charge = 3, ChargeMove = 4;

    /// <summary>
    /// タッチした瞬間はtrue 一定時間が経過しても指定された距離より伸ばさなかったらfalse
    /// </summary>
    private bool isCharge;

    private Touch touch;
    private TouchPhase touchphase;
    public TouchPhase GetPhase() { return touchphase; }
    public Touch GetTouch() { return touch; }

    public int GetState() { return touchState; }
    public Vector2 GetDirection() { return touchDir; }

    void Start()
    {

    }

    void Update()
    {

            //タッチした時と離した時
            
        if(Input.touchCount > 0)
        {
            touch = Input.GetTouch(0);
            touchphase = touch.phase;

            //タッチ開始
            if (touch.phase == TouchPhase.Began)
            {
            
                isTouch = true;

                isCharge = true;
                startTime = Time.time;
                touchTime = startTime;

                startPosition = touch.position;
                touchPosition = startPosition;

                touchDir = Vector2.zero;
                touchLength = 0.0f;

                touchState = NONE;
            }
              
            //タッチ終了
            if(touch.phase == TouchPhase.Ended)
            {
                isTouch = false;
                //タッチ時間が0.5f以下だった場合
                //タップかフリック
                if(touchTime <= 0.5f)
                {
                    touchLength = Vector2.Distance(touchPosition, startPosition);
                    touchDir = touch.position - startPosition;
                    touchState = (touchLength > 50.0f) ? Flick : Tap;
                }
            }

            if(touchphase == TouchPhase.Moved||touchphase==TouchPhase.Stationary)
            {
                touchTime = Time.time - startTime;
                touchLength = Vector2.Distance(touchPosition, startPosition);
                touchDir = touch.position - startPosition;

                if (touchTime <= 0.5f)
                {
                    isCharge = (isCharge && touchLength < 20.0f) ?
                        true : false;
                }
                if(touchTime > 0.5f)
                {
                    touchState = (isCharge) ? ChargeMove : Move;
                }
            }



               
            }
        }
}