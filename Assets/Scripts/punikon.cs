using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class punikon : MonoBehaviour {
    private TouchPhase touchPhase;
    private Touch touch;
    private TouchParam touchParam;
    public Touch GetTouch() { return touch; }
    
 

    // Use this for initialization
    void Awake () {
        touchParam = TouchParam.GetInstance;
        touchParam.isTouch = false;
        touchParam.startTime = 0;

        Input.multiTouchEnabled = false;
	}

    private void Update()
    {
        //touchCount:タッチした指の数
        if (Input.touchCount > 0)
        {
            //タッチ状態の取得
            touch = Input.GetTouch(0);
            //画面へのタッチ状態
            touchPhase = touch.phase;

            switch (touch.phase)
            {
                case TouchPhase.Began:
                    //
                    touchParam.startTime = Time.time;
                    touchParam.startPosition = touch.position;
                    touchParam.isTouch = true;
                    break;
                case TouchPhase.Moved:

                    touchParam.touchTime = Time.time - touchParam.startTime;
                    touchParam.touchPosition = touch.position;

                    touchParam.touchDirection = (touchParam.touchPosition - touchParam.startPosition).normalized;
                    touchParam.touchLength = Vector2.Distance(touchParam.startPosition, touchParam.touchPosition);

                    if (touchParam.touchLength < touchParam.flickLength)
                    {

                    }
                    if (touchParam.touchTime >= touchParam.flickTime)
                    {
                        touchParam.touchState = TouchState.Move;
                    }

                    break;
                case TouchPhase.Stationary:
                    //TODO :白猫のスキルを使うときみたいなアレを作る

                    //chargeTime = Time.time - startTime;

                    break;
                case TouchPhase.Ended:
                    touchParam.touchPosition = touch.position;

                    touchParam.touchDirection = (touchParam.touchPosition - touchParam.startPosition).normalized;
                    touchParam.touchLength = Vector2.Distance(touchParam.startPosition, touchParam.touchPosition);

                    //タップかフリックかの判定
                    if (touchParam.touchTime < 0.3f)
                    {
                        touchParam.touchState = (touchParam.touchLength < touchParam.flickLength) ? TouchState.Tap : TouchState.Flick;
                    }
                    touchParam.isTouch = false;
                    break;
            }
        }else touchParam.Reset();
    }
 
}
