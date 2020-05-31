using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerCube : MonoBehaviour
{
    [HideInInspector]
    public GameObject TowerCubeOn;

    public void BuildTower(GameObject towerprefab)
    {
        TowerCubeOn = GameObject.Instantiate(towerprefab, transform.position, Quaternion.identity);
    }
}
