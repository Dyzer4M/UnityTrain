using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;
public class TowerCube : MonoBehaviour
{
    [HideInInspector] //被选中的cube的塔
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
    public float damage = -10;
    public float CubeHp = 100.0f;

    //分裂
    private int SplitLimit;
    private int CurrentSplitLevel = 0;

    public Text TowerDescription;

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
    void TowerDescriUpdate(TowerData tower)
    {
        if (tower.type == TowerType.AttackTower)
            TowerDescription.text = "Damage: " + tower.TowerPrefab.GetComponent<Tower>().damage + '\n' + "Speed: " + tower.TowerPrefab.GetComponent<Tower>().bulletRate + '\n' + "Range: " + tower.TowerPrefab.GetComponent<Tower>().attackRange + '\n';
        if (tower.type == TowerType.RecoverTower)
            TowerDescription.text = "Damage: " + tower.TowerPrefab.GetComponent<RecoverTower>().recoverNum + '\n' + "Range: " + tower.TowerPrefab.GetComponent<RecoverTower>().attackRange + '\n';
    }
    private void Start()
    {
        render = GetComponent<MeshRenderer>();
        initColor = render.material.color;
        //build.enabled = false;
    }
    private float time = 0.0f;
    private void Update()
    {
        time += Time.deltaTime;
        if (time >= 1)
        {
            time = 0;
            HpSetting(damage);
        }
        //如果被感染了，cube变色
        if (CubeHp <= 100)
        {
            Vector3 nowRGB = new Vector3(render.material.color.r, render.material.color.g, render.material.color.b);
            Vector3 newRGB = new Vector3((100 - CubeHp) / 100.0f, CubeHp / 100.0f, 0);
            //render.material.color = new Color(Vector3.Lerp(nowRGB, newRGB, Time.deltaTime).x, Vector3.Lerp(nowRGB, newRGB, Time.deltaTime).y, Vector3.Lerp(nowRGB, newRGB, Time.deltaTime).z);
            render.material.color = new Color(newRGB.x, newRGB.y, newRGB.z);

        }
        if (CubeHp <= 0)
        {
            if (TowerCubeOn != null)
                Destroy(TowerCubeOn);
        }
    }
    public void BuildTower(TowerData tower)
    {
        this.towerdata = tower;
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
        Destroy(TowerCubeOn);
        isUpgrade = false;
        towerdata = null;
        TowerCubeOn = null;
        CurrentSplitLevel = 0;
    }
    public void UpgradeTower()
    {
        if (isUpgrade == true)
            return;
        Destroy(TowerCubeOn);
        isUpgrade = true;
        Random.InitState((int)System.DateTime.Now.Ticks);

        if (towerdata.type == TowerType.AttackTower)
        {
            int i = Random.Range(1, 4);
            switch (i)
            {
                case 1:
                    towerdata.TowerPrefab.GetComponent<Tower>().damage += towerdata.damageUpdate;
                    break;
                case 2:
                    towerdata.TowerPrefab.GetComponent<Tower>().attackRange += towerdata.rangeUpdate;

                    break;
                case 3:
                    towerdata.TowerPrefab.GetComponent<Tower>().bulletRate += towerdata.speedUpdate;
                    break;
            }           
        }
        else if (towerdata.type == TowerType.RecoverTower)
        {
            int i = Random.Range(1, 3);
            switch (i)
            {
                case 1:
                    towerdata.TowerPrefab.GetComponent<RecoverTower>().recoverNum += towerdata.damageUpdate;
                    break;
                case 2:
                    towerdata.TowerPrefab.GetComponent<RecoverTower>().attackRange += towerdata.rangeUpdate;
                    break;
            }
        }
        TowerCubeOn = GameObject.Instantiate(towerdata.UpgradePrefab, transform.position, Quaternion.identity);
        TowerDescriUpdate(towerdata);
        //GameObject effect = GameObject.Instantiate(buildeffect, transform.position, Quaternion.identity);
        //Destroy(effect, 1);

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
            towerdata.TowerPrefab.GetComponent<Tower>().damage += towerdata.SplitUpdamage;
            TowerCubeOn = GameObject.Instantiate(towerdata.SplitPrefab[CurrentSplitLevel], transform.position, Quaternion.identity);
            CurrentSplitLevel++;
        }
        TowerDescriUpdate(towerdata);
    }
    public void HpSetting(float num)
    {

        CubeHp = Mathf.Clamp(CubeHp += num, 0, 100);
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
    public void changeDamage(float num)
    {
        this.damage += num;
    }
}
