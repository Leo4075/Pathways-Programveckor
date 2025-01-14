using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunDirection : MonoBehaviour
{
    public Vector2 Pointerposition { get; set; }

    private void Update()
    {
        transform.right = (Pointerposition - (Vector2)transform.position).normalized;
    }
}
