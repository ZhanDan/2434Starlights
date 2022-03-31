using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SS_HitDetection : MonoBehaviour
{
    public SS_DamageDealer damageDealer;

    public LayerMask hitLayer;
    private List<int> intLayers = new List<int>();

    public enum DetectType { onHit, lifetime };
    public DetectType detectType = DetectType.onHit;

    public bool checkOnHit = false;
    public int hitCount = 1;    //number of damage procs before expiration
    public bool checkLifetime = false;
    public float lifetime = 5;  //time until expiration

    public Dictionary<GameObject, HitInfo> entitiesHit = new Dictionary<GameObject, HitInfo>(); //if onHit then float is a whole number counting the number of hits
    public int hitPerSec = 1;
    private float hitInterval;


    Collider2D hitbox;
    private void Start()
    {
        hitInterval = 60f / hitPerSec;
        GetLayersFromMask();
        hitbox = GetComponent<Collider2D>();
    }

    private void Update()
    {
        if (checkLifetime)
        {
            lifetime -= Time.deltaTime;
            if (lifetime <= 0)
            {
                Expire();
            }
        }
    }

    void HitProc(GameObject gm)
    {
        //deal damage here
        Debug.Log(gm);
        if (gm.GetComponent<SS_Health>())
        {
            if (gm.GetComponent<SS_Health>().ReceiveDamage(damageDealer.damage))//if true the object died
            {
                entitiesHit.Remove(gm);
            }
            entitiesHit[gm].hitCount += 1;
            entitiesHit[gm].timeTillHit = Time.time + hitInterval;
            if (checkOnHit)
            {
                if (entitiesHit[gm].hitCount >= hitCount)
                {
                    Expire();
                }
            }
        }
    }

    public void Expire()
    {
        Destroy(gameObject);
    }

    void GetLayersFromMask()
    {
        for(int i = 0; i < 32; i++)
        {
            if (hitLayer == (hitLayer | (1 << i)))
            {
                intLayers.Add(i);
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.gameObject.CompareTag(gameObject.tag) && intLayers.Contains(collision.gameObject.layer) && !entitiesHit.ContainsKey(collision.gameObject))
        {
            entitiesHit.Add(collision.gameObject, new HitInfo());
            entitiesHit[collision.gameObject].hitCount = 0;
            HitProc(collision.gameObject);
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (!collision.gameObject.CompareTag(gameObject.tag) && intLayers.Contains(collision.gameObject.layer) && entitiesHit.ContainsKey(collision.gameObject))
        {
            if (Time.time >= entitiesHit[collision.gameObject].timeTillHit)
            {
                HitProc(collision.gameObject);
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (!collision.gameObject.CompareTag(gameObject.tag) && intLayers.Contains(collision.gameObject.layer) && entitiesHit.ContainsKey(collision.gameObject))
        {
            if (Time.time >= entitiesHit[collision.gameObject].timeTillHit)
            {
                HitProc(collision.gameObject);
            }
            entitiesHit.Remove(collision.gameObject);
        }
    }

    public class HitInfo
    {
        public int hitCount;
        public float timeTillHit;
    }
}
