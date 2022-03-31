using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SS_WeaponController : MonoBehaviour
{
    public List<SS_WeaponBase> weaponList = new List<SS_WeaponBase>();
    public int activeWeapon = 0;
    public bool infiniteEnergy;
    public float maxEnergy = 100;
    [SerializeField]
    public float currentEnergy;
    public float rechargeRate;

    private float nextFire;
    public float timeTillRecharge = 1.25f;
    private float lastFired;

    private void Start()
    {
        currentEnergy = maxEnergy;
        for(int i = 0; i < weaponList.Count; i++)
        {
            weaponList[i].OnStart();
        }
    }

    public void FireWeapon()
    {
        if (Time.time > nextFire && currentEnergy >= weaponList[activeWeapon].energyCost)
        {
            lastFired = Time.time;
            nextFire = Time.time + weaponList[activeWeapon].fireInterval;
            currentEnergy = infiniteEnergy ? maxEnergy : Mathf.Clamp(currentEnergy - weaponList[activeWeapon].energyCost, 0, maxEnergy);
            weaponList[activeWeapon].Fire(transform.position);
        }
    }

    public void ToggleInfinite()
    {
        infiniteEnergy = !infiniteEnergy;
    }

    private void Update()
    {
        if(Time.time > lastFired + timeTillRecharge)
        {
            currentEnergy = infiniteEnergy ? maxEnergy : Mathf.Clamp(currentEnergy + (rechargeRate * Time.deltaTime), 0, maxEnergy);
        }
        UpdateWeaponState();
    }

    public void UpdateWeaponState()
    {
        weaponList[activeWeapon].UpdateWeaponState(transform.position);
    }

    public void CycleUpWeaponList()
    {
        int prevNumber = activeWeapon;
        activeWeapon += 1;
        if (activeWeapon >= weaponList.Count)
        {
            activeWeapon = 0;
        }
        if (prevNumber != activeWeapon)
        {
            weaponList[prevNumber].OnSwitch();
            weaponList[activeWeapon].OnSwitch();
        }
    }

    public void CycleDownWeaponList()
    {
        int prevNumber = activeWeapon;
        activeWeapon -= 1;
        if (activeWeapon < 0)
        {
            activeWeapon = weaponList.Count - 1;
        }
        if (prevNumber != activeWeapon)
        {
            weaponList[prevNumber].OnSwitch();
            weaponList[activeWeapon].OnSwitch();
        }
    }

    public void OnDrawGizmosSelected()
    {
        float size = 0.75f;
        for (int i = 0; i < weaponList[activeWeapon].firePoints.Length; i++)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(new Vector2(transform.position.x, transform.position.y) + weaponList[activeWeapon].firePoints[i].firePoint, 0.25f);
            Gizmos.color = Color.blue;
            Gizmos.DrawLine(new Vector2(transform.position.x, transform.position.y) + weaponList[activeWeapon].firePoints[i].firePoint, new Vector2(transform.position.x, transform.position.y) + weaponList[activeWeapon].firePoints[i].firePoint + weaponList[activeWeapon].firePoints[i].fireDirection.normalized * size);
        }

    }
}
