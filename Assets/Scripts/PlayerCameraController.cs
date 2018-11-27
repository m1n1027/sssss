using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCameraController : MonoBehaviour {
    private punikon ogtInput;
    [SerializeField, Range(1, 50)]
    private float _MaxLength = 50;
    [SerializeField, Range(1, 50)]

    private float _MaxHeight = 50;

    [Header("デバッグ用")]
    [SerializeField, Range(1, 50)]
    public float SetLength = 50;
    [SerializeField, Range(1, 50)]
    public float SetHeight = 50;

    [SerializeField, Range(0.01f, 1.0f)]
    private float Speed = 0.1f;

    private float _Length = 1;
    private float _Height = 1;

    [SerializeField]
    public GameObject targetCamera;

    [System.NonSerialized]
    public int state;
    [System.NonSerialized]
    public int player_move = 1;
    [System.NonSerialized]
    public int camera_move = 2;
    [System.NonSerialized]
    public int none = 0;

    private CameraManager cameraManager;

    public float Length
    {
        get { return _Length; }
        set
        {
            var val = (_MaxLength < value) ?
                _MaxLength : (0 > value) ?
                0 : value;

            _Length = val;
        }
    }

    public float Height
    {
        get { return _Height; }
        set
        {
            var val = (_MaxHeight < value) ?
                _MaxHeight : (0 > value) ?
                0 : value;

            _Height = val;
        }
    }

    void OnValidate()
    {
        Length = SetLength;
        Height = SetHeight;

    }
    // Use this for initialization
    IEnumerator Start()
    {
        cameraManager = targetCamera.GetComponent<CameraManager>();
        cameraManager._target = gameObject;
        ogtInput = FindObjectOfType<punikon>();

        #region 一応のこしとく
        //while (true)
        //{
        //    //cameraManager.transform.position = transform.position;
        //    var vec = cameraManager.GetBehindThePlayer(gameObject, Speed, Length, Height);
        //    cameraManager.transform.position = vec;
        //    #region foo0
        //    //for (int i = 1; i <= 10; i++)
        //    //{
        //    //    transform.rotation = Quaternion.Lerp(cameraManager.transform.rotation, transform.rotation, i*0.1f);
        //    //    yield return null;
        //    //}
        //    //switch (state)
        //    //{
        //    //    case 0:
        //    //        cameraManager.transform.position = cameraManager.GetBehindThePlayer(gameObject, Speed, Length, Height);
        //    //        var targetRotation = Quaternion.LookRotation(transform.position - cameraManager.transform.position);
        //    //        var dir = transform.position - cameraManager.transform.position;
        //    //        cameraManager.transform.LookAt(transform);
        //    //        break;
        //    //    case 1:

        //    //        break;
        //    //    case 2:
        //    //        cameraManager.SetBehindThePlayer(gameObject, Speed, _Length, _Height);
        //    //        state = -1;
        //    //        break;
        //    //    case -1:
        //    //        if (cameraManager.isMoved) state = 0;                   
        //    //        break;
        //    //}

        //    //yield return null;
        //    #endregion
        //    yield return StartCoroutine(foo());
        //}
        #endregion

        yield return null;
    }
    void FixedUpdate()
    {
        var pos = cameraManager.GetBehindThePlayer(gameObject, Speed, 25, 25);
        var targetRotation = Quaternion.LookRotation(transform.position - pos);

        cameraManager.transform.position = Vector3.Lerp(cameraManager.transform.position, pos, Time.deltaTime * 1.0f);
        //cameraManager.transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, Time.deltaTime * 10.0f);

    }

    private void Update()
    {
        cameraManager.transform.LookAt(transform);
    }
    //*************************************

    //*************************************


}
