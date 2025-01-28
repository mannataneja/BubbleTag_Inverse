using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public static class GameData 
{
    public static int playerScore = 0;
    public static int playerWrong = 0;
    public static int playerMissed = 0;
    public static string playerName = "Player";
    //public static float reactionTime;
    public static List<float> reactionTimes = new List<float>(); //Store all the reaction times of the player on correct hits
    public static DifficultyEnum difficulty = DifficultyEnum.Easy;
    public enum DifficultyEnum
    {
        Easy,
        Medium,
        Hard
    }

    [Header("Bubble Parameters")]
    public static float maxHeight = 7f;
    public static float bubbleSpeed = 3f;

    private static float startTime;
    private static float endTime;

    public static void StartTimer()
    {
        startTime = Time.time;
    }

    public static void EndTimer()
    {
        endTime = Time.time;
        reactionTimes.Add(endTime - startTime);
        //reactionTime = endTime - startTime;
        Debug.Log("Reaction Time - " + reactionTimes[reactionTimes.Count-1]);
    }
}


