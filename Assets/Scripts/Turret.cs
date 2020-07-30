using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//控制炮台攻击敌人的脚本
public class Turret : MonoBehaviour
{
    //进入攻击范围内的敌人列表
    public List<GameObject> enemys=new List<GameObject>();
    void OnTriggerEnter(Collider col)
    {
        if (col.tag == "Enemy")
        {
            enemys.Add(col.gameObject);
        }
    }
    private void OnTriggerExit(Collider col)
    {
        if (col.tag == "Enemy")
        {
            enemys.Remove(col.gameObject);
        }
    }

    public float attckRateTime = 1; //多少秒攻击一次
    private float timer ; //计时器
    public GameObject bulletPrefabs; //子弹
    public Transform firePosition; //子弹发出的位置
    public Transform head; //炮台头部的位置

    void Start()
    {
        //做好攻击准备
        timer = attckRateTime;
    }
    void Update()
    {
        timer += Time.deltaTime;
        if (enemys.Count>0&&timer >=attckRateTime)
        {
            timer = 0;
            Attack();
        }
        if (enemys.Count > 0 && enemys[0] != null)
        {
            //记录第一个敌人的位置,使炮台头部望向它
            Vector3 targetPosition = enemys[0].transform.position;
            targetPosition.y = head.position.y;
            head.LookAt(targetPosition);
        }
    }
    void Attack()
    {
        if (enemys[0] == null)
        {
            UpdateEnemys();
        }
        if (enemys.Count > 0)
        {
            GameObject bullet = Instantiate(bulletPrefabs, firePosition.position, firePosition.rotation);
            bullet.GetComponent<Bullet>().SetTarget(enemys[0].transform);
        }
        else
        {
            timer = attckRateTime;
        }
    }

    //更新敌人列表的函数
    void UpdateEnemys()
    {
        List<int> emptyIndex = new List<int>();
        //记录死亡或者走出攻击范围的敌人的下标
        for(int index = 0; index < enemys.Count; index++)
        {
            if (enemys[index] == null)
            {
                emptyIndex.Add(index);
            }
        }
        for(int i=0;i< emptyIndex.Count; i++)
        {
            //由于emptyIndex是从列表的第一个开始遍历,删除后数组中的元素都会向前行走一格,故减去一个i
            enemys.RemoveAt(emptyIndex[i]-i);
        }
    }
}
