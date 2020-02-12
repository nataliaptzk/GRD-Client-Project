using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChainComponent : MonoBehaviour
{
    // Start is called before the first frame update
    private Rigidbody2D _rb2;

    void Start()
    {
        _rb2 = GetComponent<Rigidbody2D>();
        _rb2.centerOfMass = new Vector2(0,0);
        _rb2.inertia = 1.0f;
    }

    // Update is called once per frame
}