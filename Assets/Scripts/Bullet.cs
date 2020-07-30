using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//控制子弹的脚本
public class Bullet : MonoBehaviour
{
    //子弹的伤害,速度和爆炸效果
    public float damage = 50;
    public float speed = 20;
    public GameObject explosionEffectPrefabs;

    //距离目标多远时视为命中
    private float distanceArriveTarget = 1.2f;

    private Transform target;
    public void SetTarget(Transform _target)
    {
        target = _target;
    }

    // Update is called once per frame
    void Update()
    {
        //目标死亡，子弹自行销毁
        if (target == null)
        {
            Die();
            return;
        }
        transform.LookAt(target);
        transform.Translate(Vector3.forward * speed * Time.deltaTime);

        //子弹与目标间的距离
        Vector3 dir = target.position - transform.position;
        if (dir.magnitude < distanceArriveTarget)
        {
            target.GetComponent<Enemy>().TakeDamage(damage);
            GameObject effect = GameObject.Instantiate(explosionEffectPrefabs, transform.position, transform.rotation);
            Destroy(effect, 1);
            Destroy(this.gameObject);
        }
    }

    void Die()
    {
        GameObject effect = GameObject.Instantiate(explosionEffectPrefabs, transform.position, transform.rotation);
        Destroy(effect, 1);
        Destroy(this.gameObject);
    }
}
