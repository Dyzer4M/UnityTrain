using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerCube : MonoBehaviour
{
    
    [HideInInspector]
    public GameObject TowerCubeOn;
    private Renderer render;
    private Color initColor;
    private Color changeColor=Color.blue;
    private void Start()
    {
        render = GetComponent<MeshRenderer>();
        initColor = render.material.color;
    }
    public void BuildTower(GameObject towerprefab)
    {
        TowerCubeOn = GameObject.Instantiate(towerprefab, transform.position, Quaternion.identity);
    }
    private void OnMouseEnter()
    {
        render.material.color = changeColor;
    }
    private void OnMouseExit()
    {
        render.material.color = initColor;
    }
}
