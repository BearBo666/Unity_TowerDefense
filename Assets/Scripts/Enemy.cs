using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//这是控制敌人的脚本
public class Enemy : MonoBehaviour
{
    //速度和标记点数组及其下标
    public float speed = 10;
    private Transform[] positions;
    [SerializeField]
    private int index = 0;
    //当前生命,总的生命,以及血条和死亡时的粒子特效
    public float hp = 150;
    private float totalHp;
    public Slider hpSlider;
    public GameObject dieEffect;
    // Start is called before the first frame update
    void Start()
    {
        totalHp = hp;
        positions = WayPoints.positions;
    }

    // Update is called once per frame
    void Update()
    {
        Move();
    }

    void Move()
    {
        //前往下一个标记点
        transform.Translate((positions[index].position - transform.position).normalized * Time.deltaTime * speed);
        //如果与标记点之间的距离小于0.2咋判定为到达
        if (Vector3.Distance(positions[index].position, transform.position) < 0.2)
        {
            index++;
        }
        //如果是最后一个标记点则是到达终点
        if (index > positions.Length - 1)
        {
            ReachDestination();
        }
    }

    //到达终点的函数
    void ReachDestination()
    {
        Destroy(gameObject);
        GameManager.Instance.Faild();
    }

    //到达终点触发此函数
    void OnDestroy()
    {
        EnemySpawner.enemyAlive--;
    }
    //受到伤害触发此函数
    public void TakeDamage(float damage) {
        if (hp <= 0) return;
        hp -= damage;
        hpSlider.value = (float)hp / totalHp;
        if (hp <= 0)
        {
            Die();
        }
    }

    //受伤死亡触发此函数
    void Die()
    {
        GameObject effect = GameObject.Instantiate(dieEffect, transform.position, transform.rotation);
        Destroy(effect, 1.5f);
        Destroy(this.gameObject);
    }
}
