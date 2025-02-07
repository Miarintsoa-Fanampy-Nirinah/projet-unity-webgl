using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float jumpForce = 5f; // Force du saut
    public bool isDead = false; // Détecte si le joueur est mort
    public GameObject gameOverUI; // Référence à l'UI Game Over
    public Animator playerAnimator; // Animator du joueur
    private Rigidbody2D rb;
    private bool isGrounded = false; // Vérifie si le joueur est au sol

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        if (isDead) return; // Si Game Over, ne rien faire

        if (IsJumpTriggered() && isGrounded) // Saut autorisé uniquement si au sol
        {
            Jump();
        }

        // Met à jour l'état de l'animation
        playerAnimator.SetBool("Jump", !isGrounded);
    }

    void Jump()
    {
        rb.linearVelocity = Vector2.up * jumpForce; // Applique la force de saut
        isGrounded = false; // Empêche de sauter à nouveau
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = true; // Si le joueur touche le sol, il peut sauter
        }

        if (collision.gameObject.CompareTag("Obstacle"))
        {
            GameOver(); // Si le joueur touche un obstacle, Game Over
        }
    }

    void GameOver()
    {
        isDead = true;

        // Désactiver les Rigidbody pour geler le mouvement
        rb.simulated = false;

        // Arrêter les animations
        playerAnimator.SetBool("Jump", false);
        playerAnimator.SetBool("Dead", true); // Activer l'animation "Dead"

        // Arrêter les obstacles via ObstacleManager
        ObstacleManager.instance.StopSpawning();

        // Afficher l'UI Game Over
        gameOverUI.SetActive(true);

        // Mettre le jeu en pause
        Time.timeScale = 0f; // Stopper complètement le temps du jeu
    }

    bool IsJumpTriggered()
    {
        // Détecte les entrées utilisateur pour le saut
        if (Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.UpArrow)) return true;
        if (Input.GetMouseButtonDown(0)) return true;
        if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began) return true;
        return false;
    }
}
