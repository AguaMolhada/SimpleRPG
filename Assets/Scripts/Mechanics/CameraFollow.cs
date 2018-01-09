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
    public bool characterUICamera;

    /// <summary>
    /// Transform to follow.
    /// </summary>
    private Transform _target;
    /// <summary>
    /// Camera speed.
    /// </summary>
    public float SmoothSpeed;
    /// <summary>
    /// Sensitivity for zoom in/out
    /// </summary>
    public float ScrollSensitivity;
    /// <summary>
    /// Camera offset.
    /// </summary>
    public Vector3 Offset;
    /// <summary>
    /// Distance from the camera.
    /// </summary>
    public float Distance;
    /// <summary>
    /// Min distance from the target.
    /// </summary>
    public float MinDistance;
    /// <summary>
    /// Max distance from the target.
    /// </summary>
    public float MaxDistance;

    private void FixedUpdate()
    {
        if (_target != null)
        {
            Zoom(Input.GetAxis("Mouse ScrollWheel"));
            FollowTransform();
            if (characterUICamera)
            {
                RotateCamera();
            }
        }
        else
        {
            _target = GameObject.Find("Jogador").transform;
        }

    }

    private void FollowTransform()
    {
        Vector3 desiredPosition = _target.position + Offset;
        desiredPosition -= transform.forward * Distance;

        transform.position = Vector3.Lerp(transform.position, desiredPosition, SmoothSpeed * Time.deltaTime);

    }

    private void Zoom(float ammout)
    {
        Distance -= ammout * ScrollSensitivity;
        Distance = Mathf.Clamp(Distance, MinDistance, MaxDistance);
    }

    private void RotateCamera()
    {
        transform.RotateAround(_target.transform.position,new Vector3(0,1,0),2);
    }
}
