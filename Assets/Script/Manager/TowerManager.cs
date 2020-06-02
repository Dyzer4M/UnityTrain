using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

[System.Serializable]
public class TowerManager : MonoBehaviour
{
    //塔的种类
    public TowerData StandardTowerData;

    //被选择的塔属性（目前只设置了一种）
    private TowerData selectedTower;

    //金钱
    public int money = 1000;
    public Text Moneytext;
    public Animator moneyreduce;

    //更新面板
    public GameObject UpgradeCanves;
    //塔的隐藏面板按钮
    public GameObject TowerToggle;
    //场景中被选中的塔
    private TowerCube SelectedTowerObject;
    public Button UpButton;
    void MoneyUpdate(int cost)
    {
        money -= cost;
        Moneytext.text="$"+money;
    }
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
                    if (selectedTower!=null && cube.TowerCubeOn == null)
                    {
                        if (money >= selectedTower.cost)
                        {
                            //建塔
                            MoneyUpdate(selectedTower.cost);
                            moneyreduce.SetTrigger("test");
                            cube.BuildTower(selectedTower);
                        }
                        else
                        {
                        //钱不够
                            moneyreduce.SetTrigger("nomoney");
                        }
                    }
                    //有炮塔   做升级或拆迁处理
                    else if(cube.TowerCubeOn != null)
                    {
                        //是否选中同一炮塔并且画布是否被使用
                        if(cube == SelectedTowerObject && UpgradeCanves.activeInHierarchy)
                        {
                            HideUpgradeUI();
                        }
                        else
                        {
                            ShowUpgradeUI(cube.transform.position, cube.isUpgrade);
                        }
                        SelectedTowerObject = cube;
                    }
                }
            }
        }
    }
    //在面板里面添加引用
    public void OnStandardSelected(bool ToggleisOn)
    {
        if (ToggleisOn)
        {
            selectedTower = StandardTowerData;
        }
    }
   public void OnUpgradeButtonDown()
    {
        SelectedTowerObject.UpgradeTower();
        HideUpgradeUI();
    }
    public void OnDestroyButtonDown()
    {
        SelectedTowerObject.DestroyTower();
        HideUpgradeUI();
    }
    public void OnTowerChoseButtonDown()
    {
        TowerToggle.SetActive(!TowerToggle.activeInHierarchy);
        
    }
    //升级画布UI
    void ShowUpgradeUI(Vector3 pos, bool isDisableUpgrade = false)
    {
        //显示面板
        UpgradeCanves.SetActive(true);
        UpgradeCanves.transform.position = pos;
        //是否可以升级
        UpButton.interactable = !isDisableUpgrade;
    }
    void HideUpgradeUI()
    {
        UpgradeCanves.SetActive(false);
    }


}
