using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEditor;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Rigidbody2D))]
public class AttackBehaviour : MonoBehaviour
{
    private Rigidbody2D _rB;

    [SerializeField]
    private float _hitForce;

    [SerializeField]
    private float _maxSpeed;

    [SerializeField]
    private float _attackRadius;

    [SerializeField]
    private float _distance = 10;

    Vector2 dir;

    // Start is called before the first frame update
    void Start()
    {
        _rB = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
      
    }

    public void Attack(InputAction.CallbackContext context)
    {
        if (!context.action.inProgress)
        {
            dir = GetComponent<PlayerMovement>().facingDir;
            RaycastHit2D[] hits = Physics2D.CircleCastAll(
                transform.position,
                _attackRadius,
                dir,
                _distance
            );
            for (int i = 0; i < hits.Length; i++)
            {
                if (hits[i].transform.gameObject != gameObject)
                {
                    if (hits[i].transform.CompareTag("Player"))
                    {
                        //Find the direction vector from player A to B
                        Vector2 dir = hits[i].transform.position - transform.position;

                        //Get opponent Rigidbody
                        Rigidbody2D oppRB = hits[i]
                            .transform.gameObject.GetComponent<Rigidbody2D>();
                        //Add hitting Force
                        oppRB.AddForce(dir.normalized * _hitForce, ForceMode2D.Impulse);
                    }
                }
            }
        }
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;

        Gizmos.DrawSphere(transform.position + (Vector3)dir * _distance, _attackRadius);
    }
}
