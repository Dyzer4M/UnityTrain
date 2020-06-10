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

    //ATP
    public double ATP;
    public Text ATPtext;
    public Animator ATPreduce;
    private float time=0;
    public int ATPincreasetime;

    //更新面板
    public GameObject UpgradeCanves;
    //塔的隐藏面板按钮
    public GameObject TowerToggle;
    public GameObject TowerDescription;
    //场景中被选中的塔
    private TowerCube SelectedTowerObject;
    public Button UpButton;
    void MoneyUpdate(int cost)
    {
        ATP -= cost;
        ATPtext.text="ATP "+ATP;
        ATPreduce.SetTrigger("test");
    }

    void TowerDescriUpdate(TowerData tower)
    {
        TowerDescription.SetActive(true);
        TowerDescription.GetComponent<Text>().text = "Damage: " + tower.damage + '\n' + "Speed: " + tower.speed + '\n' + "Range: " + tower.range + '\n';
    }
    private void Update()
    {
        time += Time.deltaTime;
        if (time >= ATPincreasetime)
        {
            time = 0;
            MoneyUpdate(-50);
        }

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
                        if (ATP >= selectedTower.cost)
                        {
                            //建塔
                            MoneyUpdate(selectedTower.cost);
                          
                            cube.BuildTower(selectedTower);
                        }
                        else
                        {
                        //钱不够
                            ATPreduce.SetTrigger("nomoney");
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
            TowerDescriUpdate(selectedTower);
            TowerToggle.transform.GetChild(0).transform.localScale = new Vector3(1.5f, 1.5f, 0);
        }
        else
        {
            TowerDescription.SetActive(false);
            TowerToggle.transform.GetChild(0).transform.localScale = new Vector3(1, 1, 0);
        }
    }
    public void OnStandardSelected1(bool ToggleisOn)
    {
        if (ToggleisOn)
        {
            selectedTower = selectedTower1;
            TowerDescriUpdate(selectedTower);
            TowerToggle.transform.GetChild(1).transform.localScale=new Vector3(1.5f, 1.5f, 0);
        }
        else
        {
            TowerDescription.SetActive(false);
            TowerToggle.transform.GetChild(1).transform.localScale = new Vector3(1, 1, 0);
        }
    }
    public void OnStandardSelected2(bool ToggleisOn)
    {
        if (ToggleisOn)
        {
            selectedTower = selectedTower2;
            TowerDescriUpdate(selectedTower);
            TowerToggle.transform.GetChild(2).transform.localScale = new Vector3(1.5f, 1.5f, 0);
        }
        else
        {
            TowerDescription.SetActive(false);
            TowerToggle.transform.GetChild(2).transform.localScale = new Vector3(1, 1, 0);
        }
    }
    public void OnUpgradeButtonDown()
    {
        if (ATP >= selectedTower.Upcost)
        {
            MoneyUpdate(selectedTower.Upcost);
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
        SelectedTowerObject.DestroyTower();
        HideUpgradeUI();
    }
    public void OnTowerChoseButtonDown()
    {
        TowerToggle.SetActive(!TowerToggle.activeInHierarchy);
        for(int i = 0; i < TowerToggle.transform.childCount; i++)
        {
            TowerToggle.transform.GetChild(i).GetComponent<Toggle>().isOn = false;
        }
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
