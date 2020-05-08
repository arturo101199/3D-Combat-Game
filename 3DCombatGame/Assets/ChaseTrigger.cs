using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChaseTrigger : MonoBehaviour
{
    bool isCreated;
    public int ChaseAreaSize = 10;
    public TransformValue playerPos;
    Vector3 myTarget;
    public List<GameObject> enemies = new List<GameObject>();

    private void Update()
    {
        if (isCreated)
        {
            if(Vector3.Distance(playerPos.GetValue().position, myTarget) > ChaseAreaSize)
            {
                DesactivateChaseArea();
            }
        }
    }

    public void createChaseArea(Transform target)
    {
        Collider[] chaseArea = Physics.OverlapSphere(target.position, ChaseAreaSize);
        foreach (Collider col in chaseArea)
        {
            InsertInList(col);
        }
        isCreated = true;
    }

    public bool InChaseArea(Vector3 pos)
    {
        if (isCreated)
        {
            float distance = Vector3.Distance(pos, myTarget);
            if(distance <= ChaseAreaSize)
            {
                return true;
            }
            return false;
        }
        return false;
    }

    public void InsertInList(Collider col)
    {
        if (col.CompareTag("EnemyUndead"))
        {
            UndeadController undead = col.GetComponent<UndeadController>();
            undead.chasing = true;
            enemies.Add(col.gameObject);
        }
        else if (col.CompareTag("EnemyRange"))
        {
            ShooterController range = col.GetComponent<ShooterController>();
            range.chasing = true;
            enemies.Add(col.gameObject);
        }
    }

    void DesactivateChaseArea()
    {
        foreach (GameObject GO in enemies)
        {
            if (GO.CompareTag("EnemyUndead"))
            {
                UndeadController undead = GO.GetComponent<UndeadController>();
                undead.chasing = false;
            }
            else if (GO.CompareTag("EnemyRange"))
            {
                ShooterController range = GO.GetComponent<ShooterController>();
                range.chasing = false;
            }
        }
        enemies.Clear();
        isCreated = false;
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(myTarget, ChaseAreaSize);
    }
}
