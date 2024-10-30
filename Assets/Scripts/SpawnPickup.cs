using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnPickup : MonoBehaviour
{
    bool _canSpawn = true;

    [SerializeField]
    private float _spawnDelay;

    [SerializeField]
    private GameObject _spawnObject;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(_canSpawn)
        {
            StartCoroutine(Spawn());
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            _canSpawn = true;
        }
    }

    IEnumerator Spawn()
    {
        _canSpawn = false;
        yield return new WaitForSeconds(_spawnDelay);
        Instantiate(_spawnObject, transform.position + new Vector3(0, 1,0), transform.rotation);        
    }
}
