// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ColorPalletEditor.cs" by="Akapagion">
//  © Copyright Dauler Palhares da Costa Viana 2018.
//          http://github.com/DaulerPalhares
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(RarityController))]
public class RarityContollerEditor : Editor
{
    /// <summary>
    /// Target to edit on the inspector.
    /// </summary>
    private RarityController _target;
    /// <summary>
    /// 
    /// </summary>
    private bool _showInspectorColors;
    private bool _showInspectorUnc;
    private bool _showInspectorRare;
    private bool _showInspectorUnique;
    private double _totalProbility = 0;

    private void OnEnable()
    {
        _target = (RarityController) target;
    }

    public override void OnInspectorGUI()
    {
        _showInspectorColors = EditorGUILayout.Foldout(_showInspectorColors, "Pallet with Probability"); if (_showInspectorColors)
        {
            _totalProbility = 0;
            if (_target.Colors == null)
            {
                _target.Colors =new List<ColorRarity>();
            }

            for (int i = 0; i < _target.Colors.Count; i++)
            {
                EditorGUILayout.BeginHorizontal();
                _target.Colors[i].Key = EditorGUILayout.ColorField(_target.Colors[i].Key);
                _target.Colors[i].Value = EditorGUILayout.DoubleField("Probability", _target.Colors[i].Value);
                _target.Colors[i].RarityName = EditorGUILayout.TextField(_target.Colors[i].RarityName);
                EditorGUILayout.EndHorizontal();
                _totalProbility += _target.Colors[i].Value;
            }

            EditorGUILayout.BeginHorizontal();
            GUILayout.Label("Total Probility (cannot pass 100) :");
            GUILayout.Label((_totalProbility * 100).ToString());
            EditorGUILayout.EndHorizontal();
            EditorGUILayout.BeginVertical();
            GUILayout.Label("Color Management");
            EditorGUILayout.BeginHorizontal();
            if (GUILayout.Button("Add"))
            {
                _target.Colors.Add(new ColorRarity());
            }

            if (_target.Colors.Count > 0)
            {
                if (GUILayout.Button("Remove"))
                {
                    _target.Colors.RemoveAt(_target.Colors.Count - 1);
                }
            }

            EditorGUILayout.EndHorizontal();
            EditorGUILayout.EndVertical();
        }
        _showInspectorUnc = EditorGUILayout.Foldout(_showInspectorUnc, "Uncommon"); if (_showInspectorUnc)
        {
            GUILayout.Label("Uncommon Prefixes");
            if (_target.UncommunPrefixs == null)
            {
                _target.UncommunPrefixs = new List<string>();
            }
            for (int i = 0; i < _target.UncommunPrefixs.Count; i++)
            {
                _target.UncommunPrefixs[i] = EditorGUILayout.TextField(_target.UncommunPrefixs[i]);
            }
            EditorGUILayout.BeginVertical();
            GUILayout.Label("Uncommon Management");
            EditorGUILayout.BeginHorizontal();
            if (GUILayout.Button("Add"))
            {
                _target.UncommunPrefixs.Add("");
            }

            if (_target.Colors.Count > 0)
            {
                if (GUILayout.Button("Remove"))
                {
                    _target.UncommunPrefixs.RemoveAt(_target.UncommunPrefixs.Count - 1);
                }
            }

            EditorGUILayout.EndHorizontal();
            EditorGUILayout.EndVertical();
        }

        _showInspectorRare = EditorGUILayout.Foldout(_showInspectorRare, "Rare");
        if (_showInspectorRare)
        {
            GUILayout.Label("Rare Prefixes");
            if (_target.RarePrefixs == null)
            {
                _target.RarePrefixs = new List<string>();
            }
            for (int i = 0; i < _target.RarePrefixs.Count; i++)
            {
                _target.RarePrefixs[i] = EditorGUILayout.TextField(_target.RarePrefixs[i]);
            }
            EditorGUILayout.BeginVertical();
            GUILayout.Label("Rare Management");
            EditorGUILayout.BeginHorizontal();
            if (GUILayout.Button("Add"))
            {
                _target.RarePrefixs.Add("");
            }
            if (_target.Colors.Count > 0)
            {
                if (GUILayout.Button("Remove"))
                {
                    _target.RarePrefixs.RemoveAt(_target.RarePrefixs.Count - 1);
                }
            }
            EditorGUILayout.EndHorizontal();
            EditorGUILayout.EndVertical();
        }

        _showInspectorUnique = EditorGUILayout.Foldout(_showInspectorUnique, "Unique");
        if (_showInspectorUnique)
        {
            GUILayout.Label("Unique Names");
            if (_target.UniqueNames == null)
            {
                _target.UniqueNames = new List<string>();
            }
            for (int i = 0; i < _target.UniqueNames.Count; i++)
            {
                _target.UniqueNames[i] = EditorGUILayout.TextField(_target.UniqueNames[i]);
            }
            EditorGUILayout.BeginVertical();
            GUILayout.Label("Rare Management");
            EditorGUILayout.BeginHorizontal();
            if (GUILayout.Button("Add"))
            {
                _target.UniqueNames.Add("");
            }
            if (_target.UniqueNames.Count > 0)
            {
                if (GUILayout.Button("Remove"))
                {
                    _target.UniqueNames.RemoveAt(_target.UniqueNames.Count - 1);
                }
            }
            EditorGUILayout.EndHorizontal();
            EditorGUILayout.EndVertical();
        }

    }
}
