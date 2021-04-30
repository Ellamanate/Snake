using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;


public static class Events
{
    public static UnityEvent OnEat = new UnityEvent();
    public static UnityEvent OnCollectCrystal = new UnityEvent();
    public static UnityEvent OnEndGame = new UnityEvent();
    public static UnityEvent OnRestart = new UnityEvent();
    public static UnityEvent OnGameWin = new UnityEvent();
}
