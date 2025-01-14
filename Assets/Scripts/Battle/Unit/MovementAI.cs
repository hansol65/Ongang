using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementAI : MonoBehaviour
{
    public Transform target;
    public float moveSpeed = 2f;
    public float stopDistance = 1.5f;

    private void Update()
    {
        if (target == null) return;
        float distance = Vector2.Distance(transform.position, target.position);

        if(distance > stopDistance )
        {
            Vector2 direcion = (target.position - transform.position).normalized;
            transform.position += (Vector3)direcion * moveSpeed * Time.deltaTime;
        }
    }
}
