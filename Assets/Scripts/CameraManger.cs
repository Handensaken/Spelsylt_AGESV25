using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManger : MonoBehaviour
{
    [SerializeField]  Animator _cameraAnimator;
    [SerializeField] private float _shakeDuration = 1f;
    void Start()
    {
        GameEventsManager.instance.OnCameraShake += DoShake;
    }
    void OnDisable()
    {
        GameEventsManager.instance.OnCameraShake -= DoShake;
    }
    private void DoShake()
    {
        if (_cameraAnimator != null)
        {
            _cameraAnimator.SetBool("Shake", true);
        }
        Invoke(nameof(StopShake), _shakeDuration);
    }
    private void StopShake()
    {
        if (_cameraAnimator != null)
        {
            _cameraAnimator.SetBool("Shake", false);
        }
    }
}
