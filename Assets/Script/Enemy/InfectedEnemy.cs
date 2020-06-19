using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]
public struct TriggerInfo
{

    public GameObject cube;
    public float time;
}

public class InfectedEnemy : Enemy
{
    public float infectedRadius;
    public float infectedInterval=1f;
    public float infectedDamge = 10;
    public List<TriggerInfo> triggerInfo;
    new void Awake()
    {
        base.Awake();
    }
    // Start is called before the first frame update
    new void Start()
    {
        base.Start();
    }

    // Update is called once per frame
    new void Update()
    {
        base.Update();
        for(int i=0;i<triggerInfo.Count;i++)
        {
            var info = triggerInfo[i];
            info.time += Time.deltaTime;
            if (info.time >= infectedInterval)
            {
                info.time = 0;
                TowerCube cube = info.cube.GetComponent<TowerCube>();
                cube.HpSetting(-infectedDamge);
            }
            triggerInfo[i] = info;
        }

    }

    private void OnTriggerEnter(Collider other)
    {
        if (!triggerInfo.Exists(cube => cube.cube == other.gameObject))
        {
            triggerInfo.Add(new TriggerInfo { cube=other.gameObject,time=0 });
        }
    }




    private void OnTriggerExit(Collider other)
    {
        //Debug.Log(other.gameObject.name);
        //Debug.Log("移除");
        if (triggerInfo.Exists(cube => cube.cube == other.gameObject))
        {
            TriggerInfo info = triggerInfo.Find(ele => ele.cube == other.gameObject);
            triggerInfo.Remove(info);
        }
    }

}
