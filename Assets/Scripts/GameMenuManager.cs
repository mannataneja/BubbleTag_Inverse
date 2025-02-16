using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameMenuManager : MonoBehaviour
{
    [SerializeField] private GameManager gameManager;
    [SerializeField] private GameDataCollector gameDataCollector;
    [SerializeField] private FirebaseGameDataCollection firebaseGameDataCollection;
    [SerializeField] private CustomButton viewDataBtn;
    [SerializeField] private CustomButton CloseBtn;
    [SerializeField] private CustomButton exitButton;
    [SerializeField] private CustomButton backButton;

    //bool quitCalled = false;

    public void Update()
    {
        if (viewDataBtn.IsButtonHighlighted())
        {
            if (OVRInput.GetDown(OVRInput.Button.One))
            {
                gameManager.EnableDataCanvas();
            }
        }
        
        if (CloseBtn.IsButtonHighlighted())
        {
            if (OVRInput.GetDown(OVRInput.Button.One))
            {
                gameManager.DisableDataCanvas();
            }
        }
      
        if(exitButton.IsButtonHighlighted())
        {
            if (OVRInput.GetDown(OVRInput.Button.One))
            {
                gameDataCollector.LogGameData(GameData.playerName, GameData.playerScore, GameData.playerMissed, GameData.playerWrong, GameData.reactionTimes);
                
                //Log into Firebase
                firebaseGameDataCollection.AddDataToFirebase(GameData.difficulty.ToString(), GameData.playerScore, GameData.playerWrong, GameData.playerMissed, GameData.reactionTimes);
                
                Application.Quit();
            }
        }

        if (backButton.IsButtonHighlighted())
        {
            if (OVRInput.GetDown(OVRInput.Button.One))
            {
                SceneManager.LoadScene(0);
            }
        }
    }

}
