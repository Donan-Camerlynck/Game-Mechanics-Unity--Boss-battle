using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class BossHealth : MonoBehaviour
{
    private int _health;
    [SerializeField]
    private int _maxHealth = 15;
    public Slider _healthSlider;


    private bool _vulnerable;
    public bool alive;

    
    // Start is called before the first frame update
    void Start()
    {
 
        _health = _maxHealth;
        _healthSlider.maxValue = _maxHealth;
        _vulnerable = true;
        alive = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (_health <= 0)
        {
            //die
            print("boss dead");
            _vulnerable = false;
            alive = false;
            StartCoroutine(Die());
        }
        _healthSlider.value = _health;

    }

    public void DoDamage(int damage)
    {
        if (_vulnerable)
        {
            _health -= damage;
            print("damage taken");
        }
    }
    IEnumerator Die()
    {
        
        yield return new WaitForSeconds(5.0f);
        
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
