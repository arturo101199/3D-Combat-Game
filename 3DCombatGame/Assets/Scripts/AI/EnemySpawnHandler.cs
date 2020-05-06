using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawnHandler : MonoBehaviour
{

    public GameObject[] enemiesToDisable;
    public GameObject[] enemiesToEnable;
    public int zoneToGo;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            switch (zoneToGo)
            {
                case 1:
                    Debug.Log("Display Enemies Zone 1");
                    break;
                case 2:
                    Debug.Log("Display Enemies Zone 2");
                    break;
                default:
                    break;
            }
        }
    }

}
