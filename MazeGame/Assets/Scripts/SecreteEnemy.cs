using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SecreteEnemy : MonoBehaviour
{

    Rigidbody2D rb;
    public Transform player;
    float moveScale = 5.0f;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        rb.velocity = (player.position - transform.position).normalized * moveScale;
    }
}
