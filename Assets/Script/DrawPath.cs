using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Text;
using UnityEngine;
using UnityEditor;
using System.IO;

public class DrawPath : MonoBehaviour
{
    public List<Vector3> MappList = new List<Vector3>();
    // Start is called before the first frame update
    void Start()
    {
        //MappList.Add(Vector3.zero);
        MappList.Add(new Vector3(-5, 0, 0));
        MappList.Add(new Vector3(5, 0, 5));
        MappList.Add(new Vector3(0, 0, 3));
        MappList.Add(new Vector3(-5, 0, 3));
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.W))
        {
            Vector3 pos = Camera.main.transform.localPosition;
            pos.y += 1;
            Debug.Log("W按下");
        }
        if (Input.GetKeyDown(KeyCode.A))
        {
            Vector3 pos = Camera.main.transform.localPosition;
            pos.x -= 1;
            Debug.Log("W按下移动");
        }
        if (Input.GetKeyUp(KeyCode.D))
        {
            Vector3 pos = Camera.main.transform.localPosition;
            pos.x += 1;
            Debug.Log("W松开");
        }
        if (Input.GetKeyUp(KeyCode.S))
        {
            Vector3 pos = Camera.main.transform.localPosition;
            pos.y -= 1;
            Debug.Log("W松开");
        }
        if (Input.GetMouseButtonDown(0))//添加路径
        {
            Vector3 pos = Input.mousePosition;
            pos.z = 10;
            pos = Camera.main.ScreenToWorldPoint(pos);
            Debug.Log(pos);

            
            pos.x = Mathf.RoundToInt(pos.x);
            pos.z = Mathf.RoundToInt(pos.z);
            this.MappList.Add(pos);
        }
        if (Input.GetMouseButtonDown(1))//取消路径
        {
            if (this.MappList.Count > 0)
                this.MappList.RemoveAt(this.MappList.Count - 1);
        }
        if (Input.GetAxis("Mouse ScrollWheel") > 0)//滚轮操作
        {
            Camera.main.orthographicSize *= 0.9f;
            Debug.Log("大于0:" + Input.GetAxis("Mouse ScrollWheel"));
        }
        if (Input.GetAxis("Mouse ScrollWheel") < 0)
        {
            Camera.main.orthographicSize *= 1.1f;
            Debug.Log("小于0:" + Input.GetAxis("Mouse ScrollWheel"));
        }
        if (Input.GetKeyDown(KeyCode.F2))
        {
            StringBuilder sb = new StringBuilder();
            for(int i = 0; i < this.MappList.Count; i++)
            {
                sb.AppendLine(string.Format("{0},{1}", this.MappList[i].x, this.MappList[i].z));
            }
            string filepath=EditorUtility.SaveFilePanel("保存地图文件", ",",DateTime.Now.ToString("yyyyMMddHHmm"), "txt");
            File.WriteAllText(filepath, sb.ToString());
        }
   
    }
    public void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        if (MappList.Count > 0)
            Gizmos.DrawCube(MappList[0], Vector3.one);
        Gizmos.color = Color.red;
        for (int i = 1; i < MappList.Count; i++)
        {
            int _x, _z;
            _x = (int)MappList[i - 1].x;
            _z = (int)MappList[i - 1].z;
            while (_x > MappList[i].x)
            {
                _x--;
                Gizmos.DrawCube(new Vector3(_x, 0, _z), Vector3.one);
            }
            while (_x < MappList[i].x)
            {
                _x++;
                Gizmos.DrawCube(new Vector3(_x, 0, _z), Vector3.one);
            }
            while (_z > MappList[i].z)
            {
                _z--;
                Gizmos.DrawCube(new Vector3(_x, 0, _z), Vector3.one);
            }
            while (_z < MappList[i].z)
            {
                _z++; 
                Gizmos.DrawCube(new Vector3(_x, 0, _z), Vector3.one);
            }
        }
        Gizmos.color = Color.yellow;
        Gizmos.DrawCube(MappList[MappList.Count - 1], Vector3.one);
        
    }
  
}

