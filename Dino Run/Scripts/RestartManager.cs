using UnityEngine;
using UnityEngine.SceneManagement; // Nécessaire pour gérer les scènes
using UnityEngine.UI; // Nécessaire pour manipuler les boutons

public class RestartManager : MonoBehaviour
{
    public Button restartButton; // Bouton pour redémarrer le jeu
    private ScoreManager scoreManager; // Référence au ScoreManager

    private void Start()
    {
        // Vérifie si le bouton est assigné dans l'inspecteur
        if (restartButton != null)
        {
            // Ajoute l'événement RestartGame au bouton
            restartButton.onClick.AddListener(RestartGame);
        }
        else
        {
            Debug.LogWarning("Aucun bouton Restart assigné au RestartManager !");
        }

        // Trouve le ScoreManager dans la scène
        scoreManager = FindObjectOfType<ScoreManager>();
        if (scoreManager == null)
        {
            Debug.LogError("Aucun ScoreManager trouvé dans la scène !");
        }
    }

    public void RestartGame()
    {
        // Appelle EndGame() pour sauvegarder le High Score avant le redémarrage
        if (scoreManager != null)
        {
            scoreManager.EndGame();
        }

        // Réactive le temps avant de recharger la scène
        Time.timeScale = 1f;

        // Recharge la scène active
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);

        Debug.Log("Jeu redémarré !");
    }
}
