using System.Collections;
using UnityEngine;
public class ObstacleManager : MonoBehaviour
{
    public GameObject[] obstacles; // Tableau contenant les 4 types d'obstacles
    public Transform spawnPoint; // Position de spawn des obstacles
    public float minSpawnInterval = 1f; // Intervalle minimum entre les apparitions
    public float maxSpawnInterval = 3f; // Intervalle maximum entre les apparitions
    public float moveSpeed = 5f; // Vitesse de déplacement des obstacles
    public float obstacleLifetime = 10f; // Temps avant la destruction des obstacles (modifiable dans l'inspecteur)
    public static ObstacleManager instance; // Singleton pour accéder facilement au script
    private bool stopSpawning = false;

    void Awake()
    {
        instance = this; // Initialisation du singleton
    }

    void Start()
    {
        StartCoroutine(SpawnObstacles());
    }

    void Update()
    {
        if (!stopSpawning)
        {
            foreach (Transform obstacle in transform) // Déplace tous les obstacles enfants
            {
                obstacle.position += Vector3.left * moveSpeed * Time.deltaTime;
            }
        }
    }

    IEnumerator SpawnObstacles()
    {
        while (!stopSpawning)
        {
            int randomIndex = Random.Range(0, obstacles.Length); // Sélectionne un obstacle aléatoire parmi les 4
            GameObject newObstacle = Instantiate(obstacles[randomIndex], spawnPoint.position, Quaternion.identity);
            newObstacle.transform.parent = transform; // Définit le parent comme étant le GameObject qui contient ce script
            
            Destroy(newObstacle, obstacleLifetime); // Détruit l'obstacle après le temps défini par obstacleLifetime (modifiable dans l'inspecteur)

            float randomInterval = Random.Range(minSpawnInterval, maxSpawnInterval); // Calcule un intervalle aléatoire
            yield return new WaitForSeconds(randomInterval); // Attend avant de créer le prochain obstacle
        }
    }

    public void StopSpawning()
    {
        stopSpawning = true; // Arrête le spawn de nouveaux obstacles
        foreach (Transform obstacle in transform)
        {
            Rigidbody2D rb = obstacle.GetComponent<Rigidbody2D>();
            if (rb != null)
            {
                rb.simulated = false; // Stoppe le mouvement des obstacles existants
            }
        }
    }
}
