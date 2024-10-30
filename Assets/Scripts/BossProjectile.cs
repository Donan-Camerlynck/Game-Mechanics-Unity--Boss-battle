using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class BossProjectile : BasicProjectile
{
    [SerializeField] private int _damage = 10;

    [SerializeField]
    private float _explosionDelay = 0.002f;

    [SerializeField]
    private GameObject _explosion;

    bool _canSpawnExplosion = true;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(_canSpawnExplosion)
        {
            StartCoroutine(Explode());
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            other.gameObject.GetComponent<PlayerHealth>().DoDamage(_damage);
            Kill();
        }
    }

    IEnumerator Explode()
    {
        _canSpawnExplosion = false;
        Vector3 position = transform.position;
        Quaternion rotation = transform.rotation;
        yield return new WaitForSeconds(_explosionDelay);
        Instantiate(_explosion, position, rotation);
        _canSpawnExplosion = true;
    }
}
