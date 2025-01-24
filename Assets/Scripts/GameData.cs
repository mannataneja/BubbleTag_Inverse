using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class GameData 
{
    public static int playerScore = 0;
    public static int playerWrong = 0;
    public static int playerMissed = 0;
    public static string playerName = "Player";
    public static float reactionTime;

    //private static bool isTimer;
    private static float startTime;
    private static float endTime;

    public static void StartTimer()
    {
        startTime = Time.time;
    }

    public static void EndTimer()
    {
        endTime = Time.time;
        reactionTime = endTime - startTime;
        Debug.Log("Reaction Time - " + reactionTime);
    }
}


