using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Boss : MonoBehaviour
{
    public int wave;
    public GameObject prefab;
    public abstract void Attack();
    public abstract void Move();
    public abstract void Die();
}
