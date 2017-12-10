// --------------------------------------------------------------------------------------------------------------------
// <copyright file="CameraFollow.cs" by="Akapagion">
//  © Copyright Dauler Palhares da Costa Viana 2017.
//          http://github.com/DaulerPalhares
// </copyright>
// --------------------------------------------------------------------------------------------------------------------
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    /// <summary>
    /// Transform to follow.
    /// </summary>
    private Transform _target;
    /// <summary>
    /// Camera speed.
    /// </summary>
    public float SmoothSpeed;
    /// <summary>
    /// Camera offset.
    /// </summary>
    public Vector3 Offset;

    private void FixedUpdate()
    {
        if (_target != null)
        {
            FollowTransform();
        }
        else
        {
            _target = GameObject.Find("Jogador").transform;
        }
    }

    private void FollowTransform()
    {
        Vector3 desiredPosition = _target.position + Offset;
        Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, SmoothSpeed * Time.deltaTime);
        transform.position = smoothedPosition;

        transform.LookAt(_target);
    }
}
