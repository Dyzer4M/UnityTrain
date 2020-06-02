using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class EnemyHealth : MonoBehaviour
{
    public float initHealth = 100;
    private float currentHealth;
    public Image hpBar;
    // Start is called before the first frame update
    void Start()
    {
        currentHealth = initHealth;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void Damage(float amount)
    {
        currentHealth -= amount;
        hpBar.fillAmount = currentHealth / initHealth;
        if (currentHealth <= 0)
        {
            EnemyManager.EnemyAliveCount--;
            Destroy(this.gameObject);
        }
    }

    public bool isAlive()
    {
        return currentHealth > 0;
    }
}
