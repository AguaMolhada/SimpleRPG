// --------------------------------------------------------------------------------------------------------------------
// <copyright file="PlayerController.cs" by="Akapagion">
//  © Copyright Dauler Palhares da Costa Vianna 2018.
//          http://github.com/DaulerPalhares
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class PlayerController : MonoBehaviour
{
    /// <summary>
    /// Player Nickname
    /// </summary>
    public string NickName;

    /// <summary>
    /// Player stats.
    /// </summary>
    [HideInInspector]public PlayerStats PlayerStats;
    /// <summary>
    /// This game object.
    /// </summary>
    [HideInInspector]public GameObject MySelf;
    /// <summary>
    /// Movment Speed Amount.
    /// </summary>
    public float Speed;
    /// <summary>
    /// Amount player gold.
    /// </summary>
    public int GoldAmount { get; protected set; }
    /// <summary>
    /// Amount player cash.
    /// </summary>
    public int CashAmount { get; protected set; }

    protected Vector3 MoveVelocity;
    protected Rigidbody MyRigidyBody;


    private void Start()
    {
        GameObject.FindGameObjectWithTag("GameController").GetComponent<GameController>().Player = this;
        MyRigidyBody = gameObject.GetComponent<Rigidbody>();
        MyRigidyBody.constraints = RigidbodyConstraints.FreezeRotation;
        MyRigidyBody.isKinematic = false;
        PlayerStats.AddHealth(int.MaxValue);
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
