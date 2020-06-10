using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

[System.Serializable]
public class TowerManager : MonoBehaviour
{
    public TimescaleManager timeChanger;
    public bool workOnBuild = false;
    public bool workOnUp = false;
    //塔的种类
    public TowerData StandardTowerData;
    public TowerData selectedTower1;
    public TowerData selectedTower2;
    //被选择的塔属性（目前只设置了一种）
    public  TowerData selectedTower;

    //金钱
    public double money;
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
        Moneytext.text="ATP "+money;
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
                    //建造条件有三个必须满足
                    //选了塔
                    //选中的cube没有被使用
                    //选中的cube没有被感染
                    if (selectedTower!=null && cube.TowerCubeOn == null && cube.CubeHp>0)
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
            TowerToggle.transform.GetChild(0).transform.localScale = new Vector3(1.5f, 1.5f, 0);
        }
        else
        {
            TowerToggle.transform.GetChild(0).transform.localScale = new Vector3(1, 1, 0);
        }
    }
    public void OnStandardSelected1(bool ToggleisOn)
    {
        if (ToggleisOn)
        {
            selectedTower = selectedTower1;
            TowerToggle.transform.GetChild(1).transform.localScale=new Vector3(1.5f, 1.5f, 0);
        }
        else
        {
            TowerToggle.transform.GetChild(1).transform.localScale = new Vector3(1, 1, 0);
        }
    }
    public void OnStandardSelected2(bool ToggleisOn)
    {
        if (ToggleisOn)
        {
            selectedTower = selectedTower2;
            TowerToggle.transform.GetChild(2).transform.localScale = new Vector3(1.5f, 1.5f, 0);
        }
        else
        {
            TowerToggle.transform.GetChild(2).transform.localScale = new Vector3(1, 1, 0);
        }
    }
    public void OnUpgradeButtonDown()
    {
        if (money >= selectedTower.Upcost)
        {
            MoneyUpdate(selectedTower.Upcost);
            moneyreduce.SetTrigger("test");
            SelectedTowerObject.UpgradeTower();
        }
        HideUpgradeUI();
    }
    public void OnDestroyButtonDown()
    {
        if (SelectedTowerObject.isUpgrade)
        {
            MoneyUpdate(-(selectedTower.cost + selectedTower.Upcost) / 2);
        }
        else
        {
            MoneyUpdate(-selectedTower.cost / 2);
        }
        moneyreduce.SetTrigger("test");
        SelectedTowerObject.DestroyTower();
        HideUpgradeUI();
    }
    public void OnTowerChoseButtonDown()
    {
        TowerToggle.SetActive(!TowerToggle.activeInHierarchy);
        workOnBuild = !workOnBuild;
        updateTimescale();
    }
  
    //升级画布UI
    void ShowUpgradeUI(Vector3 pos, bool isDisableUpgrade = false)
    {
        workOnUp = true;
        updateTimescale();
        //显示面板
        UpgradeCanves.SetActive(true);
        UpgradeCanves.transform.position = pos;
        //是否可以升级
        UpButton.interactable = !isDisableUpgrade;
    }
    void HideUpgradeUI()
    {
        workOnUp = false;
        updateTimescale();
        UpgradeCanves.SetActive(false);
    }
    void updateTimescale()
    {
        timeChanger.working = (workOnBuild || workOnUp);
        timeChanger.UpdateTimescale();
    }

}
