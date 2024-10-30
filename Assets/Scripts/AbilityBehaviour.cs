using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AbilityBehaviour : MonoBehaviour
{

    [SerializeField]
    private GameObject _arcaneProjectile;

    [SerializeField]
    private GameObject _fireProjectile;

    [SerializeField]
    private GameObject _earthProjectile;

    [SerializeField]
    private GameObject _oilProjectile;

    [SerializeField]
    private GameObject _target;

    [SerializeField]
    private GameObject _spawnPoint;

    private MovementBehaviour _movementComponent;

    private PlayerHealth _playerHealth;

    private enum AbilityType
    {
        arcane,
        fire, 
        earth, 
        oil
    }

    private AbilityType _selectedType;

    [SerializeField]
    private float _projectileCooldown = 1.0f;

    public bool _canShoot {get;set;}


    private void Awake()
    {
        _movementComponent = GetComponent<MovementBehaviour>();
        _playerHealth = GetComponent<PlayerHealth>();
    }

    // Start is called before the first frame update
    void Start()
    {
        _target = GameObject.FindWithTag("Boss");
        _selectedType = AbilityType.arcane;
        _canShoot = true;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public IEnumerator HandleAbility()
    {
        _canShoot = false;
        _playerHealth.DoDamage(1);  
        Vector3 relativePos = _target.transform.position - transform.position;

        // the second argument, upwards, defaults to Vector3.up
        Quaternion rotation = Quaternion.LookRotation(relativePos, Vector3.up);
        Quaternion rot = Quaternion.FromToRotation(_spawnPoint.transform.position, _target.transform.position);

        Instantiate(_arcaneProjectile, _spawnPoint.transform.position, _spawnPoint.transform.rotation);
        yield return new WaitForSeconds(_projectileCooldown);
        _canShoot = true;
    }

    public void HandleDash()
    {
        _movementComponent.HandleDash();
    }
}
