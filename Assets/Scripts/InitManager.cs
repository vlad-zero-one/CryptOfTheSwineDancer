using System.Collections.Generic;
using UnityEngine;

public class InitManager : MonoBehaviour
{
    [SerializeField] private List<MonoBehaviour> objectsToInit;

    private void Start()
    {
        foreach(var obj in objectsToInit)
        {
            if (obj is IInitable)
                ((IInitable)obj).Init();
        }
    }
}

public interface IInitable
{
    void Init();
}
