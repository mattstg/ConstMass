using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetMotion : MonoBehaviour
{
    public Vector2 velocity;
    public float angularVelocity;
    public RigidbodyConstraints2D constraints;
    public bool isActive = false;
    public bool setVelocity = true;
    public bool setAngularVelocity = true;
    public bool setConstraints = true;

    void FixedUpdate()
    {
        if (isActive)
        {
            Rigidbody2D rb2d = GetComponent<Rigidbody2D>();
            if (rb2d)
            {
                if (setConstraints)
                    rb2d.constraints = constraints;
                if (setVelocity)
                    rb2d.velocity = velocity;
                if (setAngularVelocity)
                    rb2d.angularVelocity = angularVelocity;
            }
            Destroy(this);
        }
    }
}
