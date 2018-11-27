using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class moveText : MonoBehaviour {
    public Text text;
    private string txt="";
	// Use this for initialization
    IEnumerator Start () {
        yield return null;
    }
    public void SetText(string t) { txt = ""+t; }
    public void StartMove() 
    {
        text = GetComponent<Text>();
        text.text = ""+txt;
        StartCoroutine(move());
    }
    IEnumerator move()
    {
        while (text.color.a >= 0.0f)
        {
            transform.position += Vector3.up;
            text.color += new Color(0, 0, 0, -0.05f);
            yield return null;
        }
        Destroy(gameObject);
        //gameObject.SetActive(false);
    }

	// Update is called once per frame

}
