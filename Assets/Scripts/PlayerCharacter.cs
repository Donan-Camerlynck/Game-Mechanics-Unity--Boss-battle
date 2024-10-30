using System;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.ProBuilder;

public class PlayerCharacter : BasicCharacter
{
    [SerializeField]
    private InputActionAsset _inputAsset;

    [SerializeField]
    private InputActionReference _movementActionForward;
    [SerializeField]
    private InputActionReference _movementActionRight;    

    private AbilityBehaviour _abilityBehaviour;
    
    private InputAction _abilityAction;

    private InputAction _jumpAction;

    private InputAction _dashAction;

    private PlayerHealth _health;

    [SerializeField]
    private Transform orientation;
    

    protected override void Awake()
    {
        base.Awake();

        _health = GetComponent<PlayerHealth>();
      
    }

    private void Start()
    {
        if (_inputAsset == null) return;

        _jumpAction = _inputAsset.FindActionMap("Gameplay").FindAction("Jump");

        _jumpAction.performed += HandleJumpInput;

        _abilityAction = _inputAsset.FindActionMap("Gameplay").FindAction("Ability");

        _abilityAction.performed += HandleAbilityInput;

        _dashAction = _inputAsset.FindActionMap("Gameplay").FindAction("Dash");

        _dashAction.performed += HandleDashInput;

        _abilityBehaviour = GetComponent<AbilityBehaviour>();
    }
    private void HandleAbilityInput(InputAction.CallbackContext context)
    {
        if (_abilityAction == null) return;
        if (!_health.alive) return;
        if (_abilityBehaviour._canShoot)
        {
            StartCoroutine(_abilityBehaviour.HandleAbility());
        }       
        
    }

    private void HandleDashInput(InputAction.CallbackContext context)
    {
        if (_dashAction == null) return;
        if (!_health.alive) return;
        _abilityBehaviour.HandleDash();
        StartCoroutine(_health.Dash());
        
    }

    private void OnEnable()
    {
        if (_inputAsset == null) return;

        _inputAsset.Enable();
    }

    private void OnDisable()
    {
        if ( _inputAsset == null) return;

        _inputAsset.Disable();
    }

    private void Update()
    {
        if (!_health.alive) return;
        HandleMovementInput();
    }

    void HandleMovementInput()
    {
        
        if (_movementBehaviour == null || _movementActionForward == null || _movementActionRight == null) return;

        float movementInputForward = _movementActionForward.action.ReadValue<float>();
        float movementInputRight = _movementActionRight.action.ReadValue<float>();

        Vector3 movementForward = movementInputForward * orientation.forward;
        Vector3 movementRight = movementInputRight * orientation.right;

        _movementBehaviour.DesiredMovementDirection = movementRight + movementForward;
    }

    private void HandleJumpInput(InputAction.CallbackContext context)
    {
        if (!_health.alive) return;
        _movementBehaviour.Jump();
    }

    private void OnDestroy()
    {
       

        _jumpAction.performed -= HandleJumpInput;

       

        _abilityAction.performed -= HandleAbilityInput;

       

        _dashAction.performed -= HandleDashInput;

      
    }
}
