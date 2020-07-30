using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 保存每一波敌人生成所需要的属性
[System.Serializable]
public class Wave
{
    //生成敌人的种类，数量以及生成速率
    public GameObject enemyPrefab;
    public int count;
    public float rate;
}
