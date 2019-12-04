using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpecialEffectController : MonoBehaviour
{
    public static SpecialEffectController instance;

    private ParticleSystem particle;

    private void Awake()
    {
        particle = GetComponentInChildren<ParticleSystem>();
        particle.Stop();

        if (instance == null)
            instance = this;
    }

    public void PlaySpecialAttack()
    {
        if (!gameObject.activeInHierarchy)
        {
            gameObject.SetActive(true);
        }
        particle.Play();
       
    }

    public void DeactivateParticleSystem()
    {
        gameObject.SetActive(false);
    }
}
