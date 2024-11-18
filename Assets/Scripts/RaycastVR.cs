using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RaycastVR : MonoBehaviour
{

    public GameManager gameManager;
    public Material highlightMaterial;
    public Material selectionMaterial;

    private Material originalMaterialHighlight;
    private Material originalMaterialSelection;
    private Transform highlight;
    private Transform selection;
    //private RaycastHit raycastHit;

    void Update()
    {
        RaycastHit hit;

        if (highlight != null)
        {
            highlight.GetComponent<MeshRenderer>().sharedMaterial = originalMaterialHighlight;
            highlight = null;
        }
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(transform.position, transform.forward, out hit)) //Make sure you have EventSystem in the hierarchy before using EventSystem
        {
           
            highlight = hit.transform;
            if (highlight.CompareTag("Selectable") && highlight != selection)
            {
                if (highlight.GetComponent<MeshRenderer>().material != highlightMaterial)
                {
                    originalMaterialHighlight = highlight.GetComponent<MeshRenderer>().material;
                    highlight.GetComponent<MeshRenderer>().material = highlightMaterial;
                }
            }
            else
            {
                highlight = null;
            }

            if (OVRInput.Get(OVRInput.RawButton.RIndexTrigger))
            {
                Debug.Log(hit);

                if (highlight)
                {
                    if (selection != null)
                    {
                        selection.GetComponent<MeshRenderer>().material = originalMaterialSelection;
                    }
                    selection = hit.transform;
                    if (selection.GetComponent<MeshRenderer>().material != selectionMaterial)
                    {
                        originalMaterialSelection = originalMaterialHighlight;
                        selection.GetComponent<MeshRenderer>().material = selectionMaterial;
                        Debug.Log(selection.transform.parent.GetComponent<Bubbles>().animalTag);
                        if (selection.transform.parent.GetComponent<Bubbles>().animalTag == gameManager.currentAnimalTag)
                        {
                           // gameManager.AddScore();
                            StartCoroutine(selection.transform.parent.GetComponent<Bubbles>().PopBubble());
                        }
                        else
                        {
                            Debug.Log("Wrong Selection");
                            AudioManager.instance.playSFX(AudioManager.instance.wrongSelection);
                            gameManager.wrong++;
                        }

                    }
                    highlight = null;
                }
                else
                {
                    AudioManager.instance.playSFX(AudioManager.instance.emptySelection);
                    if (selection)
                    {
                        selection.GetComponent<MeshRenderer>().material = originalMaterialSelection;
                        selection = null;
                    }
                }
            }
        }


        //if (Physics.Raycast(transform.position, transform.forward, out hit))
        //{
        //    if (OVRInput.Get(OVRInput.RawButton.RIndexTrigger))
        //    {
        //        selection = hit.transform;
        //        if (selection.transform.parent.GetComponent<Bubbles>().animalTag == gameManager.currentAnimalTag)
        //        {
        //            gameManager.AddScore();
        //            StartCoroutine(selection.transform.parent.GetComponent<Bubbles>().PopBubble());
        //            Debug.Log("hit");
        //        }
        //        else
        //        {
        //            Debug.Log("Wrong Selection");
        //        }

        //    }
        //}
            
    }
}
