// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ColorPalletEditor.cs" by="Akapagion">
//  © Copyright Dauler Palhares da Costa Viana 2018.
//          http://github.com/DaulerPalhares
// </copyright>
// --------------------------------------------------------------------------------------------------------------------
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(RarityController))]
public class RarityContollerEditor : Editor
{
    private RarityController _target;
    private bool ShowInspectorColors;
    private bool ShowInspectorUnc;
    private bool ShowInspectorRare;
    private bool ShowInspectorUnique;
    private double TotalProbility = 0;

    private void OnEnable()
    {
        _target = (RarityController) target;
    }

    public override void OnInspectorGUI()
    {
        ShowInspectorColors = EditorGUILayout.Foldout(ShowInspectorColors, "Pallet with Probability"); if (ShowInspectorColors)
        {
            TotalProbility = 0;
            for (int i = 0; i < _target.Colors.Count; i++)
            {
                EditorGUILayout.BeginHorizontal();
                _target.Colors[i].Key = EditorGUILayout.ColorField(_target.Colors[i].Key);
                _target.Colors[i].Value = EditorGUILayout.DoubleField("Probability", _target.Colors[i].Value);
                _target.Colors[i].RarityName = EditorGUILayout.TextField(_target.Colors[i].RarityName);
                EditorGUILayout.EndHorizontal();
                TotalProbility += _target.Colors[i].Value;
            }

            EditorGUILayout.BeginHorizontal();
            GUILayout.Label("Total Probility (cannot pass 100) :");
            GUILayout.Label((TotalProbility * 100).ToString());
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
        ShowInspectorUnc = EditorGUILayout.Foldout(ShowInspectorUnc, "Uncommon"); if (ShowInspectorUnc)
        {
            GUILayout.Label("Uncommon Prefixes");
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

        ShowInspectorRare = EditorGUILayout.Foldout(ShowInspectorRare, "Rare");
        if (ShowInspectorRare)
        {
            GUILayout.Label("Rare Prefixes");
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

        ShowInspectorUnique = EditorGUILayout.Foldout(ShowInspectorUnique, "Unique");
        if (ShowInspectorUnique)
        {
            GUILayout.Label("Unique Names");
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
