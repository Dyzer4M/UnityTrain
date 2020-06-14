using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
//保存塔的数据
public class TowerData
{
    public int damage;
    public int range;
    public int speed;
    public int SplitUpdamage = 10;
    public int damageUpdate;
    public int rangeUpdate;
    public int speedUpdate;
    public int Updamage;
    public GameObject TowerPrefab;
    public int cost;
    public GameObject UpgradePrefab;
    public int Upcost;
    public List<GameObject> SplitPrefab;
    public List<int> SplitCost;
    public TowerType type;
}
public enum TowerType
{
    AttackTower,
    RecoverTower
}

