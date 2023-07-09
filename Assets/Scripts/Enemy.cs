using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Enemy : MonoBehaviour
{
    [SerializeField] private int moveSpeed = 3;
    [SerializeField] private float enemyHP;
    public float HP
    {
        get
        {
            return enemyHP;
        }
        set
        {
            if (value <= 0) enemyHP = 0;
            else enemyHP = value;
        }
    }
    public int enemyATK;
    List<Transform> pathPoints;
    [SerializeField] int pathIndex = 0;

    protected virtual void Start()
    {
        GetPathPoints();
    }

    protected virtual void Update()
    {
        MoveByPath(pathPoints);
        EnemyDie();
    }
    public virtual void Move()
    {

    }
    public virtual void EnemyDie()
    {
        if (HP <= 0)
        {
            Destroy(gameObject);
        }
    }
    void GetPathPoints()
    {
        //步骤：实例化>获取父物体>获取子物体数量>依次填充
        pathPoints = new List<Transform>();
        Transform path = GameObject.Find("Path").transform;
        int pathCount = path.childCount;
        for (int i = 0; i < pathCount; i++)
        {
            Transform child = path.GetChild(i);
            pathPoints.Add(child);
        }
    }
    void MoveByPath(List<Transform> pathPoints)
    {

        if (pathIndex >= pathPoints.Count) { return; }//越界则退出

        Vector3 dir = pathPoints[pathIndex].position - transform.position;
        float distance = Vector3.Distance(pathPoints[pathIndex].position, transform.position);

        transform.Translate(dir.normalized * moveSpeed * Time.deltaTime, Space.World);

        if (distance <= 0.1f)
        {
            pathIndex++;
        }
    }
    void AttackTaret()
    {
        GameManager.life -= enemyATK;
        Debug.Log(GameManager.life);
    }
    protected virtual void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Target"))
        {
            Debug.Log("Targe is attacked");
            AttackTaret();
        }
    }
}
