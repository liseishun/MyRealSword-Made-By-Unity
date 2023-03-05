using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class slime : Enemytest
{
    public Transform target;
    public float chaseRadius;
    public float attackRadius;
    public Transform homePosition;
    // Start is called before the first frame update
    void Start()
    {
        target = GameObject.FindWithTag("Player").transform;
    }

    // Update is called once per frame
    void Update()
    {
        CheckDistance();
    }
    void CheckDistance()
    {
        if (Vector2.Distance(target.position, transform.position) <= chaseRadius && Vector2.Distance(target.position, transform.position) > attackRadius)
        {
            Vector2 targetPosition = new Vector2(target.position.x, transform.position.y);
            transform.position = Vector2.MoveTowards(transform.position, targetPosition, moveSpeed * Time.deltaTime);
        }
    }
}
