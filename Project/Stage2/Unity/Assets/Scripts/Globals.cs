using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Globals
{
    public static bool InGame;

    public static void TransitionToInGame()
    {
        InGame = true;
    }

    public static void BackToMenu()
    {
        InGame = false;
    }
}
