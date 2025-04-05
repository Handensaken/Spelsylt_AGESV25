using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using TMPro;
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
    private float _maxForceMultiplyer = 2;

    [SerializeField]
    [Tooltip("Hur mycket ska multiplyern öka pär tid, (time * _forcebyTime) = force applyed")]
    private float _forceByTime = 2;

    [SerializeField]
    [Tooltip(
        "Tiden det tar att göra den charge attack, borde inte _timeUntillCharged * _forceByTime < 1"
    )]
    private float _timeUntillCharged = 1;
    private float _chargeTimer = 0;
    private bool _isCharging = false;

    Vector2 dir;

    // Start is called before the first frame update
    void Start()
    {
        _rB = GetComponent<Rigidbody2D>();
    }

    private bool shouldCast = false;

    void Update()
    {
        _attackTimer -= Time.deltaTime;
        if (_isCharging)
        {
            _chargeTimer += Time.deltaTime;
        }

        if (shouldCast)
        {
            MakeCircleCast();
        }
    }

    public void castShit(int i)
    {
        if (i == 1)
            shouldCast = true;
        else
            shouldCast = false;
        //shouldCast = castState;
    }

    private bool BABYCOR = false;

    public void Attack(InputAction.CallbackContext context)
    {
        if (_attackTimer >= 0)
            return;
        if (context.started)
        {
            _isCharging = true;
            _chargeTimer = 0;
            GetComponent<PlayerMovement>().anim.SetTrigger("AttackInit");
            BABYCOR = false;
        }
        if (!context.action.inProgress && _attackTimer <= 0)
        {
            _attackTimer = _attackCooldown;

            /*  float _tempForce = _hitForce;
              if (_chargeTimer >= _timeUntillCharged)
              {
                  _attackTimer = _heavyAttackCooldown;
                  _tempForce = _forceByTime * _tempForce * _chargeTimer;
                  if (_tempForce > _maxForceMultiplyer * _hitForce)
                  {
                      _tempForce = _maxForceMultiplyer * _hitForce;
                  }
              }*/
            _isCharging = false;
            GetComponent<PlayerMovement>().anim.SetTrigger("AttackRelease");

            dir = GetComponent<PlayerMovement>().facingDir;

            /*  RaycastHit2D[] hits = Physics2D.CircleCastAll(
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
  
                          PlayerMovement pM = hits[i]
                              .transform.gameObject.GetComponent<PlayerMovement>();
                          pM.DisableMovment();
                          //Add hitting Force
                          Debug.Log(_tempForce + " force we are using");
                          oppRB.AddForce(dir.normalized * _tempForce, ForceMode2D.Impulse);
                          GameEventsManager.instance.PlayerHit(hits[i].transform);
                          GameEventsManager.instance.CameraShake();
                      }
                  }
              }*/
        }
    }

    [SerializeField]
    private Transform _swordReferencePoint;

    private void MakeCircleCast()
    {
        float _tempForce = _hitForce;
        if (_chargeTimer >= _timeUntillCharged)
        {
            _attackTimer = _heavyAttackCooldown;
            _tempForce = _forceByTime * _tempForce * _chargeTimer;
            if (_tempForce > _maxForceMultiplyer * _hitForce)
            {
                _tempForce = _maxForceMultiplyer * _hitForce;
            }
        }
        if (BABYCOR)
            return;
        RaycastHit2D[] hits = Physics2D.CircleCastAll(
            _swordReferencePoint.position,
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
                    Rigidbody2D oppRB = hits[i].transform.gameObject.GetComponent<Rigidbody2D>();

                    PlayerMovement pM = hits[i].transform.gameObject.GetComponent<PlayerMovement>();
                    pM.DisableMovment();
                    //Add hitting Force
                    Debug.Log(_tempForce + " force we are using");
                    oppRB.AddForce(dir.normalized * _tempForce, ForceMode2D.Impulse);
                    GameEventsManager.instance.PlayerHit(hits[i].transform);
                    GameEventsManager.instance.CameraShake();
                    BABYCOR = true;
                }
            }
        }
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;

        Gizmos.DrawSphere(_swordReferencePoint.position, _attackRadius);
    }
}
