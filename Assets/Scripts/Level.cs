using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Level
{
    private static int currentLevel = 0;

    public static void SetCurrentLevel(int _level)
    {
        currentLevel = _level;
    }

    public static int GetCurrentLevel()
    {
        return currentLevel;
    }
}
