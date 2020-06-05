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
    private Color changeColor=Color.blue;
    public Color InfectedColor = Color.red;
    public Canvas buildCanves;//创建不同塔的界面

    //cube的感染值
    public int CubeHp = 100;
    public void ChangeHP(int damage)
    {
        CubeHp -= damage;
        if (CubeHp < 0)
        {
            CubeHp = 0;
        }
    }

    private void Start()
    {
        render = GetComponent<MeshRenderer>();
        initColor = render.material.color;
        //build.enabled = false;
    }
    private void Update()
    {
        //如果被感染了，让脚本失活，cube变色
        if (CubeHp <= 0)
        {
            render.material.color = InfectedColor;
            if(TowerCubeOn!=null)
                TowerCubeOn.GetComponent<Tower>().enabled = false;
        }
        else
        {
            render.material.color = initColor;
            if (TowerCubeOn != null)
                TowerCubeOn.GetComponent<Tower>().enabled = true ;
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
