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
    private Animator anim;
    // Start is called before the first frame update
    void Start()
    {
        recoverCube = new List<GameObject>();
        RecoverCube();
        anim = GetComponentInChildren<Animator>();
        //anim.SetBool("Active", true);
        
    }

    void Update()
    {
    }

    private void OnDestroy()
    {
        resetCube();
    }

    private void SetCube(TowerCube cube)
    {

        this.cubeon = cube;
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(this.transform.position, attackRange);
    }

    /// <summary>
    /// 为范围内的塔台增加恢复值
    /// </summary>
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
                CubeScript.changeDamage( this.recoverNum);
            }
        }
        //Debug.Log(recoverCube.Count);
    }
    
    /// <summary>
    /// 恢复净化塔范围内的塔台的恢复值
    /// </summary>
    private void resetCube()
    {
        for(int i = 0; i < recoverCube.Count; i++)
        {
            TowerCube CubeScript = recoverCube[i].GetComponent<TowerCube>();
            CubeScript.changeDamage(-this.recoverNum);
        }
    }


}
