#if UNITY_EDITOR

using UnityEngine;
using UnityEngine.UI;

public class ControlPigWithKeys : MonoBehaviour
{
    [SerializeField] private Button upButton;
    [SerializeField] private Button downButton;
    [SerializeField] private Button rightButton;
    [SerializeField] private Button leftButton;
    [SerializeField] private Button bombButton;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.W))
            upButton.onClick.Invoke();
        else if (Input.GetKeyDown(KeyCode.S))
            downButton.onClick.Invoke();
        else if (Input.GetKeyDown(KeyCode.A))
            leftButton.onClick.Invoke();
        else if (Input.GetKeyDown(KeyCode.D))
            rightButton.onClick.Invoke();
        else if (Input.GetKeyDown(KeyCode.Space))
            bombButton.onClick.Invoke();
    }
}

#endif