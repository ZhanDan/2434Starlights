using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAiBasic : MonoBehaviour
{
    private SS_WeaponController weaponController;
    public float shootingLength;
    public float timeBetweenFire;
    private bool isFiring;

    private void Start()
    {
        weaponController = GetComponent<SS_WeaponController>();
    }

    private void Update()
    {
        weaponController.FireWeapon();
    }
}
