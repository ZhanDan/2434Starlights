using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class SS_GunWeapon : SS_WeaponBase
{
    [Range(1,100)]
    public int barrelCount = 1;
    [Range(0, 360)]
    public float barrelSpreadAngle = 0f;
    [SerializeField]
    private float stepAngle = 0;

    public override void Fire(Vector2 shooterPosition)
    {
        for (int i = 0; i < firePoints.Length; i++)
        {
            Vector3 vectorDirection = firePoints[i].fireDirection.normalized;
            float directionAngle = (Mathf.Atan2(vectorDirection.y, vectorDirection.x) * Mathf.Rad2Deg) - 90f;
            if(barrelCount > 1)
            {
                directionAngle = CalculateStartAngle(directionAngle);
                for (int j = 0; j < barrelCount; j++)
                {
                    Instantiate(fireable, shooterPosition + firePoints[i].firePoint, Quaternion.Euler(0, 0, directionAngle));
                    directionAngle -= stepAngle;
                }
            }
            else
            {
                Instantiate(fireable, shooterPosition + firePoints[i].firePoint, Quaternion.Euler(0, 0, directionAngle));
            }
        }
    }

    float CalculateStartAngle(float forwardAngle)
    {
        float newAngle = forwardAngle;
        float individualSpread = barrelSpreadAngle / (barrelCount - 1);
        stepAngle = individualSpread;
        float startAngle = barrelSpreadAngle / 2;
        return newAngle + startAngle;
    }
}
