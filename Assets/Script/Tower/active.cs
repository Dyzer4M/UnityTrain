using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class active : MonoBehaviour
{
    public void SetTowerActive()
    {
        this.GetComponent<Animator>().SetBool("Active", true);
    }
}
