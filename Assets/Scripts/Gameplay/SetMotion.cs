using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetMotion : MonoBehaviour
{
    public Vector2 velocity;
    public float angularVelocity;
    public RigidbodyConstraints2D constraints;
    public bool isActive = false;

    void FixedUpdate()
    {
        if (isActive)
        {
            Rigidbody2D rb2d = GetComponent<Rigidbody2D>();
            if (rb2d)
            {
                rb2d.constraints = constraints;
                rb2d.velocity = velocity;
                rb2d.angularVelocity = angularVelocity;
            }
            Destroy(this);
        }
    }
}
