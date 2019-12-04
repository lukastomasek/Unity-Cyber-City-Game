using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnscaledParticleSystem : MonoBehaviour
{
    private ParticleSystem _particleSystem;

    private void OnEnable()
    {
        _particleSystem = GetComponent<ParticleSystem>();
    }

    private void Awake()
    {
        _particleSystem.Simulate(Time.unscaledDeltaTime, true, false);
    }
}
