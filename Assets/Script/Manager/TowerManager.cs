using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

[System.Serializable]
public class TowerManager : MonoBehaviour
{
    //塔的种类
    public TowerData StandardTowerData;


    //被选择的塔（目前只设置了一种）
    public TowerData selectedTower;
    public int money;
    private void Update()
    {
        //检测鼠标按下
        if (Input.GetMouseButtonDown(0))
        {
            //检测鼠标点击位置（按在UI的时候不做建造处理）
            if (EventSystem.current.IsPointerOverGameObject()==false)
            {
                //鼠标点转化为射线进行检测
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                RaycastHit hit;
                //最大检测距离1000
                bool isCollider = Physics.Raycast(ray, out hit, 1000, LayerMask.GetMask("TowerCube"));
                if (isCollider)
                {
                    //得到点击的cube
                    TowerCube cube = hit.collider.GetComponent<TowerCube>();
                    if (cube.TowerCubeOn == null)
                    {
                        if (money >= selectedTower.cost)
                        {
                            money -= selectedTower.cost;
                            cube.BuildTower(selectedTower.TowerPrefab);
                        }
                    }
                    else
                    {
                        //钱不够添加UI
                    }
                }
            }
        }
    }
    public void OnStandardSelected(bool ToggleisOn)
    {
        if (ToggleisOn)
        {
            selectedTower = StandardTowerData;
        }
    }
}
