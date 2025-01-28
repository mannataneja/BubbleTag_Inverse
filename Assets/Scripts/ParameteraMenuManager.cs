using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ParameteraMenuManager : MonoBehaviour
{
    [SerializeField] private CustomButton startBtn; 
    [SerializeField] private CustomButton easyBtn;
    [SerializeField] private CustomButton mediumBtn;
    [SerializeField] private CustomButton hardBtn;
    [SerializeField] private GameManager gameManager;
    [SerializeField] private GameObject Spawners;
    [SerializeField] private GameObject HUD;
    [SerializeField] private GameObject ParametersUI;

    public void StartGame_Btn()
    {
        Spawners.SetActive(true);
        HUD.SetActive(true);
        ParametersUI.SetActive(false);
        SetParametersFromGameDifficulty(GameData.difficulty);
    }

    public void Update()
    {
        //if (startBtn.IsButtonHighlighted())
        //{
        //    if (OVRInput.GetDown(OVRInput.Button.One))
        //    {
        //        GameData.difficulty = GameData.DifficultyEnum.Easy;
        //        StartGame_Btn();
        //    }
        //}

        if (easyBtn.IsButtonHighlighted())
        {
            if (OVRInput.GetDown(OVRInput.Button.One))
            {
                GameData.difficulty = GameData.DifficultyEnum.Easy;
                StartGame_Btn();
            }
        }

        if (mediumBtn.IsButtonHighlighted())
        {
            if (OVRInput.GetDown(OVRInput.Button.One))
            {
                GameData.difficulty = GameData.DifficultyEnum.Medium;
                StartGame_Btn();
            }
        }

        if (hardBtn.IsButtonHighlighted())
        {
            if (OVRInput.GetDown(OVRInput.Button.One))
            {
                GameData.difficulty = GameData.DifficultyEnum.Hard;
                StartGame_Btn();
            }
        }
    }

    public void SetParametersFromGameDifficulty(GameData.DifficultyEnum difficulty)
    {
        switch (difficulty)
        {
            case GameData.DifficultyEnum.Easy:
                SetEasyParameters();
                break;
            case GameData.DifficultyEnum.Medium:
                SetMediumParameters();
                break;
            case GameData.DifficultyEnum.Hard:
                SetHardParameters();
                break;
            default:
                SetEasyParameters();
                break;
        }
    }

    public void SetEasyParameters()
    {
        gameManager.timeBetweenSpawns = 5f;
        gameManager.DistractorBubbleSpawner.percentageThatFlicker = 10;
        gameManager.activeSpawners = 0;
        //gameManager.bubbleSpawners[0].bubble.maxHeight = 7f;
        //gameManager.bubbleSpawners[0].bubble.speed = 1f;
        GameData.maxHeight = 7f;
        GameData.bubbleSpeed = 1f; //added them in the gamedata as to set change in value across all the spawners
        gameManager.DistractorBubbleSpawner.flickerMaterial.SetFloat("_FlickerSpeed", 2);
    }

    public void SetMediumParameters()
    {
        gameManager.timeBetweenSpawns = 3f;
        gameManager.DistractorBubbleSpawner.percentageThatFlicker = 50;
        gameManager.activeSpawners = 0;
        //gameManager.bubbleSpawners[0].bubble.maxHeight = 7f;
        //gameManager.bubbleSpawners[0].bubble.speed = 3f;
        GameData.maxHeight = 7f;
        GameData.bubbleSpeed = 3f; 
        gameManager.DistractorBubbleSpawner.flickerMaterial.SetFloat("_FlickerSpeed", 20.0f);
    }

    public void SetHardParameters()
    {
        gameManager.timeBetweenSpawns = 2f;
        gameManager.DistractorBubbleSpawner.percentageThatFlicker = 100;
        gameManager.activeSpawners = 0;
        //gameManager.bubbleSpawners[0].bubble.maxHeight = 7f;
        //gameManager.bubbleSpawners[0].bubble.speed = 5f;
        GameData.maxHeight = 7f;
        GameData.bubbleSpeed = 5f;
        gameManager.DistractorBubbleSpawner.flickerMaterial.SetFloat("_FlickerSpeed", 200.0f);
    }
}
