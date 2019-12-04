using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoliceDroneController : MonoBehaviour
{
    [SerializeField]
    private float _hoverSpeed = 1f;
    private float positionY;

    [SerializeField]
    private Transform _target;

    [SerializeField]
    private int _speed = 2;

    private int _minDistance = -164, _maxDistance = - 170;

    [SerializeField]
    private float _targetInDistance = 4f;

    [SerializeField]
    private float _damage = 10f;

    private Light _pointLight;

    private AudioSource _audioSource;
    public List<AudioClip> robotWarings = new List<AudioClip>();
    public AudioClip shootClip;

    public float slerpTime = 1.2f;

    public float fireRate = 2.1f;
    public GameObject shootFx;
    public Transform shootPoint;

    
    private float _defaultTimer;
    private bool startShootingAfterWaring;

    // Start is called before the first frame update
    void Start()
    {
        positionY = transform.position.y;
        _audioSource = GetComponent<AudioSource>();
        _pointLight = GetComponentInChildren<Light>();
    }

    // Update is called once per frame
    void Update()
    {
        Hover();
        LookAtTarget();
        MoveAround();
    }


    private void Hover()
    {
        Vector3 hoverPos = transform.position;
        hoverPos = new Vector3(hoverPos.x, positionY + Mathf.Sin(Time.time * _hoverSpeed) * 0.2f, hoverPos.z);

        transform.position = hoverPos;
    }


    private void MoveAround()
    {
        if(Vector3.Distance(_target.position, transform.position)<= _targetInDistance)
        {
          Vector3 tempPos = transform.position;
          tempPos.x += _speed * Time.deltaTime;

          if(tempPos.x > _maxDistance)
          {
              _speed *= -1;
          }    
          if(tempPos.x < _minDistance)
          {
             _speed *= -1;
          }

            transform.position = tempPos;
        }
    }

    private void LookAtTarget()
    {
        if (_target != null )
        {
            if (Vector3.Distance(_target.position, transform.position) <= _targetInDistance)
            {
                Vector3 direction = _target.position - transform.position;
                Quaternion directionToFace = Quaternion.LookRotation(direction);
               
                transform.rotation = Quaternion.Slerp(transform.rotation, directionToFace, slerpTime);

                startShootingAfterWaring = true;
                StartShooting();

                _pointLight.color = Color.red;
            }
            else if ( Vector3.Distance(_target.position, transform.position)> _targetInDistance)
            {
                _pointLight.color = Color.blue;
                startShootingAfterWaring = false;
            }
        }
    }

    public void StartShooting()
    {
        _defaultTimer += Time.deltaTime;

        if(_defaultTimer >= fireRate && startShootingAfterWaring == true)
        {
            _defaultTimer = 0f;
         
            var effect = Instantiate(shootFx, shootPoint.position, Quaternion.identity);
            effect.transform.SetParent(transform);

            _audioSource.clip = shootClip;
            _audioSource.Play();

            Vector3 shootDir = _target.position - transform.position + Vector3.up;
            Ray ray = new Ray(transform.position, shootDir);
            RaycastHit hit;

            if(Physics.Raycast(ray, out hit))
            {
              
                if (hit.transform.tag == "Player")
                {
                    UniversalHealthController playerHealth =  hit.transform.gameObject.GetComponent<UniversalHealthController>();
                    if(playerHealth != null)
                    {
                        if (playerHealth.isPlayer)
                        {
                            playerHealth.ApplyDamage(_damage);
                        }
                        if(playerHealth.health <= 0)
                        {
                            _audioSource.Stop();
                            enabled = false;
                            startShootingAfterWaring = false;
                        }
                    }

                   // Debug.Log("player's health :" + playerHealth.health);
                }
            }
           
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, _targetInDistance);
    }



    private void OnTriggerEnter(Collider collider)
    {
        if(collider.tag == Tags.PLAYER)
        {
            var randomWaring = Random.Range(0, robotWarings.Count);
            for (int i = 0; i < randomWaring; i++)
            {
                _audioSource.clip = robotWarings[randomWaring];
                _audioSource.PlayOneShot(robotWarings[i]);
         
               
            }
        }
    }

}// end
