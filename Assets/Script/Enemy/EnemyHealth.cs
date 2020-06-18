using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class EnemyHealth : MonoBehaviour
{
    private Animator anim;
    public float initHealth = 100;
    private float currentHealth, healthBeforeDamageShow, hpBarShowDamageDownSpeed=0, timeForhpBarShowDamageDown = 0.5f, timeBeginToDown;
    public Image hpBar, hpBarShowDamage;
    private Light redLight;
    private ParticleSystem ps;
    // Start is called before the first frame update
    void Start()
    {
        currentHealth = initHealth;
        healthBeforeDamageShow = currentHealth;
        anim = GetComponentInChildren<Animator>();
        redLight = GetComponentInChildren<Light>();
        ps= this.transform.Find("PillExplosion").GetComponent<ParticleSystem>();
    }

    // Update is called once per frame
    void Update()
    {
        if(currentHealth<healthBeforeDamageShow)//红血显示需要下降
        {
            healthBeforeDamageShow -= hpBarShowDamageDownSpeed * Time.deltaTime;
            if (healthBeforeDamageShow < currentHealth)
            {
                healthBeforeDamageShow = currentHealth;
                hpBarShowDamageDownSpeed = 0;
            }
            hpBarShowDamage.fillAmount = healthBeforeDamageShow / initHealth;
        }
        if (healthBeforeDamageShow <= 0) return;
            //Destroy(gameObject);Destroy被我放到DestroyEnemy脚本下Die函数里了，可以在动画播放完再destroy
            //这个应该用来检测动画放完没有
    }
    public void Damage(float amount)
    {
        redLight.enabled = true;
        Invoke("ClearEffect", 0.05f);
        currentHealth -= amount;
        if (currentHealth < 0) currentHealth = 0;
        //下面的代码用来控制红血下降速度
        if (hpBarShowDamageDownSpeed == 0) 
        {
            timeBeginToDown = Time.time;
            hpBarShowDamageDownSpeed = (healthBeforeDamageShow - currentHealth) / timeForhpBarShowDamageDown;
        }
        else
        {
            hpBarShowDamageDownSpeed = (healthBeforeDamageShow - currentHealth) / (timeForhpBarShowDamageDown - (Time.time - timeBeginToDown)) ;
        }


        hpBar.fillAmount = currentHealth / initHealth;
        if (!isAlive())
        {
            BeforeDestroy();
        }
    }
    public void BeforeDestroy()
    {
        //游戏中一般都有死亡动画，不应该在currentHealth归零瞬间destroy
        ps.Play();
        anim.SetBool("Die", true);

    }
    public bool isAlive()
    {
        return currentHealth > 0;
    }
    private void ClearEffect()
    {
        redLight.enabled = false;
    }
}
