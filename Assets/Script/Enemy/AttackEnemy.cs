using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackEnemy : Enemy
{
    private Tower target;
    public float searchRange;

    new void Awake()
    {
        base.Awake();
    }
    // Start is called before the first frame update
    new void Start()
    {
        base.Start();
    }


    // Update is called once per frame
    new void Update()
    {
        base.Update();

        if (target)
        {
            Attack();
        }
        else
        {
            SearchEnemy();
        }
    }
    /// <summary>
    /// 搜索searchRange半径内是否存在敌人
    /// </summary>
    public void SearchEnemy()
    {

    }
    /// <summary>
    /// 若target目标不为空则进行攻击
    /// </summary>
    public void Attack()
    {

    }

}
