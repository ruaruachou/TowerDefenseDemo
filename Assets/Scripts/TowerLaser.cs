using UnityEngine;

public class TowerLaser : Tower
{
    public GameObject linePrefab;
    private LineRenderer lineRenderer;
    

    protected override void Update()
    {
        base.Update();

        closestEnemy = FindClosestEnemyInRange();
        if (IsEnemyInRange())//首要条件：有敌人在范围内
        {
            if (targetEnemy == null || !IsTargetInRange(targetEnemy))//次要条件：当当前目标敌人为空或目标敌人不在范围内
            {
                targetEnemy = FindClosestEnemyInRange();//更新目标：将当前的最近敌人设为目标。注意：这里使用了封装的思维，类似get，set，仅在满足条件时set。
            }
            if (targetEnemy != null)
            {
                Attack(targetEnemy);
            }
        }
        else
        {//当范围内没有敌人时，销毁激光
            if (lineRenderer != null)
            {
                DestroyLine();
            }
        }
    }

    public override void Attack(Enemy enemy)
    {
        base.Attack(enemy);

        float levelATK = data[currentLvl].levelAtk;
        enemy.HP -= (levelATK* Time.deltaTime);
       

        Vector3 targetPos = enemy.transform.position;
        CreateOrUpdateLine(transform.position, targetPos);

    }

    private void CreateOrUpdateLine(Vector3 initPos, Vector3 endPos)
    {
        if (lineRenderer == null)
        {
            CreateLine(initPos, endPos);
        }
        else
        {
            UpdateLine(endPos);
        }
    }

    private void CreateLine(Vector3 initPos, Vector3 endPos)
    {
        GameObject lineObject = Instantiate(linePrefab, transform.position, Quaternion.identity);
        lineRenderer = lineObject.GetComponent<LineRenderer>();
        lineRenderer.positionCount = 2;
        lineRenderer.SetPosition(0, initPos);
        lineRenderer.SetPosition(1, endPos);
    }

    private void UpdateLine(Vector3 endPos)
    {
        lineRenderer.SetPosition(1, endPos);
    }

    private void DestroyLine()
    {
        if (lineRenderer != null)
        {
            Destroy(lineRenderer.gameObject);
            lineRenderer = null;
        }
    }
}
