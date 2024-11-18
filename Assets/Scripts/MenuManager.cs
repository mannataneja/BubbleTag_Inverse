using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuManager : MonoBehaviour
{
    [SerializeField] private CustomButton startBtn;

    public void StartGame_Btn()
    {
        SceneManager.LoadScene(1);
    }

    public void Update()
    {
       if(startBtn.IsButtonHighlighted())
       {
            Debug.Log("in");
            if (OVRInput.Get(OVRInput.RawButton.RIndexTrigger))
            {
                Debug.Log("true");
                StartGame_Btn();
            }
       }     
    }
}
