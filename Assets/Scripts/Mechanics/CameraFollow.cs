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
    /// Is this a UI Camera?
    /// </summary>
    public bool CharacterUiCamera;
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
            if (CharacterUiCamera)
            {
                RotateCamera();
            }
        }
        else
        {
            _target = GameObject.Find("Jogador").transform;
        }

    }
    /// <summary>
    /// Method to make the camera follow the certain target.
    /// </summary>
    private void FollowTransform()
    {
        Vector3 desiredPosition = _target.position + Offset;
        desiredPosition -= transform.forward * Distance;

        transform.position = Vector3.Lerp(transform.position, desiredPosition, SmoothSpeed * Time.deltaTime);

    }
    /// <summary>
    /// Method to zoom in and out.
    /// </summary>
    /// <param name="amount">Amount to zoom.</param>
    private void Zoom(float amount)
    {
        Distance -= amount * ScrollSensitivity;
        Distance = Mathf.Clamp(Distance, MinDistance, MaxDistance);
    }
    /// <summary>
    /// Making the camera rotating around the target.
    /// </summary>
    private void RotateCamera()
    {
        transform.RotateAround(_target.transform.position,new Vector3(0,1,0),2);
    }
}
