using System.Collections.Generic;
using UnityEngine;

public class SeekAndDestroy : MonoBehaviour
{
    public Transform enemySpawner;
    private List<GameObject> enemyNPCs = new List<GameObject>();

    public GameObject projectilePrefab;
    public Transform muzzle;
    public int projectilePoolSize = 10;
    private List<GameObject> projectilePool = new List<GameObject>();
    private int currentProjectileIndex = 0;

    public float fireInterval = 1f;
    private float fireTimer;

    public GameObject targetObject; // The object that this script will interact with

    public float movementSpeed = 1f; // The speed at which this game object will move towards the target

    private bool shouldMove = true; // A flag to determine if the game object should continue moving

    private Animator animator; // Animator component

    private void Awake()
    {
        // Get the Animator component
        animator = GetComponent<Animator>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject == targetObject) // Check if the triggered object is the target object
        {
            // Stop moving
            shouldMove = false;

            // Start shooting animation
            animator.SetTrigger("Shooting");

            // Rotate towards target
            RotateTowardsTarget();

            // Populate the enemyNPCs list when the trigger is entered
            PopulateEnemyNPCs();

            // Initialize the projectile pool
            InitializeProjectilePool();
        }
    }

    private void RotateTowardsTarget()
    {
        Vector3 direction = targetObject.transform.position - transform.position;
        direction.y = 0;  // Keep the rotation on the Y-axis only
        Quaternion rotation = Quaternion.LookRotation(direction);
        transform.rotation = Quaternion.Slerp(transform.rotation, rotation, 1);
    }


    private void PopulateEnemyNPCs()
    {
        Debug.Log("populating enemies");

        foreach (Transform child in enemySpawner)
        {
            if (child.CompareTag("Enemy"))
            {
                enemyNPCs.Add(child.gameObject);
            }
        }
    }

    private void InitializeProjectilePool()
    {
        for (int i = 0; i < projectilePoolSize; i++)
        {
            GameObject projectile = Instantiate(projectilePrefab);
            projectile.SetActive(false);
            projectilePool.Add(projectile);
        }
    }

    private void Update()
    {
        if (shouldMove)
        {
            // Move towards the target
            MoveTowardsTarget();
        }

        // Always face the target
        RotateTowardsTarget();

        // Example usage: Find closest enemy and rotate towards it
        GameObject closestEnemy = FindClosestEnemy();
        if (closestEnemy != null)
        {
            RotateTowardsEnemy(closestEnemy);
            FireAtEnemy();
        }
    }

    private void MoveTowardsTarget()
    {
        float step = movementSpeed * Time.deltaTime;
        transform.position = Vector3.MoveTowards(transform.position, targetObject.transform.position, step);
    }

    private GameObject FindClosestEnemy()
    {
        GameObject closestEnemy = null;
        float closestDistance = Mathf.Infinity;
        Debug.Log("enemies list " + enemyNPCs.Count);

        foreach (GameObject enemyNPC in enemyNPCs)
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
        // Get the next available projectile from the pool
        GameObject projectile = projectilePool[currentProjectileIndex];
        currentProjectileIndex = (currentProjectileIndex + 1) % projectilePool.Count;
        return projectile;
    }
}
