using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunController : MonoBehaviour
{
    private CharacterAttackController attackController;
    private ShootingController shootingController;

    // Start is called before the first frame update
    void Start()
    {
        attackController = GetComponentInParent<CharacterAttackController>();
        shootingController = GetComponentInParent<ShootingController>();
    }

    // Update is called once per frame
    void Update()
    {

        if (gameObject.activeInHierarchy)
        {
            attackController.enabled = false;
            shootingController.enabled = true;
        }

    }
}
