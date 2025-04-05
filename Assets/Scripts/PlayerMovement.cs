using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField]
    private float _Speed = 5f;
    private Rigidbody2D rb;
    private Vector2 inputVector;
    public Vector2 facingDir;
    public Animator anim;

    [Header("MovementDisabled")]
    public float disabledMovementDuration;
    private bool _DisabledMovement = false;
    private float _disabledMovementTimer = 0;

    [SerializeField]
    private float _maxSpeed = 100;

    void Start()
    {
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        anim.SetFloat("Horizontal", facingDir.x);
        anim.SetFloat("Vertical", facingDir.y);
        //Debug.Log(rb.velocity.magnitude);

        if (_DisabledMovement)
        {
            _disabledMovementTimer -= Time.deltaTime;
            if (_disabledMovementTimer <= 0)
            {
                ReEnableMovment();
            }
        }
        else
        {
            transform.Translate(inputVector * _Speed * Time.deltaTime);
            anim.SetFloat("Speed", inputVector.magnitude);
        }
    }

    void FixedUpdate()
    {
        // rb.AddForce(inputVector * _Speed * Time.deltaTime);
        //Limit Velocity
        if (rb.velocity.magnitude > _maxSpeed)
        {
            Vector2 newSpeed = rb.velocity.normalized * _maxSpeed;
            rb.velocity = newSpeed;
        }
    }

    public void Movement(InputAction.CallbackContext ctx)
    {
        inputVector = ctx.ReadValue<Vector2>();
        if (ctx.action.inProgress)
        {
            facingDir = inputVector;
        }
    }

    public void DisableMovment()
    {
        _DisabledMovement = true;
        _disabledMovementTimer = disabledMovementDuration;
    }

    private void ReEnableMovment()
    {
        rb.velocity = Vector2.zero;
        _DisabledMovement = false;
    }
}
