using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]

public class PlayerMove : MonoBehaviour
{
    [SerializeField] float speed = 2f;

    public Vector3 movementVector;

    [HideInInspector] public float lastHorizontalDeCoupledVector;
    [HideInInspector] public float lastVerticalDeCoupledVector;

    [HideInInspector] public float lastHorizontalCoupledVector;
    [HideInInspector] public float lastVerticalCoupledVector;

    Rigidbody2D rgb2d;
    Animate animate;
    Character character;
    void Awake()
    {
        rgb2d = GetComponent<Rigidbody2D>();
        animate = GetComponent<Animate>();
        movementVector = new Vector3();
        
    }
    void Start()
    {
        lastHorizontalDeCoupledVector = 1f;
        lastVerticalDeCoupledVector = 1f;

        lastHorizontalCoupledVector = 1f;
        lastVerticalCoupledVector = 0f;

    }

    void Update()
    {

        movementVector.x = Input.GetAxisRaw("Horizontal");
        movementVector.y = Input.GetAxisRaw("Vertical");

        if (movementVector.x != 0 || movementVector.y != 0)
        {
            lastHorizontalCoupledVector = movementVector.x;
            lastVerticalCoupledVector = movementVector.y;
        }

        if (movementVector.x != 0)
        {
            lastHorizontalDeCoupledVector = movementVector.x;
        }
        if(movementVector.y != 0)
        {
            lastVerticalDeCoupledVector = movementVector.y;
        }

        animate.inputVec = movementVector;

        movementVector *= speed;

        rgb2d.velocity = movementVector;
    }

}
