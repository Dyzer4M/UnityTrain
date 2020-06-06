using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
public class TowerCube : MonoBehaviour
{
    [HideInInspector] //被选中的cube的塔
    public GameObject TowerCubeOn;
    //塔属性
    private TowerData towerdata;
    //塔是否升级
    public bool isUpgrade = false;
    //塔的建造特效
    public GameObject buildeffect;
    //改变cube颜色
    private Renderer render;
    private Color initColor;
    private Color changeColor = Color.blue;
    private Color CurrentColor;
    public Color InfectedColor = Color.red;
    public Canvas buildCanves;//创建不同塔的界面
    //cube的感染值
    public float damage = 10;
    public float CubeHp = 0.0f;
    //public void ChangeHP(int damage)
    //{
    //    CubeHp -= damage;
    //    if (CubeHp < 0)
    //    {
    //        CubeHp = 0;
    //    }
    //}
    private void HpDamage()
    {
        this.CubeHp -= 1;
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
        //如果被感染了，让脚本失活，cube变色
        if (CubeHp <= 100)
        {
            Vector3 nowRGB = new Vector3(render.material.color.r, render.material.color.g, render.material.color.b);
            Vector3 newRGB = new Vector3(CubeHp / 100.0f, (100 - CubeHp) / 100.0f, 0);
            render.material.color = new Color(Vector3.Lerp(nowRGB, newRGB, Time.deltaTime).x, Vector3.Lerp(nowRGB, newRGB, Time.deltaTime).y, Vector3.Lerp(nowRGB, newRGB, Time.deltaTime).z);
            if (TowerCubeOn != null)
            //TowerCubeOn.GetComponent<Tower>().enabled = false;
                Destroy(TowerCubeOn);
        }
    }
    public void BuildTower(TowerData tower)
    {
        this.towerdata = tower;
        isUpgrade = false;
        TowerCubeOn = GameObject.Instantiate(towerdata.TowerPrefab, this.transform.position, Quaternion.identity);

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
    }
    public void UpgradeTower()
    {
        if (isUpgrade == true)
            return;
        Destroy(TowerCubeOn);
        isUpgrade = true;
        TowerCubeOn = GameObject.Instantiate(towerdata.UpgradePrefab, transform.position, Quaternion.identity);
        //GameObject effect = GameObject.Instantiate(buildeffect, transform.position, Quaternion.identity);
        //Destroy(effect, 1);

    }
    public void HpSetting(float num)
    {
        if (CubeHp < 0)
        {
            CubeHp = 0;
            return;
        }
        if (CubeHp > 100)
        {
            CubeHp = 100;
            return;
        }
        CubeHp += num;
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
