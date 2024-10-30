using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class BossBehaviour : MonoBehaviour
{
    [SerializeField]
    private NavMeshAgent _navMeshAgent;

    [SerializeField]
    private float _slamRange = 5.0f;

    [SerializeField]
    private float _maxRange = 15.0f;

    [SerializeField]
    private float _idleRange = 100.0f;

    [SerializeField]
    private GameObject player;

    [SerializeField]
    private float _slamCooldown;

    [SerializeField]
    private float _projectileCooldown;

    [SerializeField]
    private float _slamDashTime = 0.4f;

    [SerializeField]
    private GameObject _spawnPoint;

    [SerializeField]
    private GameObject _bossProjectile;

    [SerializeField]
    private GameObject _slamHitbox;


    private BossHealth _health;

    private bool _idle;

    private bool _attacking;

    private bool _canSlam = true;

    private bool _isGoingForSlam = false;

    private void Awake()
    {
        _health = GetComponent<BossHealth>();
    }


    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void FixedUpdate()
    {
        if (!_health.alive) return;
        Vector3 lookat = player.transform.position - transform.position;
        transform.rotation = Quaternion.LookRotation(lookat.normalized, Vector3.up);
    }

    // Update is called once per frame
    void Update()
    {
        if (!_health.alive) return;
        //check distance between player and boss
        float distance = Vector3.Distance(transform.position, player.transform.position);
        
        if (distance < _idleRange) //if player is close enough boss activates
        {           
            _idle = false;
        }
        else 
        {
            _idle = true;
        }
        if(!_idle) //if boss is active, do attack behaviour and track player behaviour
        {
            _navMeshAgent.SetDestination(player.transform.position);
            
            if(distance < _maxRange) //check if player is in attackrange boss
            {                
                if(distance > _slamRange && _canSlam) //check if player is outside slam range when slam is off cooldown
                {
                    _isGoingForSlam = true;
                }
                //start attack
                if (_isGoingForSlam) //check if boss going for a slam attack
                {                    
                    if (distance < _slamRange)
                    {
                        //reset navmeshagent and start slam attack
                        
                        _navMeshAgent.speed = 25;
                        _navMeshAgent.acceleration = 8;
                        _isGoingForSlam = false;
                        StartCoroutine(SlamCooldown());
                    }
                    else
                    {
                        //setup navmeshagent for dash to player if outside of slamrange
                        _navMeshAgent.isStopped = false;
                        _navMeshAgent.speed = 500;
                        _navMeshAgent.acceleration = 300;
                    }
                }
                else if(!_attacking)
                {
                    //ranged attack if not going for slam
                    StartCoroutine(AttackCooldown());
                }
            }
            else
            {
                _navMeshAgent.isStopped = false;
            }
        }
        
    }

    IEnumerator AttackCooldown()
    {
        _navMeshAgent.isStopped = true;
        _navMeshAgent.velocity = Vector3.zero;
        _attacking = true;        
        HandleProjectile();
        yield return new WaitForSeconds(_projectileCooldown);
        _attacking = false;
    }

    IEnumerator SlamCooldown() 
    {
        _navMeshAgent.isStopped = true;
        _navMeshAgent.velocity = Vector3.zero;
        _attacking = true;
        _canSlam = false;
        HandleSlam();
        yield return new WaitForSeconds(_projectileCooldown);
        _attacking = false;        
        yield return new WaitForSeconds(_slamCooldown);        
        _canSlam = true;
    }

    public void HandleProjectile()
    {
        Vector3 relativePos = player.transform.position - transform.position;

        // the second argument, upwards, defaults to Vector3.up
        Quaternion rotation = Quaternion.LookRotation(relativePos, Vector3.up);
        Quaternion rot = Quaternion.FromToRotation(_spawnPoint.transform.position, player.transform.position);

        Instantiate(_bossProjectile, _spawnPoint.transform.position, transform.rotation);

    }

    public void HandleSlam()
    {
        Instantiate(_slamHitbox, transform.position, transform.rotation);
    }
}
