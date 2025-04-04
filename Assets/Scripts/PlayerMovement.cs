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

    [SerializeField]
    private float _maxSpeed = 100;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
<<<<<<< Updated upstream
    void Update()
    {
        transform.Translate(inputVector * _Speed * Time.deltaTime);
=======
    void Update() { }

    void FixedUpdate()
    {
        rb.AddForce(inputVector * _Speed * Time.deltaTime);
        //Limit Velocity
        if (rb.velocity.magnitude > _maxSpeed)
        {
            Vector2 newSpeed = rb.velocity.normalized * _maxSpeed;
            rb.velocity = newSpeed;
        }
>>>>>>> Stashed changes
    }

    public void Movement(InputAction.CallbackContext ctx)
    {
        inputVector = ctx.ReadValue<Vector2>();
        if (ctx.action.inProgress)
        {
            facingDir = inputVector;
        }
    }
}
