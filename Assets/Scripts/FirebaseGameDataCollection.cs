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

    private void Update()
    {
        if (Input.GetKeyDown("space"))
        {
            AddDataToFirebase(GameData.playerScore, GameData.playerWrong, GameData.playerMissed);
        }
    }

    public void AddDataToFirebase(int playerScore, int playerWrong, int playerMissed)
    {
        FirebaseData firebaseData = new FirebaseData
        {
            sessionID = sessionId,
            playerScore = playerScore,
            playerWrong = playerWrong,
            playerMissed = playerMissed
        };
        DocumentReference playerDataRef = db.Collection("PlayerData").Document(sessionId);

        playerDataRef.SetAsync(firebaseData).ContinueWithOnMainThread(task =>
        {
            Debug.Log("Data Logged on Firebase");
        });
    }
}
