using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using UnityEngine;
using UnityEngine.AI;

public class Enemy :MonoBehaviour
{
    // Start is called before the first frame update
    public List<Vector3> mPath = new List<Vector3>();
    
    public float mMoveSpeed=1;
    public void InitData(List<Vector3> path)
    {
        mPath.Add(new Vector3(-5,0, 0));
        mPath.Add(new Vector3(5, 0, 5));
        mPath.Add(new Vector3(0, 0, 3));
        mPath.Add(new Vector3(-5,0, 3));
        if (this.mPath.Count > 0)
        {
            this.transform.localPosition = this.mPath[0];
        }
    }
    void Start()
    {
        InitData(mPath);
    }
    void Update()
    {
        if (this.mPath.Count > 0)
        {
            Vector3 pos = this.mPath[0];
            float range;
            if (this.transform.localPosition.x == pos.x)
            {
                range = Math.Abs(this.transform.localPosition.z - pos.z);
                
            }
            else
            {
                pos.z = this.transform.localPosition.z;
                range = Math.Abs(this.transform.localPosition.x - pos.x);
            }
            float tutal_time = range / this.mMoveSpeed;
            if (Time.deltaTime >= tutal_time)
            {
                this.transform.localPosition = pos;
                if (this.transform.localPosition == this.mPath[0])
                {
                    this.mPath.RemoveAt(0);
                }
            }
            else
            {
                this.transform.localPosition = Vector3.Lerp(this.transform.localPosition, pos, Time.deltaTime / tutal_time);
            }
        }
        else
        {
            this.DeleteObj();
        }
    }
    public void DeleteObj()
    {
        Destroy(this.gameObject);
    }
}
