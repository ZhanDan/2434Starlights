using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SS_WeaponBase : ScriptableObject
{
    public string WeaponName;
    public float energyCost = 0;
    public GameObject fireable;
    public FirePoint[] firePoints;
    [Range(0,100)]
    public float fireInterval;

    public virtual void OnStart() { }
    public virtual void Fire(Vector2 shooterPosition) 
    { 
        for(int i = 0; i < firePoints.Length; i++)
        {
            Instantiate(fireable, shooterPosition + firePoints[i].firePoint, Quaternion.LookRotation(firePoints[i].fireDirection));
        }
    }

    public virtual void UpdateWeaponState(Vector2 shooterPosition) { }

    public virtual void OnSwitch() { }

    [System.Serializable]
    public class FirePoint
    {
        public Vector2 firePoint;
        public Vector2 fireDirection;
    }
}
