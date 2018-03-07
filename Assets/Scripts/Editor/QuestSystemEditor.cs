// --------------------------------------------------------------------------------------------------------------------
// <copyright file="QuestSystemEditor.cs" by="Akapagion">
//  © Copyright Dauler Palhares da Costa Viana 2018.
//          http://github.com/DaulerPalhares
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

using System.Collections.Generic;
using System.Linq;
using QuestSystem;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(QuestData))]
public class QuestSystemEditor : Editor
{
    private QuestData _target;

    private bool Help;
    private bool Quests;
    private int SelectedQuestID;
    private bool SelectedQuest;
    private bool ShowAllQuest;
    private bool editQuestIdentifier;
    private bool ShowAllObjective;

    private bool ShowNewQuestEditor;
    private bool ShowObjectiveEditor;
    ////////////////////////////
    private bool haveContinuation;
    private int chain;
    private bool havePreviusQuest;
    private int previus;

    private string qName;
    private string qDescript;
    private string qHint;
    private List<QuestObjective> qObjectives;
    private enum QuestType { Gather, Kill}
    private QuestType qType;
    private string qobjText;
    private int qobjTotal;
    private string qobjDescript;
    private bool qobjBonus;
    private GameObject qobjItem;

    private GUIStyle TitleStyle = new GUIStyle();
    private GUIStyle SubTiyleStyle = new GUIStyle();

    private void ConstructStyles()
    {
        TitleStyle.fontSize = 24;
        TitleStyle.fontStyle = FontStyle.Bold;
        TitleStyle.normal.textColor = Color.yellow;

        SubTiyleStyle.fontSize = 18;
        SubTiyleStyle.fontStyle = FontStyle.Bold;
    }

    private void OnEnable()
    {
        EditorUtility.SetDirty(target);
        ConstructStyles();
        _target = (QuestData) target;
        Help = false;
        Quests = false;
        SelectedQuestID = 0;
        SelectedQuest = false;
        ShowAllQuest = false;
        qObjectives = new List<QuestObjective>();
    }

    public override void OnInspectorGUI()
    {
        GUILayout.Label(Resources.Load("QuestSystem/Logo") as Texture);      
        EditorGUILayout.BeginHorizontal();
        if (GUILayout.Button("Show Help"))
        {
            Help = true;
        }
        if (GUILayout.Button("Show QuestEditor"))
        {
            Quests = true;
            ShowAllQuest = true;
        }
        EditorGUILayout.EndHorizontal();
        if (Help)
        {
            GUILayout.Label("Information");
            if (GUILayout.Button("Close Help"))
            {
                Help = false;
            }
        }
        if (Quests)
        {
            EditorGUILayout.Space();
            EditorGUILayout.Space();
            EditorGUILayout.Space();
            if (_target.Quests.Count == 0)
            {
                GUILayout.Label("First quest to add",SubTiyleStyle);
                AddNewQuest();
            }
            if (ShowAllQuest && _target.Quests.Count > 0)
            {
                GUILayout.Label("Quest Table");
                ShowAllQuests();
                if (GUILayout.Button("+"))
                {
                    SelectedQuest = false;
                    ShowNewQuestEditor = true;
                }
            }
            if (ShowNewQuestEditor)
            {
                GUILayout.Label("Configure the new quest", SubTiyleStyle);
                AddNewQuest();
            }
            if (SelectedQuest && !ShowNewQuestEditor)
            {
               var tempSelectedQuest = _target.Quests[SelectedQuestID];
               ShowSelectedQuest(tempSelectedQuest);               
            }
        }
    }

    /// <summary>
    /// Show all quests avaliable in the inspector.
    /// </summary>
    private void ShowAllQuests()
    {
        EditorGUILayout.BeginVertical();
        for (var i = 0; i < _target.Quests.Count; i++)
        {
            var quest = _target.Quests[i];
            EditorGUILayout.BeginHorizontal();
            if (GUILayout.Button("Select"))
            {
                SelectedQuestID = i;
                SelectedQuest = true;
            }
            GUILayout.Label("ID: " + quest.Identifier.ID + " | ");
            GUILayout.Label(quest.Text.Title + " | ");
            GUILayout.Label("Require:" + quest.Identifier.PreviusID + " | ");
            GUILayout.Label("Next:" + quest.Identifier.ChainQuestID + " | ");
            if (GUILayout.Button("X"))
            {
                SelectedQuest = false;
                _target.Quests.RemoveAt(i);
            }
            EditorGUILayout.EndHorizontal();
        }
        EditorGUILayout.EndVertical();
    }

