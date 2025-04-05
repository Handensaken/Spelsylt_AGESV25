using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleHitFX : MonoBehaviour
{
    [SerializeField]
    private GameObject particle;

    // Start is called before the first frame update
    void Start()
    {
        GameEventsManager.instance.OnPlayerHit += OnPlayerHit;
    }

    void OnDisable()
    {
        GameEventsManager.instance.OnPlayerHit -= OnPlayerHit;
    }

    // Update is called once per frame
    void Update() { }

    private void OnPlayerHit(Transform targetTransform)
    {
        Instantiate(particle, targetTransform);
    }
}
