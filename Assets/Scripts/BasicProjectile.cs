using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicProjectile : MonoBehaviour
{

    [SerializeField] private float _speed = 50.0f;
    [SerializeField] private float _verticalStartSpeed = 10.0f;
    [SerializeField] private float _lifeTime = 2.0f;

    private float _verticalSpeed;

    [SerializeField] protected bool _arcTrajectory = false;
    protected float _gravity = -20f;
    

    private void Awake()
    {
        if (!_arcTrajectory)
        {
            Invoke("Kill", _lifeTime);            
        }
        else
        {
            _verticalSpeed = _verticalStartSpeed;
        }
    }

    private void FixedUpdate()
    {
        if(!WallDetection() & !GroundDetection())
        {
            transform.position += transform.forward * Time.fixedDeltaTime * _speed;
            if(_arcTrajectory) 
            {
                transform.position += transform.up * Time.fixedDeltaTime * _verticalSpeed;
                _verticalSpeed += Time.fixedDeltaTime * _gravity;
            }
        }
    }

    static readonly string[] RAYCAST_MASK = { "Ground", "StaticLevel" };
    bool WallDetection()
    {
        Ray collisionRay = new Ray(transform.position, transform.forward);
        if(Physics.Raycast(collisionRay, Time.deltaTime * _speed, LayerMask.GetMask(RAYCAST_MASK)))
        {
            Kill();
            return true;
        }
        return false;
    }

    bool GroundDetection()
    {
        Ray collisionRay = new Ray(transform.position, -transform.up);
        if (Physics.Raycast(collisionRay, Time.deltaTime * _speed, LayerMask.GetMask(RAYCAST_MASK)))
        {
            Kill();
            return true;
        }
        return false;
    }

    void Update()
    {
        //transform.position += transform.forward * Time.deltaTime * _speed;
    }

    protected void Kill()
    {
        Destroy(gameObject);
    }
}
