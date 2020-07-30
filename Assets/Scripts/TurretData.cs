using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
//炮台数据脚本
public class TurretData
{
    //初级炮台及其价格，升级后的炮台及升级价格
    public GameObject turretPrefab;
    public int cost;
    public GameObject turretUpgradedPrefab;
    public int costUpgraded;
}
//炮台的种类
public enum TurretType
{
    LaserTurret,
    StandardTurret
}