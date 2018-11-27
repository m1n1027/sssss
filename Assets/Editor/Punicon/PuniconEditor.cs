using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
public class PuniconEditor : EditorWindow {
    [MenuItem("GameSetting/PuniconSetting")]
	public static void Create()
    {
        var window = GetWindow<PuniconEditor>("ぷにコン設定");
        window.minSize = new Vector2(640, 480);
    }
    private void OnGUI()
    {
        EditorGUILayout.BeginVertical(GUI.skin.box);

        EditorGUILayout.BeginVertical(GUI.skin.box);
        {
            EditorGUILayout.LabelField("GGGG");
        }
        EditorGUILayout.EndVertical();
  
        
        EditorGUILayout.BeginVertical(GUI.skin.box);
        {
            EditorGUILayout.LabelField("FFFF");
        }
        EditorGUILayout.EndVertical();

        EditorGUILayout.EndVertical();
    }
}
