using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEditor;
using UnityEditor.EditorTools;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Rigidbody2D))]
public class AttackBehaviour : MonoBehaviour
{
    private Rigidbody2D _rB;
    [Header("Attack Info")]
    [SerializeField]
    private float _hitForce;

    [SerializeField]
    private float _maxSpeed;

    [SerializeField]
    private float _attackRadius;

    [SerializeField]
    private float _distance = 10;
    [SerializeField]
    private float _attackCooldown = 2;
    private float _attackTimer = 0;

    [Header("Heavy Attack")]
    [SerializeField]
    private float _heavyAttackCooldown = 3;
    [SerializeField]
    private float _forceMultiplyer = 2;
    [SerializeField]
    private float _timeUntillMaxCharged = 2;
    private float _chargeTimer = 0;
    private bool _isCharging = false;



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
    void Update()
    {
        _attackTimer -= Time.deltaTime;
        if (_isCharging)
        {
            _chargeTimer += Time.deltaTime;
        }
    }

    public void Attack(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            Debug.Log("hej");
            _isCharging = true;
            _chargeTimer = 0;
        }
        Debug.Log("We try to attack");
        if (!context.action.inProgress && _attackTimer <= 0)
        {
            float _tempForce = _hitForce;
            if (_chargeTimer > 1){
                _tempForce = _tempForce * _chargeTimer;
            }
            _isCharging = false;
            Debug.Log("We attack");
            _attackTimer = _attackCooldown;
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

                        hits[i].transform.gameObject.GetComponent<PlayerMovement>().DisableMovment();
                        //Add hitting Force
                        Debug.Log(_tempForce + " force we are using");
                        oppRB.AddForce(dir.normalized * _tempForce, ForceMode2D.Impulse);
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
