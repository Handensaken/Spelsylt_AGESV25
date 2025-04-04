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

        if (_DisabledMovement) return;
        transform.Translate(inputVector * _Speed * Time.deltaTime);
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
    public void DisableMovment(){
        _DisabledMovement = true;
        Invoke(nameof(ReEnableMovment), disabledMovementDuration);
    }
    private void ReEnableMovment(){
        rb.velocity = Vector2.zero;
        _DisabledMovement = false;
    }
}
