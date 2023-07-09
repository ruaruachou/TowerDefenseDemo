using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Tower : MonoBehaviour
{
    protected Enemy closestEnemy = null;
    protected Enemy targetEnemy = null;
    public TowerLevelData[] data;
    public int currentLvl;
    public int buildCost;


    protected virtual void Start()
    {
    }

    protected virtual void Update()
    {

    }
    public virtual void Attack(Enemy enemy)
    {

    }
    protected bool IsEnemyInRange()
    {
        float distance = Mathf.Infinity;
        if (FindClosestEnemyInRange() != null)
        {
            Enemy enemy1 = FindClosestEnemyInRange();
            distance = Vector3.Distance(transform.position, enemy1.transform.position);
        }
        return distance <= data[currentLvl].levelRange;
    }
    //找到范围内最近敌人的方法
    protected Enemy FindClosestEnemyInRange()
    {
        Enemy[] enemies = FindObjectsOfType<Enemy>();

        //初始化最近敌人和最近敌人的距离
        Enemy closest = null;
        float closestDis = Mathf.Infinity;

        foreach (Enemy enemy in enemies)
        {
            float distance = Vector3.Distance(transform.position, enemy.transform.position);
            if (distance <= data[currentLvl].levelRange && distance <= closestDis)
            {
                closest = enemy;
                closestDis = distance;
            }
        }
        return closest;
    }

    protected Enemy SetLockedEnemy(Enemy enemy)
    {
        Enemy enemy2 = null;
        if (targetEnemy == null && IsEnemyInRange())
        {
            enemy2 = FindClosestEnemyInRange();
        }
        return enemy2;
    }
    protected bool IsTargetInRange(Enemy enemy)
    {
        
            if (enemy != null)
            {
                float distance = Vector3.Distance(transform.position, enemy.transform.position);
                return distance <= data[currentLvl].levelRange;
            }
            return false;
        
    }
   
}

