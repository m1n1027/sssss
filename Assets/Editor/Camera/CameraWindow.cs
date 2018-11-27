using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
public class CameraWindow : EditorWindow
{

    CameraParamTable table;
    string FILE_PATH = "";
    float t;
    [MenuItem(itemName: "OGT/Setting/Camera/つぎ/これのつぎ/もう1個つぎ/そろそろでてくる/あともうちょい/もうすぐでる/カメラシェイク")]
    static void Create()
    {
        var window = GetWindow<CameraWindow>("カメラシェイク");
        window.minSize = new Vector2(640, 480);
    }


    void OnGUI()
    {


        EditorGUILayout.BeginHorizontal(GUI.skin.box);
        {
            EditorGUILayout.BeginVertical(GUI.skin.box);
            {
                EditorGUILayout.LabelField("[ F i l e ]");
                //table = (CameraParamTable)EditorGUILayout.ObjectField(table, typeof(CameraParamTable), true);
                table = CameraParamTable.GetInstance;
                //var script = MonoScript.FromScriptableObject(table);
                FILE_PATH = AssetDatabase.GetAssetPath(table);
                if (table != null) EditorGUILayout.LabelField(("THIS FILE PATH:" + FILE_PATH));
                else EditorGUILayout.Space();

            }
            EditorGUILayout.EndVertical();

        }
        EditorGUILayout.EndHorizontal();
        if (table == null) return;

        EditorGUILayout.BeginVertical(GUI.skin.box);
        {
            EditorGUILayout.LabelField("[ 振れ幅の調整 ]");
            table.shakeHeight = EditorGUILayout.Slider("上下", table.shakeHeight, 0, 2);
            table.shakeWidth = EditorGUILayout.Slider("左右", table.shakeWidth, 0, 2);
            EditorGUILayout.Space();
            EditorGUILayout.LabelField("[ 画面の揺れの強さ ]");
            table.shakePower = EditorGUILayout.Slider("強さ", table.shakePower, 0, 5);
            EditorGUILayout.Space();
            EditorGUILayout.LabelField("[ 次の揺れまでの間隔 ]");
            table.shakeInterval = EditorGUILayout.Slider("間隔", table.shakeInterval, 0, 1);

        }

        if (GUILayout.Button("保存"))
        {
            EditorUtility.SetDirty(table);
            AssetDatabase.SaveAssets();

        }
        EditorGUILayout.EndVertical();

    }
}
