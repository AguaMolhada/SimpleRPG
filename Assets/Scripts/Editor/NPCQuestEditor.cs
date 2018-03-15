// --------------------------------------------------------------------------------------------------------------------
// <copyright file="NPCQuestEditor.cs" by="Akapagion">
//  © Copyright Dauler Palhares da Costa Viana 2018.
//          http://github.com/DaulerPalhares
// </copyright>
// --------------------------------------------------------------------------------------------------------------------
using UnityEngine;
using System.Collections;
using UnityEditor;

[CustomEditor(typeof(NPCBasicInformationData))]
public class NPCQuestEditor : Editor
{
    private NPCBasicInformationData _target;
    private int _questID;
    
    private void OnEnable()
    {
        _target = (NPCBasicInformationData) target;
    }

    public override void OnInspectorGUI()
    {
        _target.NPCName = EditorGUILayout.TextField("Npc name:", _target.NPCName);
        _target.NpcGameobject = (GameObject) EditorGUILayout.ObjectField(_target.NpcGameobject, typeof(GameObject), false);
        EditorGUILayout.BeginHorizontal();
        _questID = EditorGUILayout.IntField("Quest id to add",_questID);
        if (GUILayout.Button("Add quest"))
        {   
            _target.QuestsToGive.Add(DatabaseControl.Instance.FetchQuest(_questID));
        }
        EditorGUILayout.EndHorizontal();
        GUILayout.Label("Quests to give:");
        for (var index = 0; index < _target.QuestsToGive.Count; index++)
        {
            var quest = _target.QuestsToGive[index];
            EditorGUILayout.BeginHorizontal(MyEditorStyles.BackgroundStyle(MyEditorStyles.ListColorsBG[index%2]));
            GUILayout.Label(quest.Text.Title);
            if (GUILayout.Button("X", GUILayout.Width(20)))
            {
                _target.QuestsToGive.Remove(quest);
            }
            EditorGUILayout.EndHorizontal();
        }
        EditorUtility.SetDirty(target);
        serializedObject.Update();
        Undo.RecordObject(_target, "Changing NPC");
        serializedObject.ApplyModifiedProperties();
    }
}
