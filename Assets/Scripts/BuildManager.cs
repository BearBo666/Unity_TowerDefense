using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

//建造炮台的脚本
public class BuildManager : MonoBehaviour
{
    public TurretData laserTurretData;
    public TurretData standardTurretData;

    //表示当前选择将要创建建造的炮台
    private TurretData selectedTurretData;

    //表示当前选中的炮台
    private MapCube selectedMapCube;

    //显示在游戏界面上的金钱
    public Text moneyText;

    //金钱不足的闪烁动画
    public Animator moneyAnimator;

    //初始金币数量
    private int money = 1000;

    public GameObject upgradeCanvas;
    public Button buttonUpgrad;

    //金钱改变的函数
    public void MoneyChanged(int change=0)
    {
        money += change;
        moneyText.text = "￥" + money;
    }

    void Update()
    {
        //鼠标点击
        if (Input.GetMouseButtonDown(0))
        { 
            if (EventSystem.current.IsPointerOverGameObject()==false)
            {
                //开发炮台的建造
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                RaycastHit hit;
                bool isCollider = Physics.Raycast(ray,out hit, 1000, LayerMask.GetMask("MapCube"));
                //鼠标点击了地面
                if (isCollider)
                {
                    MapCube mapcube = hit.collider.GetComponent<MapCube>(); //得到点击的mapcube
                    //选中了一种并且选中的地面上面没有炮台,可以在当前位置创建
                    if (selectedTurretData!=null&&mapcube.turretGo == null)
                    {
                        //钱够，调用mapcube里的函数创建炮台  
                        if (money >= selectedTurretData.cost)
                        {                              
                            MoneyChanged(-selectedTurretData.cost);
                            mapcube.BuildTurret(selectedTurretData);
                        }
                        else 
                        {
                            //钱不够给出提示
                            moneyAnimator.SetTrigger("flicker");
                        }
                    }
                    //否则:上面如果有炮台,可进行升级处理
                    else if(mapcube.turretGo!=null)
                    {                      
                        if (mapcube == selectedMapCube&&upgradeCanvas.activeInHierarchy) {
                            HideUpgradeUI();
                        }
                        else
                        {
                            ShowUpradeUI(mapcube.transform.position, mapcube.isUpgrade);
                        }
                        selectedMapCube = mapcube;                      
                    }
                }
            }
        } 
    }
    //选中某种类型炮台触发下列三个函数
    public void OnLaserSelected(bool isOn)
    {
        if (isOn)
        {
            selectedTurretData = laserTurretData;
        }
    }
    public void OnStandardSelected(bool isOn)
    {
        if (isOn)
        {
            selectedTurretData = standardTurretData;
        }
    }

    void ShowUpradeUI(Vector3 pos, bool isDisableUpgrade=false)
    {
        upgradeCanvas.SetActive(true);
        upgradeCanvas.transform.position = pos;
        buttonUpgrad.interactable = !isDisableUpgrade;
    }
    IEnumerator HideUpgradeUI()
    {
        yield return new WaitForSeconds(0.2f);
        upgradeCanvas.SetActive(false);
    }

    public void OnUpgradeButtonDown()
    {
        if (money >= selectedMapCube.turretData.costUpgraded)
        {
            MoneyChanged(-selectedMapCube.turretData.costUpgraded);
            selectedMapCube.UpgradeTurret();
            StartCoroutine(HideUpgradeUI());
        }
        else
        {
            moneyAnimator.SetTrigger("flicker");
        }
    }
    public void OnDestroyButtonDown()
    {
        MoneyChanged(selectedMapCube.turretData.cost/2);
        selectedMapCube.DestoryTurret();
        StartCoroutine(HideUpgradeUI());
    }
}
