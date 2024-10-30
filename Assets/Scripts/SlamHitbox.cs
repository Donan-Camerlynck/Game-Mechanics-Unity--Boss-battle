using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlamHitbox : MonoBehaviour
{
    [SerializeField]
    private int _damage = 3;
    bool _hasHit = false;
    // Start is called before the first frame update
    void Start()
    {
        Invoke("Kill", 0.5f);
    }

    // Update is called once per frame
    void Update()
    {
        transform.localScale = new Vector3( transform.localScale.x + 50.0f * Time.deltaTime, transform.localScale.y, transform.localScale.z + 50.0f * Time.deltaTime);
        
    }

  

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player") && !_hasHit) 
        {
            //damage player
            print("SLAM HIT");
            other.GetComponent<PlayerHealth>().DoDamage(_damage);
            _hasHit = true;
        }
    }

    private void Kill()
    {
        Destroy(gameObject);
    }
}
