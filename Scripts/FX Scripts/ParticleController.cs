using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleController : MonoBehaviour
{
    private ParticleSystem _particles;


   
    void Awake()
    {
        _particles = GetComponentInChildren<ParticleSystem>();
        _particles.Play();
    }

}
