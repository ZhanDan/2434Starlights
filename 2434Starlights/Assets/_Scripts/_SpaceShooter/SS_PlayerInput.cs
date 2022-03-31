using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SS_PlayerController))]
[RequireComponent(typeof(SS_WeaponController))]
public class SS_PlayerInput : MonoBehaviour
{

    SS_PlayerController playerController;
    SS_WeaponController weaponController;
    public bool isDisabled = false;

    private void Awake()
    {
        playerController = GetComponent<SS_PlayerController>();
        weaponController = GetComponent<SS_WeaponController>();
    }

    private void Update()
    {
        if (!isDisabled)
        {
            if (Input.GetKey(KeyCode.J))
            {
                weaponController.FireWeapon();
            }
            if (Input.GetKeyDown(KeyCode.I))
            {
                weaponController.ToggleInfinite();
            }

            if (Input.GetKeyDown(KeyCode.K))
            {
                weaponController.CycleUpWeaponList();
            }

            if (Input.GetKeyDown(KeyCode.L))
            {
                weaponController.CycleDownWeaponList();
            }

            Vector2 inputDir = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical")).normalized;
            playerController.GetInputDirection(ref inputDir);
        }
    }
}
