using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class GameDataCollector : MonoBehaviour
{
    private string filePath;

    void Start()
    {
        filePath = Path.Combine(Application.persistentDataPath, "BubbleGameData.csv");

        if (!File.Exists(filePath))
        {
            string headers = "SessionID,PlayerName,Score,Missed,Wrong";
            
            WriteCSVHeader(headers);
        }
    }

    void WriteCSVHeader(string headers)
    {
        //using (StreamWriter writer = new StreamWriter(filePath, true))
        //{
        //    writer.WriteLine(headers);
        //}
        File.WriteAllText(filePath, headers + "\n");
    }


    public void LogGameData(string playerName, int score, int missed, int wrong, List<float> reactionTimes)
    {
        string sessionID = System.DateTime.Now.ToString("yyyy-MM-dd_HH-mm-ss"); // Unique session ID

        string data = $"{sessionID},{playerName}, {score}, {missed}, {wrong}, {reactionTimes}";

        //using (StreamWriter writer = new StreamWriter(filePath, true))
        //{
        //    writer.WriteLine(data);
        //}

        File.AppendAllText(filePath, data + "\n");

        Debug.Log("Game data saved.");
    }

}