    private void ShowALlObjectivesFromSelectedQuest(List<QuestObjective> obj)
    {
        List<QuestType> qTypes = new List<QuestType>();
        foreach (var questObjective in obj)
        {
            qTypes.Add(questObjective is CollectionObjective ? QuestType.Gather : QuestType.Kill);
        }
        EditorGUILayout.Space();
        for (var index = 0; index < obj.Count; index++)
        {
            var questObjective = obj[index];
            EditorGUILayout.BeginHorizontal();
            GUILayout.FlexibleSpace();
            GUILayout.Label("Objective nº" + (index+1),SubTiyleStyle);
            GUILayout.FlexibleSpace();
            EditorGUILayout.EndHorizontal();
            EditorGUILayout.BeginHorizontal();
            switch (qTypes[index])
            {
                case QuestType.Gather:
                    CollectionObjective tempCollect = (CollectionObjective)questObjective;
                    tempCollect.Verb = EditorGUILayout.TextField("Action", tempCollect.Verb, GUILayout.ExpandWidth(false));
                    EditorGUILayout.EndHorizontal();
                    tempCollect.CollectionAmount = EditorGUILayout.IntField("Total to " + tempCollect.Verb, tempCollect.CollectionAmount);
                    tempCollect.Description = EditorGUILayout.TextField("What do you need to " + tempCollect.Title,tempCollect.Description);
                    tempCollect.ItemToCollect = EditorGUILayout.ObjectField(tempCollect.ItemToCollect, typeof(GameObject), true) as GameObject;
                    tempCollect.IsBonus = EditorGUILayout.Toggle("Bonus Objective:", tempCollect.IsBonus);
                    break;
                case QuestType.Kill:
                    EditorGUILayout.LabelField("NEED TO IMPLEMENT SORRY");
                    EditorGUILayout.EndHorizontal();
                    break;
            }
            if (obj.Count > 1)
            {
                if (GUILayout.Button("Remove this Objective"))
                {
                    qObjectives.Remove(questObjective);
                }
            }
        }
        EditorGUILayout.Space();
        EditorGUILayout.Space();
        EditorGUILayout.Space();
        AddNewObjective();
    }

    private void ShowSelectedQuest(Quest q)
    {
        chain = q.Identifier.ChainQuestID;
        previus = q.Identifier.PreviusID;
        EditorGUILayout.BeginVertical();
            EditorGUILayout.BeginHorizontal();
            GUILayout.FlexibleSpace();
                GUILayout.Label("Selected Quest: " + q.Text.Title,TitleStyle);
            GUILayout.FlexibleSpace();
            EditorGUILayout.EndHorizontal();
            EditorGUILayout.BeginHorizontal();
                haveContinuation = EditorGUILayout.Toggle("Have Chain?", haveContinuation);
                if (haveContinuation)
                {
                    chain = EditorGUILayout.IntField("Next Quest ID", chain);
                    q.Identifier.SetChainQuestID(chain);
                }
            EditorGUILayout.EndHorizontal();
            EditorGUILayout.BeginHorizontal();
                havePreviusQuest = EditorGUILayout.Toggle("Have PRQeust?", havePreviusQuest); if (havePreviusQuest)
                {
                    previus = EditorGUILayout.IntField("Previus Quest ID", previus);
                    q.Identifier.SetSourceID(previus);
                }
            EditorGUILayout.EndHorizontal();
            ShowALlObjectivesFromSelectedQuest(q.Objectives);
        EditorGUILayout.EndVertical();
    }

    private void AddNewQuest()
    {
        EditorGUILayout.BeginHorizontal();
        qName = EditorGUILayout.TextField("Quest Name:", qName);
        if (qObjectives.Count > 0)
        {
            if (GUILayout.Button("Add this quest"))
            {
                var exampleQuestIdentifier = new QuestIdentifier(_target.Quests.Count);
                var exampleQuestText = new QuestText(qName, qDescript, qHint);
                var exampleQuest = new Quest(exampleQuestIdentifier, exampleQuestText, qObjectives);
                _target.Quests.Add(exampleQuest);
                qObjectives = new List<QuestObjective>();
                ShowNewQuestEditor = false;
            }
        }
        EditorGUILayout.EndHorizontal();
        qDescript = EditorGUILayout.TextField("Quest Description:", qDescript);
        qHint = EditorGUILayout.TextField("Quest Hint:", qHint);
        if (qObjectives.Count > 0)
        {
            ShowAllObjective = EditorGUILayout.Toggle("Show Objectives", ShowAllObjective);
            if (ShowAllObjective)
            {
                ShowALlObjectivesFromSelectedQuest(qObjectives);
                
            }
        }
        else
        {
            AddNewObjective();
        }
    }

    private void AddNewObjective()
    {
        GUILayout.Label("New Quest objective",SubTiyleStyle);

        qType = (QuestType)EditorGUILayout.EnumPopup(qType);
        switch (qType)
        {
            case QuestType.Gather:
                qobjText = EditorGUILayout.TextField("Verb of action", qobjText);
                qobjTotal = EditorGUILayout.IntField("Total to " + qobjText, qobjTotal);
                qobjDescript = EditorGUILayout.TextField("What do you need to " + qobjText, qobjDescript);
                qobjItem = EditorGUILayout.ObjectField(qobjItem, typeof(GameObject), true) as GameObject;
                qobjBonus = EditorGUILayout.Toggle("Bonus Objective:", qobjBonus);

                if (GUILayout.Button("Add New Objective"))
                {
                    qObjectives.Add(new CollectionObjective(qobjText,qobjTotal,qobjItem,qobjDescript,qobjBonus));
                }

                break;
            case QuestType.Kill:
                EditorGUILayout.LabelField("NEED TO IMPLEMENT SORRY");
                break;
        }

    }
}