using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField]
    private float _Speed = 5f;
    [SerializeField]
    private float _minVelocityReset = 2f;
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
    [Header("Find Wall")]
    [SerializeField] private LayerMask _wallLayerMask;

    void Start()
    {
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
    }
    void OnEnable()
    {
        _disabledMovementTimer = 0;
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
            MovePlayer();
            anim.SetFloat("Speed", inputVector.magnitude);
            if (rb.velocity.magnitude <= _minVelocityReset)
            {
                rb.velocity = Vector2.zero;
            }
        }
    }

    private void MovePlayer()
    {
        float radiusX = GetComponent<CircleCollider2D>().radius * transform.localScale.x;
        float radiusY = GetComponent<CircleCollider2D>().radius * transform.localScale.y;

        float moveDistance = _Speed * 1.1f * Time.deltaTime;

        Vector2 moveX = new Vector2(inputVector.x, 0);
        Vector2 moveY = new Vector2(0, inputVector.y);

        if (moveX != Vector2.zero)
        {
            RaycastHit2D hitX = Physics2D.Raycast(transform.position, moveX.normalized, radiusX + moveDistance, _wallLayerMask);
            if (!hitX)
            {
                transform.Translate(moveX * moveDistance);
            }
            else
            {
                float distance = hitX.distance - radiusX;
                transform.Translate(moveX.normalized * distance);
            }
        }

        if (moveY != Vector2.zero)
        {
            RaycastHit2D hitY = Physics2D.Raycast(transform.position, moveY.normalized, radiusY + moveDistance, _wallLayerMask);
            if (hitY)
            {

                Debug.Log(hitY.transform.gameObject.name);

            }
            if (!hitY)
            {
                transform.Translate(moveY * moveDistance);
            }
            else
            {
                float distance = hitY.distance - radiusY;
                transform.Translate(moveY.normalized * distance);
            }
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
