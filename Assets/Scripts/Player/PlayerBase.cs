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

[RequireComponent(typeof(Rigidbody))]
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

    protected Vector3 MoveVelocity;
    protected Rigidbody MyRigidyBody;

    private void Start()
    {
        GameObject.FindGameObjectWithTag("GameController").GetComponent<GameController>().Player = this;
        MyRigidyBody = gameObject.GetComponent<Rigidbody>();
        MyRigidyBody.constraints = RigidbodyConstraints.FreezeRotation;
        MyRigidyBody.isKinematic = false;
    }

    private void Update()
    {
        if (!GameController.Instance.IsPaused)
        {
            Ray cameraRay = Camera.main.ScreenPointToRay(Input.mousePosition);
            Plane groundPlane = new Plane(Vector3.up, Vector3.zero);
            float rayLenght;

            if (groundPlane.Raycast(cameraRay, out rayLenght))
            {
                Vector3 pointToLook = cameraRay.GetPoint(rayLenght);
                Debug.DrawLine(cameraRay.origin, pointToLook, Color.black);

                transform.LookAt(new Vector3(pointToLook.x, transform.position.y, pointToLook.z));
            }
        }

        if (Input.GetKeyDown(KeyCode.K))
        {
            PlayerStats.AddExperience(10);
        }
    }

    private void FixedUpdate()
    {
        if (!GameController.Instance.IsPaused)
        {
            var moveImput = new Vector3(Input.GetAxis("Horizontal"), 0f, Input.GetAxis("Vertical"));
            MoveVelocity = moveImput * Speed;
            MoveVelocity = transform.TransformDirection(MoveVelocity);

            MyRigidyBody.velocity = MoveVelocity;
        }
    }
}
