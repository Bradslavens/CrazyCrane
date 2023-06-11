using System.Collections.Generic;
using UnityEngine;

public class SeekAndDestroy : MonoBehaviour
{
    public Transform enemySpawner;

    private EnemyManager enemyManager; // Reference to the EnemyManager script

    public GameObject projectilePrefab;
    public Transform muzzle;
    public int projectilePoolSize = 10;
    private List<GameObject> projectilePool = new List<GameObject>();
    private int currentProjectileIndex = 0;

    public float fireInterval = 1f;
    private float fireTimer;

    public float movementSpeed = 1f; // The speed at which this game object will move towards the target

    private FriendController friendController;

    private void Awake()
    {

        // Find the Enemy Manager GameObject
        GameObject enemyManagerObject = GameObject.Find("EnemyManager");

        // Check if we found it
        if (enemyManagerObject != null)
        {
            // Get the EnemyManager component
            enemyManager = enemyManagerObject.GetComponent<EnemyManager>();
        }

        // Check if we got the component
        if (enemyManager == null)
        {
            Debug.LogError("Could not find EnemyManager component on Enemy Manager object");
        }

        InitializeProjectilePool();
    }

    void Start()
    {
        friendController = GetComponent<FriendController>();

        if (friendController == null)
        {
            Debug.LogError("FreindController component not found!");
        }

    }

    private void InitializeProjectilePool()
    {
        for (int i = 0; i < projectilePoolSize; i++)
        {
            Debug.Log("pool");
            GameObject projectile = Instantiate(projectilePrefab);
            projectile.SetActive(false);
            projectilePool.Add(projectile);
        }
    }

    private void Update()

    {
        if (friendController.currentState == FriendController.CharacterState.Shooting)
        {
            GameObject closestEnemy = FindClosestEnemy();
            if (closestEnemy != null)
            {
                RotateTowardsEnemy(closestEnemy);
                FireAtEnemy();
            }
        }

    }


    private GameObject FindClosestEnemy()
    {
        Debug.Log("found");

        GameObject closestEnemy = null;
        float closestDistance = Mathf.Infinity;

        foreach (GameObject enemyNPC in enemyManager.currentEnemies)
        {
            if (enemyNPC.activeSelf) // Check if the enemy is active
            {
                float distance = Vector3.Distance(transform.position, enemyNPC.transform.position);
                if (distance < closestDistance)
                {
                    closestDistance = distance;
                    closestEnemy = enemyNPC;
                }
            }
        }

        return closestEnemy;
    }

    private void RotateTowardsEnemy(GameObject enemy)
    {
        Vector3 direction = enemy.transform.position - transform.position;
        direction.y = 0f; // Set the y-component to zero to restrict rotation to the y-axis only
        Quaternion rotation = Quaternion.LookRotation(direction);
        transform.rotation = rotation;
    }

    private void FireAtEnemy()
    {
        fireTimer += Time.deltaTime;
        if (fireTimer >= fireInterval)
        {
            // Get the next available projectile from the pool
            GameObject projectile = GetNextAvailableProjectile();

            // If there are no available projectiles, return
            if (projectile == null) return;

            // Set the position and rotation of the projectile
            projectile.transform.position = muzzle.position;
            projectile.transform.rotation = muzzle.rotation;

            // Activate the projectile
            projectile.SetActive(true);

            // Get the projectile's Rigidbody component
            Rigidbody projectileRigidbody = projectile.GetComponent<Rigidbody>();

            // Set the speed of the projectile
            float projectileSpeed = 50f; // Adjust the speed as desired

            // Calculate the movement vector based on the projectile's local forward direction
            Vector3 velocity = projectile.transform.forward * projectileSpeed;

            // Set the velocity of the projectile's Rigidbody in local space
            projectileRigidbody.velocity = velocity;

            // Reset the timer
            fireTimer = 0f;
        }
    }

    private GameObject GetNextAvailableProjectile()
    {
        // If the projectile pool is empty, return null
        if (projectilePool.Count == 0) return null;

        // Get the next available projectile from the pool
        GameObject projectile = projectilePool[currentProjectileIndex];
        currentProjectileIndex = (currentProjectileIndex + 1) % projectilePool.Count;
        return projectile;
    }
}
