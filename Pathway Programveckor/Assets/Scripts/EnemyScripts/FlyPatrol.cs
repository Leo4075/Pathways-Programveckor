using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SideFlight : MonoBehaviour
{
    //R�relserelaterade variabler
    //Antiborder: Ett empty-gameObject med en box collider som fienden patrullerar inom
    Rigidbody2D rb;
    public float moveSpeed = 2f;
    public float borderCheckRadius = 0.1f;
    public LayerMask antiborderLayer;           
    public Transform borderCheckRight;
    public Transform borderCheckLeft;

    private bool movingRight = false;   //best�mmer riktning, b�rjar v�nster

    //Spelardetektionsrelaterade variabler
    public Transform detectionZoneCenter;
    public float detectionZoneRadius = 0.1f;
    public LayerMask playerLayer;

    private bool playerIsDetected = false;

    //Follow()-relaterade variabler
    public Transform player;
    public float followspeed = 3f;
    public float stopdistance = 2f;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        Debug.Log("FlyPatrol.cs start");
    }

    // Update is called once per frame
    void Update()
    {
        rb.velocity = new Vector2(0, rb.velocity.y);//Fienden ska stanna om inget annat anges

        PlayerDetect();
        if (playerIsDetected)
        {
            Debug.Log("<color=green>PlayerIsDetected = true</color>");
            Follow();
        }
        else
        {
            Debug.Log("<color=red>PlayerIsDetected = false</color>");
            SideMovement();
        }
    }

    void SideMovement()
    {
        if (!Scan(borderCheckRight,borderCheckRadius,antiborderLayer))
        {
            Debug.Log("Det finns ingen antiborder till h�ger");
            movingRight = false;
        }

        if (!Scan(borderCheckLeft,borderCheckRadius,antiborderLayer))
        {
            Debug.Log("Det finns ingen antiborder till v�nster");
            movingRight = true;
        }

        int direction = movingRight ? 1 : -1;   //Riktningen best�ms av movingRight-variabeln
        rb.velocity = new Vector2(moveSpeed * direction, rb.velocity.y);
    }

    void PlayerDetect()
    {
        bool playerDetected = Scan(detectionZoneCenter,detectionZoneRadius,playerLayer);
        playerIsDetected = playerDetected;
    }

    void Follow()   //Fienden f�ljer efter spelaren
    {

    }

    bool Scan(Transform center,float radius, LayerMask layer)   //En skanningsmetod som kan anropas varifr�n som helst
    {
        return Physics2D.OverlapCircle(center.position,radius,layer);
    }
   

}
