using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameMenuManager : MonoBehaviour
{
    [SerializeField] private GameManager gameManager;
    [SerializeField] private CustomButton viewDataBtn;
    [SerializeField] private CustomButton CloseBtn;
    [SerializeField] private CustomButton exitButton;

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
            Debug.Log("in data");
            if (OVRInput.Get(OVRInput.Button.One))
            {
                gameManager.DisableDataCanvas();
            }
        }
        if(exitButton.IsButtonHighlighted())
        {
            if (OVRInput.Get(OVRInput.Button.One))
            {
                Application.Quit();
            }

        }

    }

}
