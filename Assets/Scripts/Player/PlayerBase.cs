// --------------------------------------------------------------------------------------------------------------------
// <copyright file="PlayerBase.cs" by="Akapagion">
//  © Copyright Dauler Palhares da Costa Viana 2017.
//          http://github.com/DaulerPalhares
// </copyright>
// --------------------------------------------------------------------------------------------------------------------
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBase : MonoBehaviour
{
    /// <summary>
    /// Player Nickname
    /// </summary>
    public string NickName;
    public PlayerStats PlayerStats;
    public GameObject MySelf;
    public float Speed;

    private void Update()
    {
        transform.Translate(Input.GetAxis("Horizontal") * Speed * Time.deltaTime, 0f, Input.GetAxis("Vertical") * Speed * Time.deltaTime);
    }

}
