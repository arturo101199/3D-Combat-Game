using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChaseTrigger : MonoBehaviour
{

    public List<GameObject> enemies = new List<GameObject>();

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("EnemyUndead"))
        {
            UndeadController undead = other.GetComponent<UndeadController>();
            undead.chasing = true;
            enemies.Add(other.gameObject);
        }
        else if (other.CompareTag("EnemyRange")) //Mirar por qué esto no funciona
        {
            ShooterController range = other.GetComponent<ShooterController>();
            range.chasing = true;
            enemies.Add(other.gameObject);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            foreach(GameObject enemy in enemies)
            {
                if (enemy.CompareTag("EnemyUndead"))
                {
                    UndeadController undead = enemy.GetComponent<UndeadController>();
                    undead.chasing = false;
                }
                else if (enemy.CompareTag("EnemyRange"))
                {
                    ShooterController range = other.GetComponent<ShooterController>();
                    range.chasing = false;
                }
            }
            enemies.Clear();
            Invoke("DestroyTrigger", 0.5f);
        }
    }

    void DestroyTrigger()
    {
        Destroy(this.gameObject);
    }
}
