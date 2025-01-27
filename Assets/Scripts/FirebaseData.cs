using Firebase.Firestore;
using System.Collections.Generic;

[FirestoreData]
public struct FirebaseData
{
    [FirestoreProperty]
    public string sessionID { get; set; }

    [FirestoreProperty]
    public int playerScore { get; set; }

    [FirestoreProperty]
    public int playerWrong { get; set; }

    [FirestoreProperty]
    public int playerMissed { get; set; }

    [FirestoreProperty]
    public List<float> reactionTimes { get; set; }
}
