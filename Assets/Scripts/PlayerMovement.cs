using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    private float _Speed = 5f;
    private Rigidbody2D rb;
    private Vector2 inputVector;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(inputVector * _Speed * Time.deltaTime);
    }
    public void Movement(InputAction.CallbackContext ctx){
        inputVector =  ctx.ReadValue<Vector2>();
    }
}
