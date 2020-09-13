using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Level
{
    private static int currentLevel = 1;
    private static int score = 0;

    public static void SetCurrentLevel(int _level)
    {
        currentLevel = _level;
    }

    public static void SetScore(int _score)
    {
        score = _score;
    }

    public static int GetCurrentLevel()
    {
        return currentLevel;
    }

    public static int GetScore()
    {
        return score;
    }
}
