using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{
    [SerializeField] GameObject controlButtons;

    [SerializeField] GameObject continueButton;

    public void GameOver(bool porkieDied = false)
    {
        gameObject.SetActive(true);
        continueButton.SetActive(!porkieDied);
        controlButtons.SetActive(false);
    }

    public void ExitGame()
    {
        Application.Quit();
    }

    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void Continue()
    {
        controlButtons.SetActive(true);
        gameObject.SetActive(false);
    }
}
