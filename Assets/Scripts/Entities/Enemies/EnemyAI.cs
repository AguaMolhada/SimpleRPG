// --------------------------------------------------------------------------------------------------------------------
// <copyright file="EnemyAI.cs" by="Akapagion">
//  © Copyright Dauler Palhares da Costa Viana 2018.
//          http://github.com/DaulerPalhares
// </copyright>
// --------------------------------------------------------------------------------------------------------------------
using System.Collections;
using System.Collections.Generic;
using UnityEditor.PackageManager;
using UnityEngine;
using UnityEngine.AI;

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
    /// <summary>
    /// The NavMeshAgent to move.
    /// </summary>
    protected NavMeshAgent Agent;
    /// <summary>
    /// Reference to my aggro Table from the GameController.
    /// </summary>
    protected AggroStructure MyAggroTable;
    /// <summary>
    /// My index on the Game Constroller list.
    /// </summary>
    protected int MyAggroIndex;
    private void Start()
    {
        Target = GameObject.FindWithTag("Player");
        Agent = GetComponent<NavMeshAgent>();
        MyAggroTable = GameController.Instance.EnemyAgroTabble.Find(a => a.Enemy == this);
        MyAggroIndex = GameController.Instance.EnemyAgroTabble.IndexOf(MyAggroTable);
        StartCoroutine(UpdateAgro());
    }

    IEnumerator UpdateAgro()
    {
        while (true)
        {
            var amountonrange = 0;
            for (var index = 0; index < GameController.Instance.PlayersClients.Count; index++)
            {
                var client = GameController.Instance.PlayersClients[index];
                if (TargetInRange(client))
                {
                    MyAggroTable.AddAggro(client, Target == null ? 100 : 1);
                    amountonrange++;
                }
                else
                {
                    MyAggroTable.AddAggro(client, -1);
                }
            }

            Target = amountonrange != 0 ? MyAggroTable.SetTargetBasedOnAggro() : null;
            GameController.Instance.EnemyAgroTabble[MyAggroIndex] = MyAggroTable;
            yield return new WaitForSeconds(1);
        }

    }

    private bool TargetInRange(GameObject t)
    {
        return Vector3.Distance(t.transform.position, transform.position) <= MaxDistancePerception;
    }

    private void MoveToTarget()
    {
        Agent.destination = Target.transform.position;
        Agent.isStopped = false;
    }

    private void Update()
    {
        if (Target != null)
        {
            if (Vector3.Distance(Target.transform.position, transform.position) <= MaxDistancePerception)
            {
                if (Vector3.Distance(Target.transform.position, transform.position) > MinDistance)
                {
                    MoveToTarget();
                }
                else
                {
                    Debug.Log("Corre cuzao");
                }
            }
        }
        else
        {
            Agent.isStopped = true;
        }
    }
}
