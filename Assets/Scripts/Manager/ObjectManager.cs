using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ObjectManager : MonoBehaviour {
 //   [SerializeField] private GameObject textObj = null;
 //   public int maxCount = 10;
 //   private List<GameObject> textPool = new List<GameObject>();
    

	//// Use this for initialization
	//void Start () {
 //       CreateNewTextPool();
	//}
	
	//// Update is called once per frame
	//void Update () {
 //       if(Input.GetKeyDown(KeyCode.Z))
 //       {
 //           //GameObject obj;
 //           //obj = getFreeTextObject();
 //           //if (obj != null) Debug.Log("空いてるから使う");
 //           //else Debug.Log("空いていない");
 //           PlayTextMove("tesuto");
 //       }
	//}

    //void CreateNewTextPool()
    //{
    //    for (int i = 0; i < maxCount;i++)
    //    {
    //        textPool.Add(new GameObject());
    //    }

    //    Text t = textObj.GetComponent<Text>();
    //    foreach(var obj in textPool)
    //    {
    //        obj.AddComponent<moveText>();
    //        obj.AddComponent<Text>();
    //        Text txt = obj.GetComponent<Text>();
    //        txt = t;
    //        obj.SetActive(false);
    //    }
    //}

    //GameObject getFreeTextObject()
    //{
    //    foreach(var obj in textPool)
    //    {
    //        if (obj.activeSelf == false)
    //        {
    //            obj.SetActive(true);
    //            return obj;
    //        }
    //    }

    //    return null;
    //}

    //public void PlayTextMove(string s)
    //{
    //    var useObject = getFreeTextObject();
    //    if(useObject!=null)
    //    {
            
    //        var mt = useObject.GetComponent<moveText>();
    //        mt.SetText(s);
    //        mt.StartMove();
    //    }

    //}
    
}
