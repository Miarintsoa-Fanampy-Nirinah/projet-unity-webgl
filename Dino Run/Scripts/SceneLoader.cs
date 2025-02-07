using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    // Nom de la scène à charger
    [SerializeField] private string targetScene;

    void Update()
    {
        // Détection du clic gauche de la souris ou de la touche espace
        if (Input.GetMouseButtonDown(0) || Input.GetKeyDown(KeyCode.Space))
        {
            LoadTargetScene();
        }

        // Détection tactile (pour mobile)
        if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began)
        {
            LoadTargetScene();
        }
    }

    // Fonction pour charger la scène
    private void LoadTargetScene()
    {
        if (!string.IsNullOrEmpty(targetScene))
        {
            SceneManager.LoadScene(targetScene);
        }
        else
        {
            Debug.LogWarning("Nom de la scène cible non défini !");
        }
    }
}
