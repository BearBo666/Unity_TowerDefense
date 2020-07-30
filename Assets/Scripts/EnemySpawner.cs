using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

//生成敌人的脚本
public class EnemySpawner : MonoBehaviour
{
    //存活敌人的数量，生成的波数，生成点，每波间的间隔
    public static int enemyAlive=0;
    public Wave[] waves;
    public Transform START;
    public float waveRate = 0.3f;
    private Coroutine coroutine;

    void Start()
    {
        coroutine= StartCoroutine(SpawnEnemy());
    }
    public void Stop()
    {
        StopCoroutine(coroutine);
    }
    IEnumerator SpawnEnemy()
    {
        foreach(Wave wave in waves)
        {
            for(int i = 0; i < wave.count; i++)
            {
                GameObject.Instantiate(wave.enemyPrefab, START.position, Quaternion.identity);
                enemyAlive++;
                if(i!=wave.count-1)
                    //一波中每个敌人之间的生成时间间隔
                    yield return new WaitForSeconds(wave.rate);
            }
            //若存活敌人数量大于0，则等死光后再生成
            while (enemyAlive>0)
            {
                yield return 0;
            }
            //再间隔一个生成时间
            yield return new WaitForSeconds(waveRate);
        }
        while (enemyAlive>0)
        {
            yield return 0;
        }
        GameManager.Instance.Win();
    }
}
