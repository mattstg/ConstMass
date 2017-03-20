using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomSpin : MonoBehaviour
{
    void FixedUpdate()
    {
        AddRandomAngularVelocity();
        Destroy(this);
    }

    public void AddRandomAngularVelocity()
    {
        Rigidbody2D rb2d = GetComponent<Rigidbody2D>();
        float angularVelocity = Random.Range(-300f, 300f);
        rb2d.angularVelocity += angularVelocity;
    }
}
