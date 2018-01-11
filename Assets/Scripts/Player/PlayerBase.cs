// --------------------------------------------------------------------------------------------------------------------
// <copyright file="PlayerBase.cs" by="Akapagion">
//  © Copyright Dauler Palhares da Costa Viana 2017.
//          http://github.com/DaulerPalhares
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

using System;
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
    /// <summary>
    /// Ammount player gold.
    /// </summary>
    public int GoldAmmount { get; protected set; }
    /// <summary>
    /// Ammount player cash.
    /// </summary>
    public int CashAmmount { get; protected set; }

    private void Start()
    {
        GameObject.FindGameObjectWithTag("GameController").GetComponent<GameController>().Player = this;
    }

    private void Update()
    {
        transform.Translate(Input.GetAxis("Horizontal") * Speed * Time.deltaTime, 0f, Input.GetAxis("Vertical") * Speed * Time.deltaTime);
    }

}
