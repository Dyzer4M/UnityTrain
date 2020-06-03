using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerCube : MonoBehaviour
{
    
    [HideInInspector]
    //被选中的cube
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
    public Canvas build;//创建不同塔的界面
    private void Start()
    {
        render = GetComponent<MeshRenderer>();
        initColor = render.material.color;
        //build.enabled = false;
    }

    public void BuildTower(TowerData tower)
    {
        this.towerdata = tower;
        isUpgrade = false;
        TowerCubeOn = GameObject.Instantiate(towerdata.TowerPrefab, transform.position, Quaternion.identity);
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
