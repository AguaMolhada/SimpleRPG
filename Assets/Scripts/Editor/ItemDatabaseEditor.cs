// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ItemDatabaseEditor.cs" by="Akapagion">
//  © Copyright Dauler Palhares da Costa Viana 2018.
//          http://github.com/DaulerPalhares
// </copyright>
// --------------------------------------------------------------------------------------------------------------------
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;

[CustomEditor(typeof(ItemDatabase))]
public class ItemDatabaseEditor : Editor
{
    private ItemDatabase _target;
    private Item _selectedItem;
    private List<BonusAttribute> _selecteBonusAttributes;
    private void Awake()
    {
        _target = (ItemDatabase) target;
    }
    private void ShowMyLogo()
    {
        GUILayout.Label(Resources.Load("ItemDatabase/Logo") as Texture);
    }

    public override void OnInspectorGUI()
    {
        ShowMyLogo();
        EditorGUILayout.BeginHorizontal(MyEditorStyles.BackgroundStyle(MyEditorStyles.ListColorsBG[0]));
        if (GUILayout.Button("Load Json"))
        {
            _target.ConstructItemDatabase();
        }
        if (GUILayout.Button("Save Data to Json"))
        {
            _target.SaveItemDatabase();
            EditorUtility.SetDirty(target);
            serializedObject.Update();
            Undo.RecordObject(_target, "Changing ItemDatabase");
            serializedObject.ApplyModifiedProperties();
        }
        EditorGUILayout.EndHorizontal();
        ShowItemList();
    }

    private void ShowItemList()
    {
        for (var i = 0; i < _target.ItemsDatabase.Count; i++)
        {
            var item = _target.ItemsDatabase[i];
            EditorGUILayout.BeginHorizontal(MyEditorStyles.BackgroundStyle(MyEditorStyles.ListColorsBG[i % 2]));
            if (_selectedItem != null && _selectedItem == item)
            {
                if (GUILayout.Button("Deselect", MyEditorStyles.SelectionButton))
                {
                    _selectedItem = null;
                }
            }
            else
            {
                if (GUILayout.Button("Select", MyEditorStyles.SelectionButton))
                {
                    _selectedItem = item;
                    if (_selectedItem.BonusAttributes.Length == 0)
                    {
                        _selectedItem.BonusAttributes = new BonusAttribute[1];
                        _selecteBonusAttributes = _selectedItem.BonusAttributes.ToList();
                    }
                    else
                    {
                        _selecteBonusAttributes = _selectedItem.BonusAttributes.ToList();
                    }
                }
            }

            GUILayout.Label("ID:"+item.Id, GUILayout.Width(50));
            GUILayout.Label(" | " + item.Title + " ",GUILayout.Width(EditorGUIUtility.currentViewWidth/2f));
            GUILayout.Label(Resources.Load("Sprites/" + item.Sprite) as Texture, GUILayout.Width(15),GUILayout.Height(15));
            GUILayout.Label("| ");
            GUILayout.Label(item.ItemT.ToString());
            if (GUILayout.Button("X", GUILayout.Width(20)))
            {
                _target.ItemsDatabase.Remove(item);
            }

            EditorGUILayout.EndHorizontal();
            if (_selectedItem != null && _selectedItem == item)
            {
                ShowSelectedItem(i);
            }
        }

        if (GUILayout.Button("+"))
        {
            var temp = new Item
            {
                Id = _target.ItemsDatabase.Count,
                BonusAttributes = new BonusAttribute[1] {new BonusAttribute()}
            };
            _target.ItemsDatabase.Add(temp);
        }
    }

