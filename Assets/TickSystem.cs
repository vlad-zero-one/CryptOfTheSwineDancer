using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TickSystem : MonoBehaviour
{
    public delegate void TickDelegate();

    public event TickDelegate TickEvent;

    public void Tick()
    {
        TickEvent?.Invoke();
    }
}
