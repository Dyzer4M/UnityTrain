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
    public GameObject TowerPrefab;
    public int cost;
    public GameObject UpgradePrefab;
    public int Upcost;
    public TowerType type;
}
public enum TowerType
{
    StandardTower,
    RecoverTower
}

