using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    public static int currentEnemy = 0;
    public Wave[] waves;
    public float waveInterval;

    private void Start()
    {
        StartCoroutine(SpawmEnemy());
    }

    IEnumerator SpawmEnemy()
    {
        
        for (int i = 0; i < waves.Length; i++)
        {
            yield return new WaitForSeconds(waveInterval);
            Wave wave = waves[i]; //������ʵ����Wave��
            for (int j = 0; j < wave.enemyPrefab.Length; j++)
            {
                //Ȼ������ʵ����Wave�е�enemyPrefab����
                GameObject enemyy = Instantiate(wave.enemyPrefab[j], transform.position, Quaternion.identity);
                if(j!=wave.enemyPrefab.Length-1)
                 yield return new WaitForSeconds(wave.spawnInterval);
            }
            
        }
    }
}
