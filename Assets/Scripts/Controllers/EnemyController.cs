using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public void Damaged()
    {
        Debug.Log("Udaren");

        Destroy(gameObject);
    }
}
