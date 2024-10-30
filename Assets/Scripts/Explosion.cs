using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explosion : MonoBehaviour
{
    [SerializeField]
    private int _damage = 1;
    bool _hasHit = false;
    // Start is called before the first frame update
    void Start()
    {
        Invoke("Kill", 0.25f);
    }

    // Update is called once per frame
    void Update()
    {
        transform.localScale = new Vector3(transform.localScale.x + 50.0f * Time.deltaTime, transform.localScale.y + 50.0f * Time.deltaTime, transform.localScale.z + 50.0f * Time.deltaTime);

    }



    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player") && !_hasHit)
        {
            //damage player
            print("Explosion HIT");
            other.GetComponent<PlayerHealth>().DoDamage(_damage);
            _hasHit = true;
        }
    }

    private void Kill()
    {
        Destroy(gameObject);
    }
}
