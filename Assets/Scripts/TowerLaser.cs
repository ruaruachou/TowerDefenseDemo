using UnityEngine;

public class TowerLaser : Tower
{
    public GameObject linePrefab;
    private LineRenderer lineRenderer;
    

    protected override void Update()
    {
        base.Update();

        closestEnemy = FindClosestEnemyInRange();
        if (IsEnemyInRange())//��Ҫ�������е����ڷ�Χ��
        {
            if (targetEnemy == null || !IsTargetInRange(targetEnemy))//��Ҫ����������ǰĿ�����Ϊ�ջ�Ŀ����˲��ڷ�Χ��
            {
                targetEnemy = FindClosestEnemyInRange();//����Ŀ�꣺����ǰ�����������ΪĿ�ꡣע�⣺����ʹ���˷�װ��˼ά������get��set��������������ʱset��
            }
            if (targetEnemy != null)
            {
                Attack(targetEnemy);
            }
        }
        else
        {//����Χ��û�е���ʱ�����ټ���
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
