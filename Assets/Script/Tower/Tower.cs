using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tower : MonoBehaviour
{
    private TowerCube cubeon;
    private float attackRange=4;
    public string enemyTag = "Enemy";
    public Transform attackTarget;
    public Transform bullet;
    public GameObject bulletPrefab;//子弹的主体
    public float rotSpeed = 10;
    public float bulletRate = 2f;//发射子弹的速度
    private float countDown = 0;

    public void SetCube(TowerCube cube)
    {
        this.cubeon = cube;
    }
    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating("UpdateTarget", 0, 0.5f);
        countDown = 1 / bulletRate;
    }

    // Update is called once per frame
    void Update()
    {
        if (attackTarget == null) return;
        countDown -= Time.deltaTime;
        if (countDown <= 0)
        {
            GameObject bulletGo=Instantiate(bulletPrefab, this.transform.position,this.transform.rotation);
            Bullet bullet = bulletGo.AddComponent<Bullet>();
            if (bullet == null)
            {
                bulletGo.AddComponent<Bullet>();
            }
            bullet.SetTarget(attackTarget);
            bullet.SetFather(this) ;
            countDown = 1 / bulletRate;
        }
    }

    public void ResetCountDown()
    {
        countDown = 0;
    }
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(this.transform.position, attackRange);   
    }
    private void UpdateTarget()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag(enemyTag);
        float minDistance = Mathf.Infinity;
        Transform nearnestEnemy = null;
        foreach(var enemy in enemies)
        {
            float distance =Vector3.Distance(enemy.transform.position, this.transform.position);
            if (distance < minDistance)
            {
                EnemyHealth targetEnemyHealth = enemy.GetComponent<EnemyHealth>();
                if (targetEnemyHealth.isAlive())//防止子弹攻击空血但仍未destroy的敌人
                {
                    minDistance = distance;
                    nearnestEnemy = enemy.transform;
                }
            }
        }
        if (minDistance < attackRange)
        {
            attackTarget = nearnestEnemy;
        }
        else
        {
            attackTarget = null;
        }
    }
}