    private void ShowSelectedItem(int index)
    {
        EditorGUILayout.BeginHorizontal(MyEditorStyles.BackgroundStyle(MyEditorStyles.ListColorsBG[index % 2]));
            GUILayout.Label("Name",GUILayout.ExpandWidth(false));
            _selectedItem.Title = EditorGUILayout.TextField(_selectedItem.Title);
            EditorGUILayout.BeginHorizontal(MyEditorStyles.BackgroundStyle(MyEditorStyles.ListColorsBG[index % 2]));
                GUILayout.Label("Type:", GUILayout.ExpandWidth(false));
                _selectedItem.ItemT = (ItemType)EditorGUILayout.EnumPopup(_selectedItem.ItemT);
            EditorGUILayout.EndHorizontal();
        EditorGUILayout.EndHorizontal();
        //Sprite, S,B value, stackable and usable.
        EditorGUILayout.BeginHorizontal(MyEditorStyles.BackgroundStyle(MyEditorStyles.ListColorsBG[index % 2]));
            GUILayout.Label(Resources.Load("Sprites/" + _selectedItem.Sprite)as Texture, GUILayout.Width(50), GUILayout.Height(50));
            EditorGUILayout.BeginVertical();
                EditorGUILayout.BeginHorizontal();
                    GUILayout.Label("SpriteName:",GUILayout.ExpandWidth(false));
                    _selectedItem.Sprite = EditorGUILayout.TextField(_selectedItem.Sprite);
                EditorGUILayout.EndHorizontal();
                EditorGUILayout.BeginHorizontal(MyEditorStyles.BackgroundStyle(MyEditorStyles.ListColorsBG[index % 2]));
                    EditorGUILayout.BeginHorizontal();
                        GUILayout.Label("S Value:", GUILayout.ExpandWidth(false));
                        _selectedItem.SellValue = EditorGUILayout.IntField(_selectedItem.SellValue);
                    EditorGUILayout.EndHorizontal();
                    EditorGUILayout.BeginHorizontal(MyEditorStyles.BackgroundStyle(MyEditorStyles.ListColorsBG[index % 2]));
                        GUILayout.Label("B Value:",GUILayout.ExpandWidth(false));
                        _selectedItem.BuyValue = EditorGUILayout.IntField(_selectedItem.BuyValue);  
                    EditorGUILayout.EndHorizontal();
                EditorGUILayout.EndHorizontal();
                EditorGUILayout.BeginHorizontal(MyEditorStyles.BackgroundStyle(MyEditorStyles.ListColorsBG[index % 2]));
                    _selectedItem.Stackable = EditorGUILayout.Toggle("Stackable:", _selectedItem.Stackable);
                    _selectedItem.Usable = EditorGUILayout.Toggle("Usable:", _selectedItem.Usable);
                EditorGUILayout.EndHorizontal();
            EditorGUILayout.EndVertical();
        EditorGUILayout.EndHorizontal();
        //Atributes;
        ShowSelectedItemAttributeBonus(index);
    }

    private void ShowSelectedItemAttributeBonus(int index)
    {
        EditorGUILayout.BeginHorizontal(MyEditorStyles.BackgroundStyle(MyEditorStyles.ListColorsBG[index % 2]));

            EditorGUI.indentLevel++;
            GUILayout.FlexibleSpace();
            GUILayout.Label("Item Attributes",MyEditorStyles.SubTiyleStyle,GUILayout.ExpandWidth(false));
            if (GUILayout.Button("+", GUILayout.Width(20)))
            {
                _selecteBonusAttributes.Add(new BonusAttribute());
            }
            GUILayout.FlexibleSpace();
            EditorGUILayout.EndHorizontal();
            EditorGUILayout.BeginVertical();
            foreach (var attribute in _selecteBonusAttributes)
            {
                EditorGUILayout.BeginHorizontal(MyEditorStyles.BackgroundStyle(MyEditorStyles.ListColorsBG[index % 2]));
                EditorGUILayout.BeginHorizontal();
                GUILayout.Label("Bonus Type:", GUILayout.ExpandWidth(false));
                attribute.AttributeBonus = (ItemBonusAttribute) EditorGUILayout.EnumPopup(attribute.AttributeBonus);
                EditorGUILayout.EndHorizontal();
                EditorGUILayout.BeginHorizontal();
                GUILayout.Label("Amount:", GUILayout.ExpandWidth(false));
                attribute.BonusAmmout = EditorGUILayout.IntField(attribute.BonusAmmout);
                EditorGUILayout.EndHorizontal();
                if (GUILayout.Button("X", GUILayout.Width(20)))
                {
                    _selecteBonusAttributes.Remove(attribute);
                }
                EditorGUILayout.EndHorizontal();
            }
            EditorGUILayout.EndVertical();
            _selectedItem.BonusAttributes = _selecteBonusAttributes.ToArray();
            EditorGUI.indentLevel--;
    }
}
