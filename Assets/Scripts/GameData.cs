using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Collections.Generic;


public static class GameData 
{
    public static int playerScore = 0;
    public static int playerWrong = 0;
    public static int playerMissed = 0;
    public static string playerName = "Player";
    //public static float reactionTime;
    public static List<float> reactionTimes = new List<float>(); //Store all the reaction times of the player on correct hits



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


