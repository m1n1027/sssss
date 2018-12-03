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

    [SerializeField, Range(0.01f, 1.0f)]
    private float Speed = 0.1f;

    [SerializeField, Range(1, 50)]
    private float _Length = 1;
    [SerializeField, Range(1, 50)]
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

    // Use this for initialization
    IEnumerator Start()
    {
        cameraManager = targetCamera.GetComponent<CameraManager>();
        cameraManager._target = gameObject;
        ogtInput = FindObjectOfType<punikon>();

        yield return null;
    }
    void FixedUpdate()
    {
        var pos = cameraManager.GetBehindThePlayer(gameObject, Speed, _Length, _Height);
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
