#if UNITY_EDITOR
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class FoldoutDrawer : MaterialPropertyDrawer
{
    public string argHeader;
    public string argCondition;
    public bool argStartFoldoutGroup = false;
    public bool bElementDrawout;
    public static bool isFoldedOut = true;

    public FoldoutDrawer(string startFoldoutGroup, string header, string condition)
    {
        argHeader = header;
        argCondition = condition;

        if(startFoldoutGroup == "StartFoldoutGroup")
        {
            argStartFoldoutGroup = true;
        }
    }

    public FoldoutDrawer(string header, string condition)
    {
        argHeader = header;
        argCondition = condition;
    }

    public override void OnGUI(Rect position, MaterialProperty prop, string label, MaterialEditor editor)
    {
        bElementDrawout = true;
        for (int i = 0; i < editor.targets.Length; i++)
        {
            Material mat = editor.targets[i] as Material;
            if (mat != null)
            {
                bElementDrawout = mat.IsKeywordEnabled(argCondition);
            }
        }

        if (bElementDrawout)
        {
            if (argStartFoldoutGroup)
                isFoldedOut = EditorGUILayout.Foldout(isFoldedOut, argHeader);

            if (isFoldedOut)
                editor.DefaultShaderProperty(prop, label);
        }
    }

    public override float GetPropertyHeight(MaterialProperty prop, string label, MaterialEditor editor)
    {
        //@TODO: manually standardise element compaction
        //     float height = base.GetPropertyHeight (prop, label, editor);
        //     return bElementHidden ? 0.0f : height-16;

        return 0;
    }
}

#endif