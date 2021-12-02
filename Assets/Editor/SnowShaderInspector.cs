using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.Linq;

public class SnowShaderInspector : MaterialEditor
{
    public override void OnInspectorGUI()
    {
        // If we are not visible, return.
        if (!isVisible)
            return;

        // Get the current keywords from the material
        Material targetMat = target as Material;
        string[] keyWords = targetMat.shaderKeywords;

        bool noiseOffsetEnabled = keyWords.Contains("NOISEOFFSET_ON");
        bool omnidirectionalSnowEnabled = keyWords.Contains("OMNIDIRECTIONALSNOW_ON");

        EditorGUI.BeginChangeCheck();

        noiseOffsetEnabled = EditorGUILayout.Toggle("Noise Offset", noiseOffsetEnabled);
        omnidirectionalSnowEnabled = EditorGUILayout.Toggle("Omnidirectional Snow", omnidirectionalSnowEnabled);

        // Draw the default inspector.
        base.OnInspectorGUI();

        // If something has changed, update the material.
        if (EditorGUI.EndChangeCheck())
        {
            List<string> keywords = new List<string>();

            keywords.Add (noiseOffsetEnabled ? "NOISEOFFSET_ON" : "NOISEOFFSET_OFF");
            keywords.Add (omnidirectionalSnowEnabled ? "OMNIDIRECTIONALSNOW_ON" : "OMNIDIRECTIONALSNOW_OFF");

            targetMat.shaderKeywords = keywords.ToArray();
            EditorUtility.SetDirty(targetMat);
        }
    }
}