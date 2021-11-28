# if UNITY_EDITOR

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class FoldoutIfTextureNotSet : MaterialPropertyDrawer
{
    public bool bElementDrawout;
    public string argTexture;
    public string argHeader;

    public FoldoutIfTextureNotSet(string texture, string header)
    {
        argTexture = texture;
        argHeader = header;
    }

    public override void OnGUI(Rect position, MaterialProperty prop, string label, MaterialEditor editor)
    {
        for (int i = 0; i < editor.targets.Length; i++)
        {
            bElementDrawout = true;
            Material mat = editor.targets[i] as Material;
            bool textureUsed = mat.GetTexture(argTexture) != null;

            if (mat != null && FoldoutDrawer.argCondition[argHeader] != "")
            {
                bElementDrawout = mat.IsKeywordEnabled(FoldoutDrawer.argCondition[argHeader]);
            }

            if (textureUsed)
            {
                return;
            }

            if (bElementDrawout)
            {
                if (FoldoutDrawer.isFoldedOut[argHeader])
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