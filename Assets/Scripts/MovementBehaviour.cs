using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementBehaviour : MonoBehaviour
{
    [SerializeField]
    private float _movementSpeed = 100.0f;

    [SerializeField]
    private float _jumpStrength = 100.0f;

    [SerializeField]
    private float groundDrag = 10.0f;

    [SerializeField]
    private float _dashSpeed = 300.0f;

    [SerializeField]
    private float _dashTime = 0.2f;

    [SerializeField]
    private float _dashCooldown = 1.0f;

    private Rigidbody _rigidBody;

    private Vector3 _desiredMovementDirection = Vector3.zero;

    private bool _grounded = false;
    private bool _isDashing = false;
    private bool _canDash = true;
    private const float GROUND_CHECK_DISTANCE = 0.2f;
    private const string GROUND_LAYER = "Ground";

    public Vector3 DesiredMovementDirection
    {
        get { return _desiredMovementDirection; }
        set { _desiredMovementDirection = value; }
    }

    private void Awake()
    {
        _rigidBody = GetComponent<Rigidbody>();
    }

    private void FixedUpdate()
    {
        if (!_isDashing) 
        {
            HandleMovement();
            
        }
        SpeedControl();

        _grounded = Physics.Raycast(transform.position + Vector3.up * 0.1f, Vector3.down, GROUND_CHECK_DISTANCE, LayerMask.GetMask(GROUND_LAYER));
    }

    private void Update()
    {
        if (_grounded)
        {
            if (_isDashing)
            {
                _rigidBody.drag = 0;
            }
            else
            {
                _rigidBody.drag = groundDrag;
            }
        }
        else
        {
            _rigidBody.drag = 0;
        }

    }

    public void HandleMovement()
    {
        if(_rigidBody == null ) return;
        _rigidBody.AddForce(DesiredMovementDirection.normalized * _movementSpeed * 10.0f, ForceMode.Force);
       
    }

    public void HandleDash()
    {
        if (_canDash)
        {
            StartCoroutine(Dash());
        }
    }

    private IEnumerator Dash()
    {
        _canDash = false;
        _isDashing = true;
        _rigidBody.useGravity = false;
        Vector3 dashDirection = Vector3.zero;
        if (DesiredMovementDirection == dashDirection)
        {
            dashDirection = _rigidBody.transform.forward;
        }
        else
        {
            dashDirection = DesiredMovementDirection.normalized;
        }
        _rigidBody.AddForce(dashDirection * _dashSpeed * 10.0f, ForceMode.Force);
        yield return new WaitForSeconds(_dashTime);
        _isDashing = false;        
        _rigidBody.useGravity = true;
        yield return new WaitForSeconds(_dashCooldown);
        _canDash = true;
    }
    public void Jump()
    {
        if (_grounded)
        {
            _rigidBody.AddForce(Vector3.up * _jumpStrength, ForceMode.Impulse);
            print("jumped");
        }
    }

    void SpeedControl()
    {
        Vector3 flatVelocity = new Vector3(_rigidBody.velocity.x, 0 , _rigidBody.velocity.z);

        if (_isDashing)
        {
            if (flatVelocity.magnitude > _dashSpeed)
            {
                Vector3 limitedVelocity = flatVelocity.normalized * _movementSpeed;
                _rigidBody.velocity = new Vector3(limitedVelocity.x, _rigidBody.velocity.y, limitedVelocity.z);
            }
        }
        else
        {
            if (flatVelocity.magnitude > _movementSpeed)
            {
                Vector3 limitedVelocity = flatVelocity.normalized * _movementSpeed;
                _rigidBody.velocity = new Vector3(limitedVelocity.x, _rigidBody.velocity.y, limitedVelocity.z);
            }
        }
    }
}
