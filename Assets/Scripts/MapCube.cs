using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

//地图砖块的脚本
public class MapCube : MonoBehaviour
{
    [HideInInspector]
    public GameObject turretGo;//保存当前Cube上的炮台
    [HideInInspector]
    public TurretData turretData;
    [HideInInspector]
    public bool isUpgrade = false; //炮台是否升级过

    public GameObject buildEffect;//建造炮台的粒子特效

    //private Renderer renderer;
    private void Start()
    {
        
    }
    //建造炮台的函数
    public void BuildTurret(TurretData turretData)
    {
        this.turretData = turretData;
        isUpgrade = false;
        //生成炮台和特效,并在1秒后销毁特效
        turretGo = Instantiate(turretData.turretPrefab, transform.position, Quaternion.identity);
        GameObject effect = Instantiate(buildEffect, transform.position, Quaternion.identity);
        Destroy(effect, 1);
    }

    //升级炮台的函数
    public void UpgradeTurret()
    {
        if (isUpgrade == true) return;
        Destroy(turretGo);
        isUpgrade = true;
        GameObject effect = Instantiate(buildEffect, transform.position, Quaternion.identity);
        Destroy(effect, 1);
        turretGo = Instantiate(turretData.turretUpgradedPrefab, transform.position, Quaternion.identity);
    }
    //拆除炮台的函数
    public void DestoryTurret()
    {
        GameObject effect = Instantiate(buildEffect, transform.position, Quaternion.identity);
        Destroy(effect, 0.6f);
        Destroy(turretGo);
        isUpgrade = false;
        turretGo = null;
        turretData = null;
    }
    //鼠标移动到地图砖块上触发的函数
    private void OnMouseEnter()
    {
        //条件符合时,地图砖块变红
       /* if (turretGo == null && EventSystem.current.IsPointerOverGameObject()==false)
        {
            renderer.material.color = Color.red;
        }*/
    }

    //鼠标移除地图砖块时触发的函数
    private void OnMouseExit()
    {
        GetComponent<Renderer>().material.color = Color.white;
    }
}
