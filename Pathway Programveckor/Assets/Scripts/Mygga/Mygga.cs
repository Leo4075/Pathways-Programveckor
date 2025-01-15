using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SideFlight : MonoBehaviour
{
    //R�relserelaterade variabler
    //Antiborder: Ett empty-gameObject med en box collider som fienden patrullerar inom
    Rigidbody2D rb;
    public float patrolSpeed = 2f;
    public float borderCheckRadius = 0.1f;
    public LayerMask antiborderLayer;           
    public Transform borderCheckRight;
    public Transform borderCheckLeft;
    public Transform startPos;

    private bool movingRight = false;   //best�mmer riktning, b�rjar v�nster

    //Spelardetektionsrelaterade variabler
    public Transform player;
    public float detectionRange = 5f;
    public LayerMask playerLayer;

    private bool playerIsDetected = false;

    //Follow()-relaterade variabler
    public float followSpeed = 3f;
    public float stopDistance = 2f;
    private bool dashActivated = false;
    

    //ReturnToStart()-relaterade variabler
    public float posTolerance = 1;  //Tolerans som anv�nds f�r att se om tv� objekt �r p� samma plats
    private bool notAtStart = false;

    //Dash()-relaterade variabler
    public float dashCooldown = 3f;
    public float dashDuration = 2f;
    private float dashCooldownTimer;
    private float dashDurationTimer;
    public float dashSpeed = 10f;
    private bool dashStart = false;
    private Vector2 dashDirection;


    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        Debug.Log("FlyPatrol.cs start");
        dashCooldownTimer = dashCooldown;
        dashDurationTimer = dashDuration;
    }

    // Update is called once per frame
    void Update()
    {
        rb.velocity = new Vector2(0, rb.velocity.y);//Fienden ska stanna om inget annat anges
        dashCooldownTimer -= Time.deltaTime;
        

        PlayerDetect();
        if (playerIsDetected && dashActivated)
        {
            Dash();
        }
        else if (playerIsDetected)
        {
            //Debug.Log("<color=blue>(Update)</color><color=green>Follow()</color>");
            Follow();
        }
        else if (notAtStart)
        {
            //Debug.Log("<color=blue>(Update)</color><color=red>ReturnToStart()</color>");
            ReturnToStart();
        }
        else
        {
            //Debug.Log("<color=blue>(Update)</color><color=yellow>SideMovement()</color>");
            SideMovement();
        }
    }

    void SideMovement()
    {
        if (!Scan(borderCheckRight,borderCheckRadius,antiborderLayer))
        {
            //Debug.Log("Det finns ingen antiborder till h�ger");
            movingRight = false;
        }

        if (!Scan(borderCheckLeft,borderCheckRadius,antiborderLayer))
        {
            //Debug.Log("Det finns ingen antiborder till v�nster");
            movingRight = true;
        }

        int direction = movingRight ? 1 : -1;   //Riktningen best�ms av movingRight-variabeln
        rb.velocity = new Vector2(patrolSpeed * direction, rb.velocity.y);
    }

    void PlayerDetect()
    {
        // Kontrollera avst�ndet till spelaren
        float distanceToPlayer = Vector2.Distance(transform.position, player.position);
        
        if (distanceToPlayer <= detectionRange)
        {
            playerIsDetected = true; // M�rker spelaren
        }
        else if(player)
        {
            playerIsDetected = false; // Tappar spelaren
        }
    }

    void Follow()   //Fienden f�ljer efter spelaren
    {
        // Ber�kna riktningen mot spelaren
        Vector2 followDirection = (player.position - transform.position).normalized;

        // S�tt Rigidbody2D-velocity f�r att flytta fienden
        rb.velocity = followDirection * followSpeed;

        if (dashCooldownTimer <= 0)
        {
            //Debug.Log("<color=red>Dash Activated</color>");
            dashActivated = true;
            dashStart = true;
            dashCooldownTimer = dashCooldown;
        }

        notAtStart = true;
    }

    void Dash()
    {
        if (dashStart)
        {
            //Debug.Log("<color=yellow>Dash Start</color>");
            dashStart = false;
            dashDurationTimer = dashDuration;
            //Ber�kna riktningen innan attacken
            dashDirection = (player.position - transform.position).normalized;
        }
        dashDurationTimer -= Time.deltaTime;
        //Fienden r�r sig i riktningen som blivit ber�knad tidigare
        rb.velocity = dashDirection * dashSpeed;
        if (dashDurationTimer <= 0)
        {
            DashEnd();
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        //Debug.Log("Collision, dashActivaded="+dashActivated);
        if (dashActivated)
        {
            //Debug.Log("Dashed into object");
            DashEnd();
        }
    }

    void DashEnd()
    {
        Debug.Log("Dash end = true");
        dashActivated = false;
        dashStart = true;
        rb.velocity = Vector2.zero; // Stoppa r�relsen
    }

    void ReturnToStart()   //Fienden �terv�nder till startpunkten
    {
        // Ber�kna riktningen mot spelaren
        Vector2 direction = (startPos.position - transform.position).normalized;

        // S�tt Rigidbody2D-velocity f�r att flytta fienden
        rb.velocity = direction * patrolSpeed;

        if (Vector2.Distance(transform.position, startPos.position) < posTolerance)
        {
            Debug.Log("<color=red>(Debug)</color><color=green>Enemy is at start)</color>");
            rb.velocity = new Vector2(0, 0);
            notAtStart = false;
        }
    }

    bool Scan(Transform center,float radius, LayerMask layer)   //En skanningsmetod som kan anropas varifr�n som helst
    {
        return Physics2D.OverlapCircle(center.position,radius,layer);
    }
}
