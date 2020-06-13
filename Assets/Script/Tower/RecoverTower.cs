using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RecoverTower : MonoBehaviour
{
    private TowerCube cubeon;
    public float attackRange = 0.0f;
    public string cubeTag = "TowerCube";
    private GameObject[] ExistCube;
    private List<GameObject> recoverCube;
    public float recoverNum;
    // Start is called before the first frame update
    void Start()
    {
        recoverCube = new List<GameObject>();
        RecoverCube();
    }
    private void OnDestroy()
    {
        resetCube();
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
    private void SetCube(TowerCube cube)
    {
        this.cubeon = cube;
    }
    private void RecoverCube()
    {
        ExistCube = GameObject.FindGameObjectsWithTag(cubeTag);
        
        foreach(var cube in ExistCube)
        {
            float distance = Vector3.Distance(cube.transform.position, this.transform.position);
            if (distance <= attackRange)
            {
                recoverCube.Add(cube);
                TowerCube CubeScript = cube.GetComponent<TowerCube>();
                CubeScript.damage += this.recoverNum;
            }
        }
        Debug.Log(recoverCube.Count);
    }
    
    private void resetCube()
    {
        for(int i = 0; i < recoverCube.Count; i++)
        {
            TowerCube CubeScript = recoverCube[i].GetComponent<TowerCube>();
            CubeScript.damage -= this.recoverNum;
        }
    }
}
