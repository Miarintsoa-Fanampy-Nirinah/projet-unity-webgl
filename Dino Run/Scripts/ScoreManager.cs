using TMPro;
using UnityEngine;
using System.Collections;

public class ScoreManager : MonoBehaviour
{
    public TextMeshProUGUI scoreText; // Affiche le score actuel
    public TextMeshProUGUI highScoreText; // Affiche le high score

    private int currentScore = 0; // Le score actuel
    private int highScore = 0; // Le score maximum sauvegardé
    private float timer = 0f; // Pour l'incrémentation progressive du score
    private float pointInterval = 0.1f; // Intervalle (secondes) pour ajouter des points
    private int pointsPerInterval = 1; // Points ajoutés à chaque intervalle
    private bool isBlinking = false; // Indique si un clignotement est en cours

    private void Start()
    {
        // Charger le High Score sauvegardé
        highScore = PlayerPrefs.GetInt("HighScore", 0);

        // Initialiser les affichages des scores
        scoreText.text = currentScore.ToString("00000");
        UpdateHighScoreText();
    }

    private void Update()
    {
        // Incrémenter le score toutes les pointInterval secondes
        timer += Time.deltaTime;
        if (timer >= pointInterval)
        {
            AddScore(pointsPerInterval);
            timer = 0f;
        }
    }

    public void AddScore(int points)
    {
        // Ajouter des points au score actuel
        currentScore += points;
        scoreText.text = currentScore.ToString("00000");

        // Vérifier si le score est un multiple de 100
        if (currentScore % 100 == 0 && !isBlinking)
        {
            StartCoroutine(BlinkText(scoreText, 1f, 5)); // Clignote 5 fois pendant 1 seconde
        }
    }

    public void EndGame()
    {
        // Vérifier si le score actuel dépasse le high score
        if (currentScore > highScore)
        {
            highScore = currentScore;
            UpdateHighScoreText();

            // Sauvegarder le High Score
            PlayerPrefs.SetInt("HighScore", highScore);
            PlayerPrefs.Save(); // Force la sauvegarde immédiate
        }
    }

    public void ResetScores()
    {
        // Réinitialiser le score actuel
        currentScore = 0;
        scoreText.text = currentScore.ToString("00000");
    }

    private void UpdateHighScoreText()
    {
        // Mettre à jour le texte du high score
        highScoreText.text = $"HIGH SCORE  :  {highScore:00000}";
    }

    private IEnumerator BlinkText(TextMeshProUGUI text, float duration, int blinkCount)
    {
        isBlinking = true;
        float blinkInterval = duration / (blinkCount * 2);
        for (int i = 0; i < blinkCount; i++)
        {
            text.enabled = false; // Cache le texte
            yield return new WaitForSeconds(blinkInterval);
            text.enabled = true; // Affiche le texte
            yield return new WaitForSeconds(blinkInterval);
        }
        isBlinking = false;
    }
}
