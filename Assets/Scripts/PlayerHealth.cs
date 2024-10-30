using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{    
    private int _health;
    [SerializeField]
    private int _maxHealth = 15;
    public Slider healthSlider;

    [SerializeField]
    private AudioSource healthAudioSource;

    private bool _vulnerable;
    public bool alive;

    

    // Start is called before the first frame update
    void Start()
    {

        _health = _maxHealth;
        healthSlider.maxValue = _maxHealth;
        _vulnerable = true;
        alive = true;
    }

    // Update is called once per frame
    void Update()
    {
        if(_health <= 0)
        {
            //die
            StartCoroutine(Die());
            _vulnerable = false;
            alive = false;
        }       
        healthSlider.value = _health;
        
    }

    public void DoDamage(int damage)
    {
        if (_vulnerable)
        {
            _health -= damage;
            print("damage taken");
            healthAudioSource.Play();
        }
    }

    public void RestoreHealth(int health) 
    {
        _health += health;
        if(_health > _maxHealth)
        {
            _health = _maxHealth;
        }
    }

    IEnumerator Die()
    {
    
        yield return new WaitForSeconds(5.0f);        
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public IEnumerator Dash()
    {
        _vulnerable = false;
        yield return new WaitForSeconds(0.2f);
        _vulnerable = true;
    }
}
