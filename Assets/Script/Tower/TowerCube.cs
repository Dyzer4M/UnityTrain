using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;
public class TowerCube : MonoBehaviour
{
    [HideInInspector] 
    //被选中的cube的塔
    public GameObject TowerCubeOn;
    //塔属性
    private TowerData towerdata;
    //塔分化
    public bool isUpgrade = false;
    //塔的建造特效
    public GameObject buildeffect;
    //改变cube颜色
    private Renderer render;
    private Color initColor;
    private Color changeColor = Color.blue;
    public Color InfectedColor = Color.red;
    public Canvas buildCanves;//创建不同塔的界面
    //cube的感染值
    public float damage = 0; // 这个表现起来更像治愈值，因为病毒会直接对Hp进行攻击
    public float CubeHp = 100.0f;

    //分裂
    private int SplitLimit;
    private int CurrentSplitLevel = 0;
    
    //当前cube上放置的tower的属性值
    public Text TowerDescription;

    //用来处理cube颜色和cube血量用到的时间计量
    private float time = 0.0f;

    public int GetSplitLimit()
    {
        return SplitLimit;
    }
    public int GetCurrentSplit()
    {
        return CurrentSplitLevel;
    }

    public TowerData Getdata()
    {
        return towerdata;
    }

    //选中的已放置的塔的面板
    public void TowerDescriUpdate(TowerData tower)
    {
        if (tower.type == TowerType.AttackTower)
            TowerDescription.text = "Damage: " + tower.damage + '\n' + "Speed: " + tower.speed + '\n' + "Range: " + tower.range + '\n';
        if (tower.type == TowerType.RecoverTower)
            TowerDescription.text = "Recover Value: " + tower.damage + '\n' + "Range: " + tower.range + '\n';
    }
    private void Start()
    {
        render = GetComponent<MeshRenderer>();
        initColor = render.material.color;
        //build.enabled = false;
    }

    private void Update()
    {
        time += Time.deltaTime;
        //每秒恢复damage的感染值
        if (time >= 1)
        {
            time = 0;
            HpSetting(damage);
        }
        //如果被感染了，cube变色
        if (CubeHp <= 100)
        {
            //Vector3 nowRGB = new Vector3(render.material.color.r, render.material.color.g, render.material.color.b);
            //render.material.color = new Color(Vector3.Lerp(nowRGB, newRGB, Time.deltaTime).x, Vector3.Lerp(nowRGB, newRGB, Time.deltaTime).y, Vector3.Lerp(nowRGB, newRGB, Time.deltaTime).z);
            Vector3 newRGB = new Vector3((100 - CubeHp) / 100.0f, CubeHp / 100.0f, 0);
            render.material.color = new Color(newRGB.x, newRGB.y, newRGB.z);

        }
        if (CubeHp <= 0)
        {
            if (TowerCubeOn != null)
                //Destroy(TowerCubeOn);
                DestroyTower();
        }
    }

    //传入TowerData
    public void BuildTower(TowerData tower)
    {
        this.towerdata = (TowerData)tower.Clone();
        SplitLimit = towerdata.SplitPrefab.Count;
        isUpgrade = false;
        TowerCubeOn = GameObject.Instantiate(towerdata.TowerPrefab, this.transform.position, Quaternion.identity);
        TowerDescriUpdate(towerdata);
        //UI
        //GameObject effect = GameObject.Instantiate(buildeffect, transform.position, Quaternion.identity);
        //Destroy(effect, 1);
    }
    public void DestroyTower()
    {
        TowerCubeOn.GetComponentInChildren<Animator>().SetBool("Die", true);
        Destroy(TowerCubeOn,1.5f);
        isUpgrade = false;
        towerdata = null;
        TowerCubeOn = null;
        CurrentSplitLevel = 0;
    }
    public void UpgradeTower()
    {
        if (isUpgrade == true || CurrentSplitLevel < SplitLimit ||
                towerdata.type == TowerType.RecoverTower)
            return;
        Destroy(TowerCubeOn);
        isUpgrade = true;
        
        Random.InitState((int)System.DateTime.Now.Ticks);

        if (towerdata.type == TowerType.AttackTower)
        {
            TowerCubeOn = GameObject.Instantiate(towerdata.UpgradePrefab, transform.position, Quaternion.identity);
            int i = Random.Range(1, 4);
            //随机升级一项属性
            switch (i)
            {
                case 1:
                    TowerCubeOn.GetComponent<Tower>().damage = towerdata.damage + towerdata.damageUpdate;
                    TowerCubeOn.GetComponent<Tower>().attackRange = towerdata.range;
                    TowerCubeOn.GetComponent<Tower>().bulletRate = towerdata.speed;
                    towerdata.damage = (int)TowerCubeOn.GetComponent<Tower>().damage;
                    break;
                case 2:
                    TowerCubeOn.GetComponent<Tower>().damage = towerdata.damage;
                    TowerCubeOn.GetComponent<Tower>().attackRange = towerdata.range + towerdata.rangeUpdate;
                    TowerCubeOn.GetComponent<Tower>().bulletRate = towerdata.speed;
                    towerdata.range = (int)TowerCubeOn.GetComponent<Tower>().attackRange;
                    break;
                case 3:
                    TowerCubeOn.GetComponent<Tower>().damage = towerdata.damage;
                    TowerCubeOn.GetComponent<Tower>().attackRange = towerdata.range;
                    TowerCubeOn.GetComponent<Tower>().bulletRate = towerdata.speed + towerdata.speedUpdate;
                    towerdata.speed = (int)TowerCubeOn.GetComponent<Tower>().bulletRate;
                    break;
            }
        }
        //else if (towerdata.type == TowerType.RecoverTower)
        //{
        //    int i = Random.Range(1, 3);
        //    switch (i)
        //    {
        //        case 1:
        //            towerdata.TowerPrefab.GetComponent<RecoverTower>().recoverNum += towerdata.damageUpdate;
        //            break;
        //        case 2:
        //            towerdata.TowerPrefab.GetComponent<RecoverTower>().attackRange += towerdata.rangeUpdate;
        //            break;
        //    }
        //}
        //GameObject effect = GameObject.Instantiate(buildeffect, transform.position, Quaternion.identity);
        //Destroy(effect, 1);
        TowerDescriUpdate(towerdata);

    }

    public void SplitTower()
    {
        if (CurrentSplitLevel == SplitLimit)
        {
            return;
        }
        else
        {
            Destroy(TowerCubeOn);
            TowerCubeOn = GameObject.Instantiate(towerdata.SplitPrefab[CurrentSplitLevel], transform.position, Quaternion.identity);
           
            CurrentSplitLevel++;

            if (towerdata.type == TowerType.AttackTower)
            {
                TowerCubeOn.GetComponent<Tower>().damage += towerdata.SplitUpdamage* CurrentSplitLevel;
                towerdata.damage += towerdata.SplitUpdamage;
            }
            else if (towerdata.type == TowerType.RecoverTower)
            {
                TowerCubeOn.GetComponent<RecoverTower>().recoverNum += towerdata.SplitUpdamage * CurrentSplitLevel;
                towerdata.damage += towerdata.SplitUpdamage;
            }
        }
        TowerDescriUpdate(towerdata);
    }
    public void HpSetting(float num)
    {
        CubeHp = Mathf.Clamp(CubeHp += num, 0, 100);
    }

    public void changeDamage(float num)
    {
        this.damage += num;
    }

    private void OnMouseEnter()
    {
        render.material.color = changeColor;
    }
    private void OnMouseExit()
    {
        render.material.color = initColor;
    }
    private void OnMouseDown()
    {

    }
}
