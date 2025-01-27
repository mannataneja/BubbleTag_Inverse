using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;
using static UnityEngine.Rendering.DebugUI;


public class StartMenuManager : MonoBehaviour
{
    [SerializeField] private CustomButton startBtn;
    [SerializeField] private CustomButton startTutorialBtn;
    //[SerializeField] private TMP_InputField nameInput;
    //[SerializeField] private Color EmptyNameFieldHighlight;

    [SerializeField] private GameManager gameManager;
    [SerializeField] private DistractorBubbleSpawner distractorBubbleSpawner;
    [SerializeField] private BubbleSpawner bubbleSpawner;

    public void StartGame_Btn()
    {
        SceneManager.LoadScene(1);
    }

    public void StartTutorial_Btn()
    {
        SceneManager.LoadScene(2);
    }

    public void Update()
    {

        //if (!string.IsNullOrEmpty(nameInput.text))
        //    GameData.playerName = nameInput.text;
        //    startBtn.interactable = true;
            
        if (startBtn.IsButtonHighlighted())
        {
            if (OVRInput.GetDown(OVRInput.Button.One))
            {
                StartGame_Btn();
            }
        }
            
        if (startTutorialBtn.IsButtonHighlighted())
        {
            if (OVRInput.GetDown(OVRInput.Button.One))
            {
                StartTutorial_Btn();
            }
        }
    }

    //private void changeBtnHighlightColor()
    //{
    //    ColorBlock colorBlock = startBtn.colors;
    //    colorBlock.highlightedColor = EmptyNameFieldHighlight;
    //    startBtn.colors = colorBlock;
    //}
}
