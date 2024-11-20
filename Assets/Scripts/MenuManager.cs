using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;
using static UnityEngine.Rendering.DebugUI;

public class MenuManager : MonoBehaviour
{
    [SerializeField] private CustomButton startBtn;
    [SerializeField] private TMP_InputField nameInput;

    public void StartGame_Btn()
    {
        SceneManager.LoadScene(1);
    }

    public void Update()
    {
        if (!string.IsNullOrEmpty(nameInput.text))
        {
            startBtn.interactable = true;
            if (startBtn.IsButtonHighlighted())
            {
                Debug.Log("in");
                if (OVRInput.Get(OVRInput.Button.One))
                {
                    Debug.Log("true");
                    StartGame_Btn();
                }
            }
        }
    }
}
