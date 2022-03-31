using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SS_GameController : MonoBehaviour
{
    [Header("Player Data")]
    public SS_Boundary boundary;
    public GameObject player;
    private GameObject activePlayer;
    public Vector3 playerSpawn;

    private SS_Health playerHealth;
    private SS_WeaponController playerWeapon;
    [Header("UI")]
    public Text playerHealthText;
    public Text playerWeaponText;
    public Text playerEnergyText;

    [Header("Enemy Manager")]
    private float startTime;
    [SerializeField]
    public bool isBossFight = false;
    public bool spawnEnabled = false;
    public SpawnZone spawnZone;
    public float initialSpawnInterval;
    private float spawnInterval;
    private float nextSpawn;
    private int lowRange = 0;
    private int highRange = 5;
    public GameObject HeadEnemy;
    public GameObject FistEnemy;
    public GameObject GrabEnemy;
    public GameObject GunEnemy;
    public GameObject SpreadEnemy;


    [Header("Background Mangager")]
    public GameObject background;
    public float startY;
    public float EndY;
    public float timeToReach = 45f;
    private float bgSpeed;

    private void Start()
    {
        startTime = Time.time;
        spawnInterval = initialSpawnInterval;
        activePlayer = Instantiate(player, playerSpawn, Quaternion.identity);
        playerHealth = activePlayer.GetComponent<SS_Health>();
        playerWeapon = activePlayer.GetComponent<SS_WeaponController>();
        bgSpeed = (EndY - startY) / timeToReach;

    }

    private void Update()
    {
        UpdateText();
        if (spawnEnabled && !isBossFight)
        {
            if (Time.time >= nextSpawn)
            {
                GameObject spawnedEnemey = new GameObject();
                switch (Random.Range(lowRange, highRange))
                {
                    case 0:
                        spawnedEnemey = HeadEnemy;
                        break;
                    case 1:
                        spawnedEnemey = FistEnemy;
                        break;
                    case 2:
                        spawnedEnemey = GrabEnemy;
                        break;
                    case 3:
                        spawnedEnemey = GunEnemy;
                        break;
                    case 4:
                        spawnedEnemey = SpreadEnemy;
                        break;
                }
                Instantiate(spawnedEnemey, new Vector3(Random.Range(-spawnZone.width / 2, spawnZone.width / 2), Random.Range(spawnZone.center.y - (spawnZone.height / 2), spawnZone.center.y + (spawnZone.height / 2)), 0f), Quaternion.identity);
                nextSpawn = Time.time + spawnInterval;
            }
        }
        if(Time.time >= 0f + startTime && Time.time < 5f + startTime)
        {
            spawnEnabled = false;
        }
        else if (Time.time >= 5f + startTime && Time.time < 13f + startTime)
        {
            spawnEnabled = true;
            UpdateSpawnData(1, 2, 0.8f);
        }
        else if (Time.time >= 13f + startTime && Time.time < 21f + startTime)
        {
            UpdateSpawnData(0, 1, 0.5f);
        }
        else if (Time.time >= 21f + startTime && Time.time < 29f + startTime)
        {
            UpdateSpawnData(0, 3, 0.85f);
        }
        else if (Time.time >= 29f + startTime && Time.time < 37f + startTime)
        {
            UpdateSpawnData(0, 4, 0.9f);
        }
        else if (Time.time >= 37f + startTime && Time.time < 45f + startTime)
        {
            UpdateSpawnData(0, 5, 1f);
        }
        else if(Time.time >= 45f + startTime)
        {
            spawnEnabled = false;
        }
        if(Time.time <= 45f)
        {
            BackgroundMover();
        }
    }

    void UpdateText()
    {
        playerHealthText.text = "Health: " + playerHealth.currentHealth + "/" + playerHealth.maxHealth;
        playerWeaponText.text = playerWeapon.weaponList[playerWeapon.activeWeapon].WeaponName;
        playerEnergyText.text = "Energy: " + Mathf.RoundToInt(playerWeapon.currentEnergy) + "/" + playerWeapon.maxEnergy;
    }

    void UpdateSpawnData(int low, int high, float newInterval)
    {
        lowRange = low;
        highRange = high;
        spawnInterval = newInterval;
    }

    void BackgroundMover()
    {
        background.transform.Translate(new Vector3(0f, 1f, 0f) * bgSpeed * Time.deltaTime);
    }

    private void LateUpdate()
    {
        activePlayer.transform.position = new Vector2(
            Mathf.Clamp(activePlayer.transform.position.x,
            boundary.CollisionBoundary.center.x - boundary.CollisionBoundary.width / 2,
            boundary.CollisionBoundary.center.x + boundary.CollisionBoundary.width / 2),
            Mathf.Clamp(activePlayer.transform.position.y,
            boundary.CollisionBoundary.center.y - boundary.CollisionBoundary.height / 2,
            boundary.CollisionBoundary.center.y + boundary.CollisionBoundary.height / 2));
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(playerSpawn, 0.25f);

        Gizmos.color = Color.yellow;
        Gizmos.DrawWireCube(boundary.CollisionBoundary.center, new Vector3(boundary.CollisionBoundary.width, boundary.CollisionBoundary.height, 0));

        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(boundary.DeleteBoundary.center, new Vector3(boundary.DeleteBoundary.width, boundary.DeleteBoundary.height, 0));

        Gizmos.color = Color.green;
        Gizmos.DrawWireCube(spawnZone.center, new Vector3(spawnZone.width, spawnZone.height, 0));
    }
}


[System.Serializable]
public class SpawnZone
{
    public float height, width;
    public Vector2 center;
}
