using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDetection : MonoBehaviour
{
    public Transform detectionZoneCenter;
    public float detectonZoneRadius = 0.1f;
    public LayerMask playerLayer;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        bool playerDetected = PlayerDetected();
        if (playerDetected == true)
        {
            Debug.Log("Player detected");
        }
    }
    
    bool PlayerDetected()
    {
        return Physics2D.OverlapCircle(detectionZoneCenter.position, detectonZoneRadius, playerLayer);
    }
}
