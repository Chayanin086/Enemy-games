using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    public float speed;
    public float checkradius;
    public float attackradius;

    public bool shouldRotate;

    public LayerMask whatIsPlayer;

    private Transform target;
    private Rigidbody2D rb;
    private Animator Anim;
    private Vector2 movement;
    public Vector3 dir;

    private bool chaseRange;
    private bool attackRange;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        Anim= GetComponent<Animator>();
        target = GameObject.FindWithTag("Player").transform;
    }
    private void Update()
    {
        Anim.SetBool("isRunning",chaseRange);
        chaseRange = Physics2D.OverlapCircle(transform.position, checkradius, whatIsPlayer);
        attackRange = Physics2D.OverlapCircle(transform.position, attackradius, whatIsPlayer);

        dir = target.position - transform.position;
        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        dir.Normalize();
        movement = dir;
        if (shouldRotate) {
            Anim.SetFloat("X", dir.x);
            Anim.SetFloat("Y", dir.y);
        }
    }
    private void FixedUpdate()
    {
        if (chaseRange && !attackRange) {
            MoveEnemy(movement); 
        }
        if (attackRange) {
            rb.velocity = Vector2.zero;
        }
    }
    private void MoveEnemy(Vector2 dir)
    {
        rb.MovePosition((Vector2)transform.position + (dir * speed * Time.deltaTime));
    }
}   
