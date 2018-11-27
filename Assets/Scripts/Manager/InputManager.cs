using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InputManager : MonoBehaviour {
 //   [SerializeField] 
 //   private GameObject obj = null;
 //   private Text text;
 //   [SerializeField]
 //   private GameObject obj2 = null;
 //   private Text text2;

 //   TouchManager _Touch = new TouchManager();


 //   [Range(0.02f, 0.15f)] 
 //   float _TapTime = 0.06f;

 //   [Range(0.08f, 0.2f)] 
 //   public float _FlickTime = 0.10f;

 //   [SerializeField]
 //   GameObject _MoveText=null;
 //   [SerializeField]
 //   GameObject _SpawnPoint =null;

 //   public bool GetTouchFlg() { return _Touch._touchFlg; }    //タッチしているかどうか      
 //   public Vector2 GetTouchDirRaw() { return _Touch._direction; } //normalizeしてないDir
 //   public Vector2 GetTouchDir() { return (_Touch._direction).normalized; } //normalize前
 //   public float GetTouchLength() { return _Touch._touchLength; }
 //   public float GetTouchTime() { return _Touch._touchTime; }  //タッチしている時間
 //   public float GetTouchEndTime() { return _Touch._endTime; } //タッチしてから離すまでにかかった時間(次にタッチされてから話されるまで保持する)
 //   public bool  GetTouch(TouchPhase State)
 //   {
 //        return (_Touch._touchState == State)?true:false;
 //       //return false;
 //   }
	//// Use this for initialization
	//IEnumerator Start () {
 //       text = obj.GetComponent<Text>();
 //       text2 = obj2.GetComponent<Text>();
 //       _Touch.Clear();
 //       StartCoroutine(touchAction());
 //       while(true)
 //       {


 //           yield return null;
 //       }
	//}
	
	//// Update is called once per frame
	//void Update () {
    //    //_Touch.Update();



    //}
    //IEnumerator touchAction()
    //{
    //    int num = 0;
    //    while(true)
    //    {
    //        _Touch.Update();
    //        if (_Touch._touchFlg)
    //        {
    //            var ob = Instantiate(_MoveText);
    //            ob.transform.parent = FindObjectOfType<Canvas>().transform;
    //            ob.transform.position = _SpawnPoint.transform.position;
    //            moveText mt = ob.GetComponent<moveText>();
    //            //else if (GetTouchTime() >= _FlickTime) text.text = "ムーブ";
    //            switch(_Touch._touchState)
    //            {
    //                case TouchPhase.Began: //タッチしているが動かしていない時

    //                    break;
    //                case TouchPhase.Moved: 

    //                    break;
    //                case TouchPhase.Ended:
    //                    if (GetTouchTime() < _FlickTime && GetTouchLength() > 40)
    //                    {

    //                        mt.SetText("フリック");
    //                    }
    //                    else if (GetTouchTime() < _TapTime)
    //                    {
    //                        num++;
    //                        mt.SetText("タップ");
    //                    }
    //                    break;
                                       
    //            }
    //            mt.StartMove();

    //        }
    //        yield return null;;
    //        //else text.text = "";
    //        text2.text = ""+_Touch._touchState + " TapNum:"+ num;
    //        yield return null;
    //    }


    //}
}
