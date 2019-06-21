using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{

    [Header("Player Properties")]
    [Tooltip("Needed for moving the camera")]
    [SerializeField] Transform vr_Cam;
    [Tooltip("Adjust accordingly for walking speed of the camera")]
    [SerializeField] [Range(0f, 10f)] float moveSpeed;
    [Tooltip("Adjust accordingly for walking angle")]
    [SerializeField] [Range(0f, 100f)] float walkAngle;
    [SerializeField] bool moveForward;

    CharacterController playerController;

    void Start()
    {
        /* Null checking if theres a character controller component 
         * attach to the player obj. else add one and get the component again */
        if (GetComponent<CharacterController>() != null)
        {
            playerController = GetComponent<CharacterController>();
        }
        else
        {
            gameObject.AddComponent<CharacterController>();
            playerController = GetComponent<CharacterController>();
        }
    }

    private void Update()
    {
        if (vr_Cam.eulerAngles.x >= walkAngle && vr_Cam.eulerAngles.x < 90.0f)
        {
            moveForward = true;
        }
        else
        {
            moveForward = false;
        }

        if (moveForward)
        {
            MovePlayer();
        }
    }

    public void MovePlayer()
    {
        Vector3 forward = vr_Cam.TransformDirection(Vector3.forward);
        playerController.SimpleMove(forward * moveSpeed);
    }

}
