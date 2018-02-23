// --------------------------------------------------------------------------------------------------------------------
// <copyright file="EnemyAI.cs" by="Akapagion">
//  © Copyright Dauler Palhares da Costa Viana 2018.
//          http://github.com/DaulerPalhares
// </copyright>
// --------------------------------------------------------------------------------------------------------------------
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    /// <summary>
    /// Target to attack.
    /// </summary>
    public GameObject Target { get; private set; }
    /// <summary>
    /// Percent health to start running.
    /// </summary>
    public int PercentHp;
    /// <summary>
    /// Min distance btween this and the Target.
    /// </summary>
    public float MinDistance;
    /// <summary>
    /// Max distance to start the chase/attack the Target.
    /// </summary>
    public float MaxDistancePerception;
}
