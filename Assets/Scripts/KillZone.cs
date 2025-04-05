using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KillZone : MonoBehaviour
{
    bool _canKillPlayer = true;
    void Start()
    {
        GameEventsManager.instance.OnWeRespawnPlayer += CanKillPlayer;
    }
    void OnDisable()
    {
        GameEventsManager.instance.OnWeRespawnPlayer -= CanKillPlayer;
    }
    void CanKillPlayer()
    {
        _canKillPlayer = true;
    }
    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.CompareTag("Player") && _canKillPlayer)
        {
            GameEventsManager.instance.PlayerDeath(col.gameObject);
            col.gameObject.SetActive(false);
            _canKillPlayer = false;
        }
    }
}
