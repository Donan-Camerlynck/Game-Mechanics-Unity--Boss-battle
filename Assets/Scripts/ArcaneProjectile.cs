using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ArcaneProjectile : BasicProjectile
{

    [SerializeField] private int _damage = 1;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("Boss"))
        {
            other.gameObject.GetComponent<BossHealth>().DoDamage(_damage);
            Kill();
        }        
    }
}
