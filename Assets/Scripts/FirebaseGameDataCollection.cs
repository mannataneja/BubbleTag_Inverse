using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Firebase.Firestore;
using Firebase.Extensions;
using Firebase;

public class FirebaseGameDataCollection : MonoBehaviour
{
    FirebaseFirestore db;
    string sessionId;

    async void Start()
    {
        // Initialize Firebase
        var dependencyStatus = await FirebaseApp.CheckAndFixDependenciesAsync();
        if (dependencyStatus == DependencyStatus.Available)
        {
            db = FirebaseFirestore.DefaultInstance;
            Debug.Log("Database Instance =" + db);
        }
        else
        {
            Debug.LogError("Could not resolve all Firebase dependencies: " + dependencyStatus);
        }

        sessionId = System.DateTime.Now.ToString("yyyy-MM-dd_HH-mm-ss");
    }

    public void AddDataToFirebase(string difficulty, int playerScore, int playerWrong, int playerMissed, List<float> reactionTimes)
    {
        FirebaseData firebaseData = new FirebaseData
        {
            Difficulty = difficulty,
            sessionID = sessionId,
            playerScore = playerScore,
            playerWrong = playerWrong,
            playerMissed = playerMissed,
            reactionTimes = reactionTimes
        };
        DocumentReference playerDataRef = db.Collection("PlayerData").Document(sessionId);

        playerDataRef.SetAsync(firebaseData).ContinueWithOnMainThread(task =>
        {
            Debug.Log("Data Logged on Firebase");
        });
    }
}
