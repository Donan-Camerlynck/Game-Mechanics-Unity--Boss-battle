using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class ThirdPersonCam : MonoBehaviour
{


    [SerializeField]
    private Transform orientation;
    [SerializeField]
    private Transform player;
    [SerializeField]
    private Transform playerObj;
    [SerializeField]
    private Rigidbody playerRigidBody;
    [SerializeField] 
    private Transform lookAtPos;
    [SerializeField]
    private float rotationSpeed = 37.0f;

    [SerializeField]
    private InputActionReference _movementActionForward;
    [SerializeField]
    private InputActionReference _movementActionRight;

    // Start is called before the first frame update
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 viewDirection = lookAtPos.position - new Vector3(transform.position.x, lookAtPos.position.y, transform.position.z);
        Vector3 viewDirNormalized = viewDirection.normalized;
        orientation.forward = viewDirNormalized; 
        
        playerObj.forward = viewDirNormalized;
        
    }
}
