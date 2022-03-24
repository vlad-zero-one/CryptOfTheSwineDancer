using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{
    [SerializeField] GameObject controlButtons;

    [SerializeField] GameObject continueButton;

    [SerializeField] GameObject winObject;
    [SerializeField] GameObject loseObject;

    public void GameOver(bool porkieDied = false, bool isWin = false)
    {
        // чтобы не вызывать функцию в случае нормального выполнение OnDestroy
        if (!this) return;

        loseObject.SetActive(!isWin);
        winObject.SetActive(isWin);
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
