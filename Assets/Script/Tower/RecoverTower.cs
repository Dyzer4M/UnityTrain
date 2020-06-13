using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RecoverTower : MonoBehaviour
{
    private TowerCube cubeon;
    public float attackRange = 4.0f;
    public string cubeTag = "TowerCube";
    private GameObject[] Cube;
    private List<GameObject> recoverCube;
    public 
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(this.transform.position, attackRange);
    }
    private void RecoverTarget()
    {
        Cube = GameObject.FindGameObjectsWithTag(cubeTag);
        foreach(var Cube in recoverCube)
        {
            float distance = Vector3.Distance(Cube.transform.position, this.transform.position);
            if (distance <= attackRange)
            {
                recoverCube.Add(Cube);
            }
        }
    }
    private void SetCube(TowerCube cube)
    {
        this.cubeon = cube;
    }
}
