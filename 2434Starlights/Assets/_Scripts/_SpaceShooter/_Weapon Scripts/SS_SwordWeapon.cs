using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class SS_SwordWeapon : SS_WeaponBase
{
    private bool isSwinging = false;
    public float swingSpeed = 1f;
    public float distanceFromOrigin = 0f;
    [SerializeField]
    private Dictionary<GameObject, int> swingingSwords = new Dictionary<GameObject, int>();//sword key, firepoint number

    [Header("Orbit Movement")]
    private bool orbit = true;
    public int orbitCount = 1;
    private float orbitStepAngle;
    private float orbitLifetime;

    [Header("Arc Movement")]
    public float swingAngle = 120f;
    private float swingProgression;
    private int swingDirection = -1;
    [SerializeField]
    private bool swingRight = false;
    private float comboSwingInterval = 0.2f; //time between swinging to count as a combo
    private float lastSwung = 0f;

    public override void OnStart()
    {
        SwingExpire();
    }

    public override void Fire(Vector2 shooterPosition)
    {
        isSwinging = true;
        if (orbit)
        {
            orbitStepAngle = 360f / orbitCount;
            orbitLifetime = fireInterval;
            for(int i = 0; i < firePoints.Length; i++)
            {
                Vector3 vectorDirection = firePoints[i].fireDirection.normalized;
                float directionAngle = (Mathf.Atan2(vectorDirection.y, vectorDirection.x) * Mathf.Rad2Deg) - 90f; //in degrees
                //Debug.Log(directionAngle);
                Vector3 centerPos = shooterPosition + firePoints[i].firePoint;
                //Debug.Log(centerPos);
                for (int j = 0; j < orbitCount; j++)
                {
                    //when calculating for rotation subtract 90 degrees, for position if 90 degrees was removed, add 90 degrees, otherwise leave it alone
                    float temp = (j * orbitStepAngle) + directionAngle + 90f;
                    float x = (distanceFromOrigin * Mathf.Cos(temp * Mathf.Deg2Rad)) + centerPos.x;
                    float y = (distanceFromOrigin * Mathf.Sin(temp * Mathf.Deg2Rad)) + centerPos.y;
                    Vector3 newPos = new Vector3(x, y, 0f);
                    swingingSwords.Add(Instantiate(fireable, newPos, Quaternion.Euler(0, 0, temp - 90f)), i);
                }
            }
        }
        else if (!orbit)
        {
            //NOT ORBIT IS BROKENSO DONT BOTHER IMPELMETN GIN
            //float startAngle = swingAngle / 2;
            //if (lastSwung < Time.time)
            //{
            //    //swing right
            //    swingDirection = -1;
            //    startAngle *= -1;
            //}
            //else
            //{
            //    //swing left
            //    swingDirection = 1;
            //}
            //lastSwung = Time.time;
            //for (int i = 0; i < firePoints.Length; i++)
            //{
            //    Vector3 vectorDirection = firePoints[i].fireDirection.normalized;
            //    float directionAngle = (Mathf.Atan2(vectorDirection.y, vectorDirection.x) * Mathf.Rad2Deg) - 90f + startAngle; //in degrees
            //    //Debug.Log(directionAngle);
            //    Vector3 centerPos = shooterPosition + firePoints[i].firePoint;
            //    //Debug.Log(centerPos);

            //    float temp = directionAngle + 90f;
            //    float x = (distanceFromOrigin * Mathf.Cos(temp * Mathf.Deg2Rad)) + centerPos.x;
            //    float y = (distanceFromOrigin * Mathf.Sin(temp * Mathf.Deg2Rad)) + centerPos.y;
            //    Vector3 newPos = new Vector3(x, y, 0f);
            //    swingingSwords.Add(Instantiate(fireable, newPos, Quaternion.Euler(0, 0, temp - 90f)), i);
            //}

        }
    }

    public override void UpdateWeaponState(Vector2 shooterPosition)
    {
        if (isSwinging)
        {
            if (orbit)
            {
                orbitLifetime -= Time.deltaTime;
                foreach(KeyValuePair<GameObject, int> orb in swingingSwords)
                {
                    float newAngle = orb.Key.transform.localEulerAngles.z - (swingSpeed * Time.deltaTime) + 90f;
                    Vector3 centerPos = shooterPosition + firePoints[swingingSwords[orb.Key]].firePoint;

                    float x = (distanceFromOrigin * Mathf.Cos(newAngle * Mathf.Deg2Rad)) + centerPos.x;
                    float y = (distanceFromOrigin * Mathf.Sin(newAngle * Mathf.Deg2Rad)) + centerPos.y;
                    Vector3 newPos = new Vector3(x, y, 0f);
                    orb.Key.transform.position = newPos;
                    orb.Key.transform.rotation = Quaternion.Euler(0, 0, newAngle - 90f);
                }

                if (orbitLifetime <= 0f)
                {
                    SwingExpire();
                }
            }else if (!orbit)
            {
                //NON ORBIT IS BROKEN SO DON'T BOTHER IMPELEMENTING
                //foreach (KeyValuePair<GameObject, int> orb in swingingSwords)
                //{
                //    Vector3 vectorDirection = firePoints[swingingSwords[orb.Key]].fireDirection.normalized;

                //    float startAngle = (Mathf.Atan2(vectorDirection.y, vectorDirection.x) * Mathf.Rad2Deg) - 90f + (-1 * swingDirection * swingAngle / 2);
                //    float endAngle = (Mathf.Atan2(vectorDirection.y, vectorDirection.x) * Mathf.Rad2Deg) - 90f + (swingDirection * swingAngle / 2);

                //    Debug.Log(swingDirection);
                //    float newAngle = orb.Key.transform.localEulerAngles.z + (swingDirection * swingSpeed * Time.deltaTime) + 90f;
                //    Debug.Log(startAngle + " : " + endAngle + " : " + newAngle);
                //    Vector3 centerPos = shooterPosition + firePoints[swingingSwords[orb.Key]].firePoint;

                //    float x = (distanceFromOrigin * Mathf.Cos(newAngle * Mathf.Deg2Rad)) + centerPos.x;
                //    float y = (distanceFromOrigin * Mathf.Sin(newAngle * Mathf.Deg2Rad)) + centerPos.y;
                //    Vector3 newPos = new Vector3(x, y, 0f);
                //    orb.Key.transform.position = newPos;
                //    orb.Key.transform.rotation = Quaternion.Euler(0, 0, newAngle - 90f);
                //    if(swingDirection == -1 && newAngle <= endAngle)
                //    {
                //        SwingExpire();
                //        return;
                //    }
                //    else if (swingDirection == 1 && newAngle >= startAngle)
                //    {
                //        SwingExpire();
                //        return;
                //    }
                //}
            }
        }
    }

    public override void OnSwitch()
    {
        SwingExpire();
    }

    void SwingExpire()
    {
        isSwinging = false;
        Debug.Log("cleared");
        Debug.Log(swingingSwords.Count);
        foreach (KeyValuePair<GameObject, int> orb in swingingSwords)
        {
            Destroy(orb.Key);
        }
        swingingSwords.Clear();
        Debug.Log(swingingSwords.Count);
        return;
    }
}
