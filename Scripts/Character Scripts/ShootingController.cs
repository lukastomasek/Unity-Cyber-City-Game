using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootingController : MonoBehaviour
{
    [Range(0.5f,2.5f)]
    [SerializeField]
    private float _fireRate = 1f;

    [Range(1, 20)]
    [SerializeField]
    private int _damage = 1;

    private float _timer;

    [SerializeField]
    private Transform _firePoint;

    [SerializeField]
    private ParticleSystem _muzzleFlash;

    [SerializeField]
    private Transform _muzzlePoint;

    private CharacterAnimations _anim;


    public AudioSource audioSource;
    public AudioClip gunSound;


    private void Awake()
    {
        _anim = GetComponent<CharacterAnimations>();
    }

    private void Update()
    {
        Shoot();
    }


    void Shoot()
    {
        _timer += Time.deltaTime;

        if(_timer >= _fireRate)
        {
            if (Input.GetButton("Fire1"))
            {
                _timer = 0f;
                _anim.EnemyShoot();
                FireGun();
                _muzzleFlash.Play();
                audioSource.PlayOneShot(gunSound);
            }
        }

    }

    private void FireGun()
    {

        Debug.DrawRay(_firePoint.position, _firePoint.forward * 100, Color.red, 2f);

        Ray ray = new Ray(_firePoint.position, _firePoint.forward);
        RaycastHit hitInfo;

        if(Physics.Raycast(ray, out hitInfo, 100f))
        {
            UniversalHealthController enemyHealth = hitInfo.collider.GetComponent<UniversalHealthController>();

            if(enemyHealth != null)
            {
                if (enemyHealth.isEnemy)
                {
                    enemyHealth.ApplyDamage(_damage);
                    Debug.Log("enemy's health :" + enemyHealth.health + hitInfo.collider.gameObject.name);
                }
            }
        }
    }









} // end 


