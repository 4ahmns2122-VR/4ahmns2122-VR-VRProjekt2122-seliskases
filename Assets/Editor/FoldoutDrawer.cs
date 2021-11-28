#if UNITY_EDITOR
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
 
public class FoldoutDrawer : MaterialPropertyDrawer
{
    public string argHeader;
    public bool argStartFoldoutGroup = false;
    public bool bElementDrawout;
    
    public static Dictionary<string, bool> isFoldedOut = new Dictionary<string, bool>();
    public static Dictionary<string, string> argCondition = new Dictionary<string, string>();

    public FoldoutDrawer(string startFoldoutGroup, string header, string condition)
    { 
        argHeader = header;
        argCondition.Add(header, condition);

        if(startFoldoutGroup == "StartFoldoutGroup")
            argStartFoldoutGroup = true;
    }

    public FoldoutDrawer(string startFoldoutGroup, string header)
    {
        argHeader = header;
        argCondition.Add(header, "");

        if (startFoldoutGroup == "StartFoldoutGroup")
            argStartFoldoutGroup = true;
    }
    
    public FoldoutDrawer(string header)
    {
        argHeader = header;
    }

    public override void OnGUI(Rect position, MaterialProperty prop, string label, MaterialEditor editor)
    {
        bElementDrawout = true;
        for (int i = 0; i < editor.targets.Length; i++)
        {
            Material mat = editor.targets[i] as Material;
            if (mat != null && argCondition[argHeader] != "")
            {
                bElementDrawout = mat.IsKeywordEnabled(argCondition[argHeader]);
            }

            if (bElementDrawout)
            {
                if (!isFoldedOut.ContainsKey(argHeader))
                    isFoldedOut.Add(argHeader, false);

                if (argStartFoldoutGroup)
                    isFoldedOut[argHeader] = EditorGUILayout.Foldout(isFoldedOut[argHeader], argHeader);

                if (isFoldedOut[argHeader])
                    editor.DefaultShaderProperty(prop, label);
            }
        }
    }

    public override float GetPropertyHeight(MaterialProperty prop, string label, MaterialEditor editor)
    {
        return -EditorGUIUtility.standardVerticalSpacing;
    }
}

#endif