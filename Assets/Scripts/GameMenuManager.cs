using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameMenuManager : MonoBehaviour
{
    [SerializeField] private GameManager gameManager;
    [SerializeField] private GameDataCollector gameDataCollector;
    [SerializeField] private CustomButton viewDataBtn;
    [SerializeField] private CustomButton CloseBtn;
    [SerializeField] private CustomButton exitButton;
    [SerializeField] private CustomButton backButton;

    bool quitCalled = false;
    public void Update()
    {
        if (viewDataBtn.IsButtonHighlighted())
        {
            if (OVRInput.Get(OVRInput.Button.One))
            {
                gameManager.EnableDataCanvas();
            }
        }
        
        if (CloseBtn.IsButtonHighlighted())
        {
            if (OVRInput.Get(OVRInput.Button.One))
            {
                gameManager.DisableDataCanvas();
            }
        }
      
        if(exitButton.IsButtonHighlighted())
        {
            if (OVRInput.Get(OVRInput.Button.One))
            {
                if (!quitCalled)
                {
                    gameDataCollector.LogGameData(GameData.playerName, GameData.playerScore, GameData.playerMissed, GameData.playerWrong);
                    quitCalled = true;
                    Application.Quit();
                }
            }
        }

        if (backButton.IsButtonHighlighted())
        {
            if (OVRInput.Get(OVRInput.Button.One))
            {
                SceneManager.LoadScene(0);
            }
        }
    }

}
