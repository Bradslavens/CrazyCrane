using UnityEngine;

public class CharacterSpawner : MonoBehaviour
{
    public GameObject characterPrefab;
    public int numberOfCharacters = 5;
    public Transform waypoint1;
    public Transform waypoint2;
    public Transform target;

    public EnemyManager enemyManager;

    private void Awake()
    {
        SpawnCharacters();
    }

    private void SpawnCharacters()
    {
        for (int i = 0; i < numberOfCharacters; i++)
        {
            Vector3 randomPosition = GetRandomPosition();
            GameObject characterInstance = Instantiate(characterPrefab, randomPosition, Quaternion.identity, transform);
            NPCController characterScript = characterInstance.GetComponent<NPCController>();
            if (characterScript != null)
            {
                characterScript.target = target;
            }

            enemyManager.AddEnemy(characterInstance);
        }
    }

    private Vector3 GetRandomPosition()
    {
        float minX = Mathf.Min(waypoint1.position.x, waypoint2.position.x);
        float maxX = Mathf.Max(waypoint1.position.x, waypoint2.position.x);
        float minY = Mathf.Min(waypoint1.position.y, waypoint2.position.y);
        float maxY = Mathf.Max(waypoint1.position.y, waypoint2.position.y);
        float minZ = Mathf.Min(waypoint1.position.z, waypoint2.position.z);
        float maxZ = Mathf.Max(waypoint1.position.z, waypoint2.position.z);

        float randomX = Random.Range(minX, maxX);
        float randomY = Random.Range(minY, maxY);
        float randomZ = Random.Range(minZ, maxZ);

        return new Vector3(randomX, randomY, randomZ);
    }
}
