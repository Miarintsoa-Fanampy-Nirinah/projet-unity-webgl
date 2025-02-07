using UnityEngine;
using UnityEngine.UI;

public class OpenWebsite : MonoBehaviour
{
    // Référence au bouton
    public Button targetButton;

    // URL du site web
    [SerializeField] private string url = "https://miarintsoa-fanampy-nirinah.github.io/"; // Exemple de site web

    void Start()
    {
        // Vérifiez si le bouton est assigné
        if (targetButton != null)
        {
            // Ajoutez un listener au bouton pour ouvrir le site web
            targetButton.onClick.AddListener(() => OpenURL());
        }
        else
        {
            Debug.LogError("Aucun bouton n'a été assigné dans l'inspecteur !");
        }
    }

    // Fonction pour ouvrir l'URL
    private void OpenURL()
    {
        Application.OpenURL(url);
        Debug.Log("Ouverture du site : " + url);
    }
}