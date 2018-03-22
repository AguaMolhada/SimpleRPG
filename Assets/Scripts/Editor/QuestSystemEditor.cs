// --------------------------------------------------------------------------------------------------------------------
// <copyright file="QuestSystemEditor.cs" by="Akapagion">
//  © Copyright Dauler Palhares da Costa Viana 2018.
//          http://github.com/DaulerPalhares
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

using System;
using System.Collections.Generic;
using QuestSystem;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(QuestData))]
public class QuestSystemEditor : Editor
{
    private bool useDafultEditor;

    private QuestData _target;

    private bool Help;
    private bool Quests;
    private int SelectedQuestID;
    private bool SelectedQuest;
    private bool ShowAllQuest { get; set; }
    private bool editQuestIdentifier;
    private bool ShowAllObjective;

    private bool ShowNewQuestEditor;
    private bool ShowObjectiveEditor;
    ////////////////////////////
    private bool haveContinuation;
    private bool havePreviusQuest;

    private string qName;
    private string qDescript;
    private string qHint;
    private Reward qReward;
    private List<QuestObjective> qObjectives;
    private int itemID;
    private enum QuestType { Gather, Kill}
    private QuestType qType;
    private string qobjText;
    private string qobjThing;
    private int qobjTotal;
    private string qobjDescript;
    private bool qobjBonus;

    private void OnEnable()
    {
        _target = (QuestData) target;
        Help = false;
        Quests = false;
        SelectedQuestID = 0;
        SelectedQuest = false;
        ShowAllQuest = false;
        qObjectives = new List<QuestObjective>();
        if (_target.Quests == null)
        {
            _target.Quests =new List<Quest>();
        }
    }
    private void ShowMyLogo()
    {
        GUILayout.Label(Resources.Load("QuestSystem/Logo") as Texture);
    }
    private void HeaderButtons()
    {
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
    }

    private void ShowHelpArea()
    {
        GUILayout.Label("Information");
        if (GUILayout.Button("Close Help"))
        {
            Help = false;
        }
    }

    private void ShowQuestArea()
    {
        EditorGUILayout.Space();
        EditorGUILayout.Space();
        EditorGUILayout.Space();
        if (_target.Quests.Count == 0)
        {
            GUILayout.Label("First quest to add", MyEditorStyles.SubTiyleStyle);
            AddNewQuest();
        }

        if (ShowAllQuest && _target.Quests.Count > 0)
        {
            ShowQuestTable();
            if (GUILayout.Button("+"))
            {
                SelectedQuest = false;
                ShowNewQuestEditor = true;
            }
        }
    }

    /// <summary>
    /// Show all quests avaliable in the inspector.
    /// </summary>
    private void ShowQuestTable()
    {
        GUILayout.Label("Quest Table",MyEditorStyles.SubTiyleStyle);
        EditorGUILayout.BeginVertical();
        for (var i = 0; i < _target.Quests.Count; i++)
        {
            var quest = _target.Quests[i];
            EditorGUILayout.BeginHorizontal(MyEditorStyles.BackgroundStyle(MyEditorStyles.ListColorsBG[i % 2]));
            if (SelectedQuest && SelectedQuestID == i)
            {
                if (GUILayout.Button("Deselect",MyEditorStyles.SelectionButton))
                {
                    SelectedQuest = false;
                }
            }
            else
            {
                if (GUILayout.Button("Select",MyEditorStyles.SelectionButton))
                {
                    SelectedQuestID = i;
                    SelectedQuest = true;
                }
            }

            GUILayout.Label("ID: " + quest.Identifier.ID + " | ");
            GUILayout.Label(quest.Text.Title, GUILayout.Width(EditorGUIUtility.currentViewWidth / 3f));
            GUILayout.Label(" | ");
            GUILayout.Label("Require:" + quest.Identifier.PrQuest,GUILayout.Width(EditorGUIUtility.currentViewWidth / 8f));
            GUILayout.Label(" | ");
            GUILayout.Label("Next:" + quest.Identifier.ChainQuestID, GUILayout.Width(EditorGUIUtility.currentViewWidth / 8f));
            if (GUILayout.Button("X",GUILayout.Width(20)))
            {
                SelectedQuest = false;
                _target.Quests.RemoveAt(i);
            }
            EditorGUILayout.EndHorizontal();
        }
        EditorGUILayout.EndVertical();
    }

    public override void OnInspectorGUI()
    {

        ShowMyLogo();
        HeaderButtons();

        if (Help)
        {
            ShowHelpArea();
        }

        if (Quests)
        {
            ShowQuestArea();
            if (ShowNewQuestEditor)
            {
                AddNewQuest();
            }

            if (SelectedQuest && !ShowNewQuestEditor)
            {
                var tempSelectedQuest = _target.Quests[SelectedQuestID];
                ShowSelectedQuest(tempSelectedQuest);
            }
        }

        EditorUtility.SetDirty(target);
        serializedObject.Update();
        Undo.RecordObject(_target, "Changing quests");
        serializedObject.ApplyModifiedProperties();

    }

