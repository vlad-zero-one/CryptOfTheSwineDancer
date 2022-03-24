using UnityEngine;

public class WinChecker : MonoBehaviour
{
    [SerializeField] Menu menu;

    public void CheckWin()
    {
        if (transform.childCount == 1)
        {
            menu.GameOver(isWin: true);
        }
    }
}
