using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitPoint : MonoBehaviour
{

    public bool taken = false;

    private IEnumerator Change()
    {
        taken = true;

        yield return new WaitForSeconds(2.5f);

        taken = false;
    }

    public void ChangeState()
    {
        StartCoroutine(Change());
    }
    

}
