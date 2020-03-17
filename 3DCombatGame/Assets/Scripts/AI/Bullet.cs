using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour, IPooledObject
{
    public void OnObjectSpawn()
    {
        
    }

    void Disable()
    {
        gameObject.SetActive(false);
    }

    /*
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Disable();
        }
    }
    */

    public void Desintegrate()
    {
        Disable();
        //Activar particulas de desintegracion
    }

}
