using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameEventsManager : MonoBehaviour
{
    public static GameEventsManager instance { get; private set; }

    public void Awake()
    {
        instance = this;
    }

    public event Action<GameObject> OnPlayerDeath;

    public void PlayerDeath(GameObject player)
    {
        if (OnPlayerDeath != null)
        {
            OnPlayerDeath(player);
        }
    }

    public event Action OnCameraShake;

    public void CameraShake()
    {
        if (OnCameraShake != null)
        {
            OnCameraShake();
        }
    }

    public event Action<Transform> OnPlayerHit;

    public void PlayerHit(Transform other)
    {
        if (OnPlayerHit != null)
        {
            OnPlayerHit(other);
        }
    }
}
