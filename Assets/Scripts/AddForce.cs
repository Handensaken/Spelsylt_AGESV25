using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Rigidbody2D))]
public class AddForce : MonoBehaviour
{
    private Rigidbody2D rB;

    [SerializeField]
    private float _force;

    [SerializeField]
    private float _maxSpeed;

    // Start is called before the first frame update
    void Start()
    {
        rB = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void LateUpdate()
    {
        //  Debug.Log(rB.velocity.sqrMagnitude);
        //Limit Velocity
        // Debug.Log(rB.velocity.sqrMagnitude);
        if (rB.velocity.magnitude > _maxSpeed)
        {
            Vector2 newSpeed = rB.velocity.normalized * _maxSpeed;
            rB.velocity = newSpeed;
        }
    }

    [SerializeField]
    private Vector2 _forceDirection;

    public void DebugForce()
    {
        Debug.Log("Hello?");

        rB.AddForce(_forceDirection.normalized * _force, ForceMode2D.Impulse);
    }
}
