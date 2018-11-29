using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum TouchState : int
{
    NONE       = -1,    //タッチされていない状態
    Tap        =  0,    
    Move       =  1,    //スワイプ
    Flick      =  2,    
    Charge     =  3,    //ずっとタッチされた状態で一定時間経ったとき(一定時間たつ前にスワイプするとMove)
    ChargeMove =  4     //チャージ状態からのスワイプ
}

[CreateAssetMenu(fileName = "TouchParam", menuName = "OGT Create/TouchParam")]
public class TouchParam : ScriptableObject {
    #region Instance
    [SerializeField,Header("Resourcesフォルダに置く ( Resources / ??? )")]
    public static string FILE_PATH = "Touch/TouchParam";
    private static TouchParam _Instance;

    public static TouchParam GetInstance
    {
        get
        {
            if(_Instance == null)
            {
                var param = (TouchParam)Resources.Load(FILE_PATH);
                if (param == null) Debug.LogError("指定のパスにTouchParamが存在しません");
                else _Instance = param;
            }
            return _Instance;
        }
    }
    #endregion

    #region Variable

    //タッチしているかどうか
    public bool isTouch = false;
    
    public TouchState touchState = TouchState.NONE;

    //タッチ開始時のポジション(Screen座標)
    public Vector2 startPosition;

    //現在の指のポジション(Screen座標)
    public Vector2 touchPosition;

    //開始位置から現在のタッチ位置のベクトル
    public Vector2 touchDirection;

    //開始位置から現在のタッチ位置までの距離(Screen座標)
    public float touchLength;

    //タッチされた時点でのゲーム開始からの時間(秒)
    public float startTime;

    //タッチ開始時からの経過時間(秒)
    public float touchTime;

    //タップかフリックかの判定用(Screen座標)
    //開始位置から現在のタッチ位置の距離がflickLength以上はなれているとフリック
    public readonly float flickLength = 100.0f;

    //フリックと移動(Move)の判定用(Screen座標)
    //タッチ開始からflickTime秒以上タッチしていると移動(Move)
    public readonly float flickTime = 0.3f;


    #endregion

    public void Reset()
    {
        touchLength = 0.0f;
        touchTime = 0.0f;
        //chargeTime = 0.0f;
        touchDirection = Vector2.zero;
        touchState = TouchState.NONE;
    }
}
