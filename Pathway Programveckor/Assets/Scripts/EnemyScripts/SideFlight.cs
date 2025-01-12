using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SideFlight : MonoBehaviour
{
    Rigidbody2D rb;
    public float speed = 2f;
    public float checkRadius = 0.1f;
    public LayerMask antiborderLayer;
    public Transform checkRight;
    public Transform checkLeft;

    private bool movingRight = true;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        Debug.Log("SideFlight.cs start");
    }

    // Update is called once per frame
    void Update()
    {
        SideMovement();
    }

    void SideMovement()
    {
        if (!IsAntiborderAtPoint(checkRight))
        {
            Debug.Log("Det finns ingen antiborder till höger");
            movingRight = false;
        }

        if (!IsAntiborderAtPoint(checkLeft))
        {
            Debug.Log("Det finns ingen antiborder till vänster");
            movingRight = true;
        }

        int direction = movingRight ? 1 : -1;
        rb.velocity = new Vector2(speed * direction, rb.velocity.y);
    }
    bool IsAntiborderAtPoint(Transform checkPoint)
    {
        return Physics2D.OverlapCircle(checkPoint.position, checkRadius, antiborderLayer);
    }

}