    private void OnValidate()
    {
        AssetDatabase.SaveAssets();
        AssetDatabase.Refresh();
    }


    private void ShowALlObjectivesFromSelectedQuest(List<QuestObjective> obj)
    {
        List<QuestType> qTypes = new List<QuestType>();
        foreach (var questObjective in obj)
        {
            if(questObjective is CollectionObjective)
            {
                qTypes.Add(QuestType.Gather);
            }
            else if (questObjective is KillTargetObjective)
            {
                qTypes.Add(QuestType.Kill);
            }
        }
        EditorGUILayout.Space();
        for (var index = 0; index < obj.Count; index++)
        {
            var questObjective = obj[index];
            EditorGUILayout.BeginHorizontal();
            GUILayout.FlexibleSpace();
            GUILayout.Label(questObjective.Title,MyEditorStyles.SubTiyleStyle);
            GUILayout.FlexibleSpace();
            EditorGUILayout.EndHorizontal();
            EditorGUILayout.BeginHorizontal();
            switch (qTypes[index])
            {
                case QuestType.Gather:
                    var tempCollect = questObjective as CollectionObjective;
                    tempCollect.Verb = EditorGUILayout.TextField("Action", tempCollect.Verb);
                    EditorGUILayout.EndHorizontal();
                    tempCollect.CollectionAmount = EditorGUILayout.IntField("Total to " + tempCollect.Verb, tempCollect.CollectionAmount);
                    tempCollect.ToCollect = EditorGUILayout.TextField("What do you need to " + tempCollect.Verb + ":", tempCollect.ToCollect);
                    GUILayout.Label("How you will Obtain",GUILayout.ExpandWidth(false));
                    EditorStyles.textField.wordWrap = true;
                    tempCollect.Description = EditorGUILayout.TextArea(tempCollect.Description);
                    tempCollect.IsBonus = EditorGUILayout.Toggle("Bonus Objective:", tempCollect.IsBonus);
                    break;
                case QuestType.Kill:
                    var tempKill = questObjective as KillTargetObjective;
                    tempKill.Target = EditorGUILayout.TextField("Thing to kill", tempKill.Target);
                    EditorGUILayout.EndHorizontal();
                    tempKill.KillTotalAmount = EditorGUILayout.IntField("Total of " + tempKill.Target, tempKill.KillTotalAmount);
                    GUILayout.Label("Where do you find:", GUILayout.ExpandWidth(false));
                    EditorStyles.textField.wordWrap = true;
                    tempKill.Description = EditorGUILayout.TextArea(tempKill.Description);
                    tempKill.IsBonus = EditorGUILayout.Toggle("Bonus Objective:", tempKill.IsBonus);
                    break;
            }
            if (obj.Count > 1)
            {
                if (GUILayout.Button("Remove this Objective"))
                {
                    obj.Remove(questObjective);
                }
            }
        }
        EditorGUILayout.Space();
        EditorGUILayout.Space();
        EditorGUILayout.Space();
        var tempObj = AddNewObjective();
        if (tempObj != null)
        {
            qobjText = " ";
            qobjThing = " ";
            qobjTotal = 0;
            qobjDescript = " ";
            qobjBonus = false;
            obj.Add(tempObj);
        }
    }

    private void ShowSelectedQuest(Quest q)
    {

        EditorGUILayout.BeginVertical();
            EditorGUILayout.BeginHorizontal();
            GUILayout.FlexibleSpace();
                GUILayout.Label("Selected Quest: " + q.Text.Title,MyEditorStyles.TitleStyle);
            GUILayout.FlexibleSpace();
            EditorGUILayout.EndHorizontal();
            EditorGUILayout.BeginHorizontal();
                q.Identifier.ChainQuestID = EditorGUILayout.IntField("Next Quest ID", q.Identifier.ChainQuestID);
                q.Identifier.PrQuest = EditorGUILayout.IntField("PRQuest ID", q.Identifier.PrQuest);
            EditorGUILayout.EndHorizontal();
            q.Text.Title = EditorGUILayout.TextField("Title", q.Text.Title);
            GUILayout.Label("Description",GUILayout.ExpandWidth(false));
            EditorStyles.textField.wordWrap = true;
            q.Text.DescriptionSummary = EditorGUILayout.TextArea(q.Text.DescriptionSummary);
            GUILayout.Label("Hint",GUILayout.ExpandWidth(false));
            EditorStyles.textField.wordWrap = true;
            q.Text.Hint = EditorGUILayout.TextArea(q.Text.Hint);
            q.GetObjectives();
            ShowALlObjectivesFromSelectedQuest(q.Objectives);
            q.ConstructObjectives();
            ShowSelectedQuestReward(q.QuestReward);
        EditorGUILayout.EndVertical();
    }

