// --------------------------------------------------------------------------------------------------------------------
// <copyright file="EditorStyles.cs" by="Akapagion">
//  © Copyright Dauler Palhares da Costa Viana 2018.
//          http://github.com/DaulerPalhares
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

using System;
using UnityEngine;

public static class MyEditorStyles
{
    public static Color[] ListColorsBG = {
        new Color(0.6f, 0.6f, 0.6f), 
        new Color(0.5f, 0.5f, 0.5f), 
    };

    public static GUIStyle TitleStyle = new GUIStyle() {
        fontSize = 24,
        fontStyle = FontStyle.Bold,
        normal = new GUIStyleState() { textColor = Color.yellow}

    };
    public static GUIStyle SubTiyleStyle = new GUIStyle()
    {
        fontSize = 18,
        fontStyle = FontStyle.Bold
    };

    public static GUIStyle SelectionButton = new GUIStyle("button")
    {
        fixedWidth = 80,
        fontStyle = FontStyle.Bold,
      
    };

    public static GUIStyle BackgroundStyle(Color c)
    {
        var text = new Texture2D(1, 1, TextureFormat.RGBA32, false);
        text.SetPixel(0,0,c);
        text.Apply();

        return new GUIStyle()
        {
            normal = new GUIStyleState()
            {
                background = text,
            },
        };
    }
}
