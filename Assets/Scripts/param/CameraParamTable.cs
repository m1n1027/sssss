using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewCameraParamTable", menuName = "OGT Create/CameraParam")]
public class CameraParamTable : ScriptableObject
{

    #region シングルトン
    // なんかいい方法考え中
    static readonly string FILE_PATH = "Camera/cameraParam";
    private static CameraParamTable _Instance;
    public static CameraParamTable GetInstance
    {
        get
        {
            if (_Instance == null)
            {
                var param = (CameraParamTable)Resources.Load(FILE_PATH);
                if (param == null) Debug.LogError("Camera の パラメーターファイルが無い無い無い内々ない");
                _Instance = param;
            }
            return _Instance;
        }
    }
    #endregion


    //左右の振れ幅
    [SerializeField]
    public float shakeWidth = 1.0f, shakeHeight = 1.0f;
   
    [SerializeField]
    public float shakePower = 0.05f;

    [SerializeField]
    public float shakeInterval = 0.05f;
    


}
