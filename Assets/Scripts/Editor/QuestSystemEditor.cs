// --------------------------------------------------------------------------------------------------------------------
// <copyright file="QuestSystemEditor.cs" by="Akapagion">
//  © Copyright Dauler Palhares da Costa Viana 2018.
//          http://github.com/DaulerPalhares
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

using System.Collections.Generic;
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

    private bool ShowObjectiveEditor;
    ////////////////////////////
    private bool haveContinuation;
    private int chain;
    private bool havePreviusQuest;
    private int previus;

    private string qName;
    private string qDescript;
    private string qHint;
    private List<IQuestObjective> qObjectives;
    private enum QuestType { Gather, Kill}
    private QuestType qType;
    private string qobjText;
    private int qobjTotal;
    private string qobjDescript;
    private bool qobjBonus;
    private GameObject qobjItem;

    private void OnEnable()
    {
        _target = (QuestData) target;
        Help = false;
        Quests = false;
        SelectedQuestID = 0;
        SelectedQuest = false;
        ShowAllQuest = false;
        qObjectives = new List<IQuestObjective>();

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
            if (_target.Quests.Count == 0)
            {
                GUILayout.Label("First Quest to add");
                AddNewQuest();

            }

            if (ShowAllQuest && _target.Quests.Count > 0)
            {
                ShowAllQuests();
            }

            if (SelectedQuest)
            {
               var tempSelectedQuest = _target.Quests[SelectedQuestID];
               ShowCurrentQuest(tempSelectedQuest);
                
            }
        }

    }

    private void ShowAllQuests()
    {
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
            EditorGUILayout.EndHorizontal();
        }
    }

    private void ShowCurrentQuest(Quest q)
    {
        GUILayout.Label("Selected quest to Edit: " + q.Text.Title);
        EditorGUILayout.BeginHorizontal();
        haveContinuation = EditorGUILayout.Toggle("Have Chain?", haveContinuation);
        havePreviusQuest = EditorGUILayout.Toggle("Have Previus?", havePreviusQuest);
        EditorGUILayout.EndHorizontal();
        if (haveContinuation)
        {
            chain = EditorGUILayout.IntField("Next Quest ID", chain);
            q.Identifier.SetChainQuestID(chain);
        }
        if (havePreviusQuest)
        {
            previus = EditorGUILayout.IntField("Previus Quest ID", previus);
            q.Identifier.SetSourceID(previus);
        }
        ShowAllObjectives(q.Objectives);

    }

    private void AddNewQuest()
    {
        qName = EditorGUILayout.TextField("Quest Name:", qName);
        qDescript = EditorGUILayout.TextField("Quest Description:", qDescript);
        qHint = EditorGUILayout.TextField("Quest Hint:", qHint);
        if (qObjectives.Count > 0)
        {
            ShowAllObjective = EditorGUILayout.Toggle("Show Objectives", ShowAllObjective);
            if (ShowAllObjective)
            {
                ShowAllObjectives(qObjectives);
            }

            ShowObjectiveEditor = EditorGUILayout.Toggle("Show Objective Editor", ShowObjectiveEditor);
            if (ShowObjectiveEditor)
            {
                AddObjectives();
            }
        }
        else
        {
            AddObjectives();
        }

        if (qObjectives.Count > 0)
        {
            if (GUILayout.Button("Add first Quest"))
            {
                var exampleQuestIdentifier = new QuestIdentifier(0);
                var exampleQuestText = new QuestText(qName, qDescript, qHint);
                var exampleQuest = new Quest(exampleQuestIdentifier, exampleQuestText, qObjectives);
                _target.Quests.Add(exampleQuest);
                qObjectives = new List<IQuestObjective>();
            }
        }
    }
    private void ShowAllObjectives(List<IQuestObjective> obj)
    {
        for (var index = 0; index < obj.Count; index++)
        {
            var questObjective = obj[index];
            GUILayout.Label("Objective nº" + index);
            qType = (QuestType) EditorGUILayout.EnumPopup(qType);
            switch (qType)
            {
                case QuestType.Gather:
                    CollectionObjective tempCollect = (CollectionObjective) questObjective;
                    tempCollect.Title = EditorGUILayout.TextField("Verb of action", tempCollect.Title);
                    tempCollect.CollectionAmount =
                        EditorGUILayout.IntField("Total to " + qobjText, tempCollect.CollectionAmount);
                    tempCollect.Description = EditorGUILayout.TextField("What do you need to " + tempCollect.Title,
                        tempCollect.Description);
                    tempCollect.ItemToCollect =
                        EditorGUILayout.ObjectField(tempCollect.ItemToCollect, typeof(GameObject), true) as GameObject;
                    tempCollect.IsBonus = EditorGUILayout.Toggle("Bonus Objective:", tempCollect.IsBonus);

                    if (GUILayout.Button("Remove this Objective"))
                    {
                        qObjectives.Remove(questObjective);
                    }

                    break;
                case QuestType.Kill:
                    EditorGUILayout.LabelField("NEED TO IMPLEMENT SORRY");
                    break;
            }
        }
    }
    private void AddObjectives()
    {
        GUILayout.Label("Now Need to configure the objectives");

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
