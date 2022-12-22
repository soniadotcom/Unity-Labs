using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIPatrol : MonoBehaviour
{
    [SerializeField] float walkSpeed;
    [SerializeField] Behaviour behaviour;
    enum Behaviour
    {
        Walk,
        Unpredictable
    }
    public bool mustPatrol;
    bool mustTurn;

    public Rigidbody2D rb;
    public LayerMask groundLayer;
    public Transform groundCheckPos;
    public Collider2D bodyCollider;


    // Start is called before the first frame update
    void Start()
    {
        mustPatrol = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (mustPatrol)
        {
            Patrol();
        }
    }

    private void FixedUpdate()
    {
        if(mustPatrol)
        {
            mustTurn = !Physics2D.OverlapCircle(groundCheckPos.position, 0.1f, groundLayer);
        }
    }

    void Patrol()
    {
        if (mustTurn || bodyCollider.IsTouchingLayers(groundLayer))
        {
            switch(behaviour)
            {
                case Behaviour.Unpredictable:
                    StartCoroutine(UnpredictableFlip());
                    break;
                case Behaviour.Walk:
                    StartCoroutine(Flip());
                    break;
            }
            
        }

        rb.velocity = new Vector2(walkSpeed * Time.fixedDeltaTime, rb.velocity.y);
    }

    IEnumerator Flip()
    {
        mustPatrol = false;
        transform.localScale = new Vector2(transform.localScale.x * -1, transform.localScale.y);
        walkSpeed *= -1;
        yield return new WaitForSeconds(0.1f);
        mustPatrol = true;
        yield return new WaitForSeconds(1f);
    }

    IEnumerator UnpredictableFlip()
    {
        mustPatrol = false;
        transform.localScale = new Vector2(transform.localScale.x * -1, transform.localScale.y);
        walkSpeed *= -1;
        yield return new WaitForSeconds(0.5f);
        mustPatrol = true;
        
    }
}
