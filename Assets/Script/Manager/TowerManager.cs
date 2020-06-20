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
    //全部塔的种类
    public TowerData AttackTowerData;
    public TowerData PurificationTowerData;
    //当前被选择的塔属性
    private TowerData selectedTower;

    //ATP
    public double ATP;
    public Text ATPText;
    public Animator ATPChangeAnimation;
    private float time=0;
    public int ATPTimePerIncrease = 1;  //每次ATP增加时间间隔
    public int ATPCountPerIncrease = 1; //每次ATP增加数值 

    //更新面板
    public GameObject UpgradeCanves;
    //塔的隐藏面板按钮
    public GameObject TowerToggle;
    public GameObject TowerDescription;
    //场景中被选中的塔
    public TowerCube SelectedTowerObject;
    public Button UpButton;
    public Button SplitButton;
    void MoneyUpdate(int cost)
    {
        if (ATPText == null)
            return;
        ATP -= cost;
        ATPText.text="ATP "+ATP;
        ATPChangeAnimation.SetTrigger("test");
    }

    void TowerDescriUpdate(TowerData tower)
    {
        
        if(tower.type==TowerType.AttackTower)
            TowerDescription.GetComponentInChildren<Text>().text = "Damage: " + tower.damage + '\n' + "Speed: " + tower.speed + '\n' + "Range: " + tower.range + '\n';
        if(tower.type==TowerType.RecoverTower)

           // TowerDescription.GetComponent<Text>().text = "Recover Value: " + tower.damage + '\n' + "Range: " + tower.range + '\n';

            TowerDescription.GetComponentInChildren<Text>().text = "Damage: " + tower.damage + '\n' + "Range: " + tower.range + '\n';
    }
    private void Update()
    {
        time += Time.deltaTime;
        if (time >= ATPTimePerIncrease)
        {
            time = 0;
            MoneyUpdate(-ATPCountPerIncrease);
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
                bool isCollider = Physics.Raycast(ray, out hit, 1000, LayerMask.GetMask("TowerCube")) ;
                if (isCollider)
                {
                    //得到点击的cube
                    TowerCube cube = hit.collider.GetComponent<TowerCube>();
                    //建造条件有三个必须满足
                    //选了部署池中的塔
                    //选中的cube没有被使用
                    //选中的cube没有被感染
                    if (selectedTower!=null && cube.TowerCubeOn == null && cube.CubeHp>0)
                    {
                        if (ATP >= selectedTower.cost)
                        {
                            //建塔
                            MoneyUpdate(selectedTower.cost);
                            if (selectedTower.type == TowerType.AttackTower)
                            {
                                GameObject BuildTower = selectedTower.TowerPrefab;
                                Tower tower = BuildTower.GetComponent<Tower>();
                                tower.attackRange = selectedTower.range;
                                tower.bulletRate = selectedTower.speed;
                                tower.damage = selectedTower.damage;
                                //tower.bulletPrefab.GetComponent<Bullet>().damageNum = selectedTower.damage;
                            }
                            else if(selectedTower.type==TowerType.RecoverTower)
                            {
                                Debug.Log("净化塔部署成功");
                                GameObject BuildTower = selectedTower.TowerPrefab;
                                RecoverTower tower=BuildTower.GetComponent<RecoverTower>();
                                tower.attackRange = selectedTower.range;
                                tower.recoverNum = selectedTower.damage;
                            }
                            cube.BuildTower(selectedTower);
                        }
                        else
                        {
                        //钱不够
                            ATPChangeAnimation.SetTrigger("nomoney");
                        }
                    }
                    //有炮塔   做升级或拆迁处理
                    else if(cube.TowerCubeOn != null)
                    {
                        TowerDescriUpdate(selectedTower);
                        //若该方块已处于选中状态，再次选中会隐藏升级界面
                        if (cube == SelectedTowerObject && UpgradeCanves.activeInHierarchy)
                        {
                            HideUpgradeUI();
                        }
                        else
                        {
                            if (cube.GetCurrentSplit() == cube.GetSplitLimit())
                                ShowUpgradeUI(cube.transform.position, true, cube.isUpgrade);
                            else
                                ShowUpgradeUI(cube.transform.position, false, cube.isUpgrade);
                        }
                        SelectedTowerObject = cube;
                    }
                }
            }
        }
    }
    //在面板里面添加引用
    public void OnAttackSelected(bool ToggleisOn)
    {
        if (ToggleisOn)
        {
            workOnBuild = true;
            updateTimescale();
            selectedTower = AttackTowerData;
            TowerDescriUpdate(selectedTower);
            TowerDescription.SetActive(true);
            TowerToggle.transform.GetChild(0).transform.localScale = new Vector3(1.5f, 1.5f, 0);
        }
        else
        {
            workOnBuild = false;
            updateTimescale();
            selectedTower = null;
            TowerDescription.SetActive(false);
            TowerToggle.transform.GetChild(0).transform.localScale = new Vector3(1, 1, 0);
        }
    }
    public void OnPurificationSelected(bool ToggleisOn)
    {
        if (ToggleisOn)
        {
            workOnBuild = true;
            updateTimescale();
            selectedTower = PurificationTowerData;
            TowerDescriUpdate(selectedTower);
            TowerDescription.SetActive(true);
            TowerToggle.transform.GetChild(1).transform.localScale=new Vector3(1.5f, 1.5f, 0);
        }
        else
        {
            workOnBuild = false;
            updateTimescale();
            selectedTower = null;
            TowerDescription.SetActive(false);
            TowerToggle.transform.GetChild(1).transform.localScale = new Vector3(1, 1, 0);
        }
    }

    public void OnUpgradeButtonDown()
    {
        selectedTower = SelectedTowerObject.Getdata();
        if (ATP >= selectedTower.Upcost)
        {
            MoneyUpdate(selectedTower.Upcost);
            SelectedTowerObject.UpgradeTower();
            TowerDescriUpdate(selectedTower);
        }
        else
        {
            ATPChangeAnimation.SetTrigger("nomoney");
        }
        HideUpgradeUI();
    }
    public void OnDestroyButtonDown()
    {
        SelectedTowerObject.DestroyTower();
        HideUpgradeUI();
    }
    public void OnSplitButtonDown()
    {
        selectedTower = SelectedTowerObject.Getdata();
        if (ATP >= selectedTower.SplitCost[SelectedTowerObject.GetCurrentSplit()])
        {
            MoneyUpdate(selectedTower.SplitCost[SelectedTowerObject.GetCurrentSplit()]);
            SelectedTowerObject.SplitTower();
            TowerDescriUpdate(selectedTower);
        }
        else
        {
            ATPChangeAnimation.SetTrigger("nomoney");
        }
        HideUpgradeUI();
    }
    public void OnTowerChoseButtonDown()
    {

        TowerToggle.SetActive(!TowerToggle.activeInHierarchy);
        for(int i = 0; i < TowerToggle.transform.childCount; i++)
        {
            TowerToggle.transform.GetChild(i).GetComponent<Toggle>().isOn = false;
        }
     }
  
    //升级画布UI
    void ShowUpgradeUI(Vector3 pos, bool isDisableSplit,bool isDisableUpgrade)
    {
        workOnUp = true;
        updateTimescale();
        //显示面板
        UpgradeCanves.SetActive(true);
        UpgradeCanves.transform.position = pos;
        //是否可以升级
        UpButton.interactable = !isDisableUpgrade;
        SplitButton.interactable = !isDisableSplit;
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
