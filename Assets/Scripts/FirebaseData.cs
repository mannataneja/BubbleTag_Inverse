using Firebase.Firestore;

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
}