    private void ShowSelectedQuestReward(Reward selectedReward)
    {
        EditorGUILayout.BeginVertical();
            EditorGUILayout.BeginHorizontal();
            GUILayout.FlexibleSpace();
            GUILayout.Label("Rewards");
            GUILayout.FlexibleSpace();
            EditorGUILayout.EndHorizontal();
            selectedReward.ExperienceReward = EditorGUILayout.IntField("Experience:", selectedReward.ExperienceReward);
            selectedReward.GoldReward = EditorGUILayout.IntField("Gold:", selectedReward.GoldReward);
            foreach (var item in selectedReward.ItemsRewards)
            {
                    GUILayout.Label(item.Title);
            }
            itemID = EditorGUILayout.IntField("ID to add:", itemID);
        if (GUILayout.Button("AddItens"))
        {
            var tempItem = DatabaseControl.Instance.FetchItem(itemID);

            if (selectedReward.ItemsRewards == null)
            {
                selectedReward.ItemsRewards = new List<Item>();
            }

            if (tempItem != null)
            {
                selectedReward.ItemsRewards.Add(tempItem);
            }
        }

        if (GUILayout.Button("Remove ID"))
        {
            foreach (var item in selectedReward.ItemsRewards)
            {
                if (item.Id == itemID)
                {
                    selectedReward.ItemsRewards.Remove(item);
                    break;
                }
            }
        }

        EditorGUILayout.EndVertical();
    }

    private void AddNewQuest()
    {
        GUILayout.Label("Configure the new quest", MyEditorStyles.SubTiyleStyle);
        EditorGUILayout.BeginHorizontal();
        qName = EditorGUILayout.TextField("Quest Name:", qName);
        if (qObjectives.Count > 0)
        {
            if (GUILayout.Button("Add this quest"))
            {
                var exampleQuestIdentifier = new QuestIdentifier(_target.Quests.Count,-1,0);
                var exampleQuestText = new QuestText(qName, qDescript, qHint);
                var exampleQuest = new Quest(exampleQuestIdentifier, exampleQuestText, qObjectives,qReward);
                exampleQuest.ConstructObjectives();
                _target.Quests.Add(exampleQuest);
                qObjectives = new List<QuestObjective>();
                ShowNewQuestEditor = false;
                qName = "";
                qDescript = "";
                qHint = "";
            }
        }
        EditorGUILayout.EndHorizontal();
        GUILayout.Label("Quest Description");
        EditorStyles.textField.wordWrap = true;
        qDescript = EditorGUILayout.TextArea(qDescript);
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
            var tempObj = AddNewObjective();
            if (tempObj != null)
            {
                qobjText = " ";
                qobjThing = " ";
                qobjTotal = 0;
                qobjDescript = " ";
                qobjBonus = false;
                qObjectives.Add(tempObj);
            }
        }
    }

    private QuestObjective AddNewObjective()
    {
        GUILayout.Label("New Quest objective",MyEditorStyles.SubTiyleStyle);

        qType = (QuestType)EditorGUILayout.EnumPopup(qType);
        switch (qType)
        {
            case QuestType.Gather:
                qobjText = EditorGUILayout.TextField("Verb of action", qobjText);
                qobjThing = EditorGUILayout.TextField("What do you need to " + qobjText, qobjThing);
                qobjTotal = EditorGUILayout.IntField("Total to " + qobjText, qobjTotal);
                GUILayout.Label("How you will obtain:",GUILayout.ExpandWidth(false));
                EditorStyles.textField.wordWrap = true;
                qobjDescript = EditorGUILayout.TextArea(qobjDescript);
                qobjBonus = EditorGUILayout.Toggle("Bonus Objective:", qobjBonus);

                if (GUILayout.Button("Add New Objective"))
                {
                    var temp = new CollectionObjective(qobjText, qobjThing, qobjTotal, qobjDescript, qobjBonus);
                    return temp;
                }
                break;
            case QuestType.Kill:
                qobjText = EditorGUILayout.TextField("Thing to kill", qobjText);
                qobjTotal = EditorGUILayout.IntField("Total of " + qobjText, qobjTotal);
                GUILayout.Label("Where do you find:", GUILayout.ExpandWidth(false));
                EditorStyles.textField.wordWrap = true;
                qobjDescript = EditorGUILayout.TextArea(qobjDescript);
                qobjBonus = EditorGUILayout.Toggle("Bonus Objective:", qobjBonus);

                if (GUILayout.Button("Add New Objective"))
                {
                    var temp = new KillTargetObjective(qobjText,qobjDescript, qobjBonus, qobjTotal);
                    return temp;
                }
                break;
        }
        return null;
    }
}