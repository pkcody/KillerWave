using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicEnemyRoatate : MonoBehaviour
{
    [SerializeField]
    float speed = 0;
    void Update()
    {
        transform.Rotate(Vector3.left * Time.deltaTime * speed);
    }
}
