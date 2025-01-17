using System.Collections;
using System.Collections.Generic;
using UnityEditor.Tilemaps;
using UnityEngine;

public class GoombaWalk : MonoBehaviour
{
    Rigidbody2D rb;
    public float speed = 2f;
    public float checkRadius = 0.1f;
    public LayerMask groundLayer;
    public Transform checkRight;
    public Transform checkLeft;
    public Transform GroundCheck;
    private bool isGrounded;
    private int direction;
    
    private bool movingRight = true;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        SideWalk();
    }

    void SideWalk()
    {
        if (!IsGroundAtPoint(checkRight))
        {
            movingRight = false;
        }

        if (!IsGroundAtPoint(checkLeft))
        {
            movingRight = true;
        }

        if(!IsGroundAtPoint(GroundCheck))
        {
            
        }
        else { isGrounded = true; }

        if (isGrounded)
        {
            direction = movingRight ? 1 : -1;
            rb.velocity = new Vector2(speed * direction, rb.velocity.y);
        }
    }
    bool IsGroundAtPoint(Transform checkPoint)
    {
        return Physics2D.OverlapCircle(checkPoint.position, checkRadius, groundLayer);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        movingRight = !movingRight;
    }
}
