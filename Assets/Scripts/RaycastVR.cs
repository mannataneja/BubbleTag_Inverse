using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RaycastVR : MonoBehaviour
{

    public GameManager gameManager;
    public Material highlightMaterial;
    public Material dullMaterial;
    public Material redMaterial;

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
                    if (selection.GetComponent<MeshRenderer>().material != dullMaterial)
                    {
                        originalMaterialSelection = originalMaterialHighlight;
                        selection.GetComponent<MeshRenderer>().material = dullMaterial;
                        Debug.Log(selection.transform.parent.GetComponent<Bubbles>().animalTag);
                        if (selection.transform.parent.GetComponent<Bubbles>().animalTag == gameManager.currentAnimalTag)
                        {
                            StartCoroutine(selection.transform.parent.GetComponent<Bubbles>().PopBubble());
                        }
                        else
                        {
                            Debug.Log("Wrong Selection");
                            AudioManager.instance.playSFX(AudioManager.instance.wrongSelection);
                            //gameManager.wrong++;
                            StartCoroutine(SetWrongSelectionColor());
                            GameData.playerWrong++;
                        }

                    }
                    highlight = null;
                }
                else
                {
                   // AudioManager.instance.playSFX(AudioManager.instance.emptySelection);
                    if (selection)
                    {
                        selection.GetComponent<MeshRenderer>().material = originalMaterialSelection;
                        selection = null;
                    }
                }
            }
        }   
    }
    public IEnumerator SetWrongSelectionColor()
    {
        originalMaterialHighlight = highlight.GetComponent<MeshRenderer>().material;
        selection.GetComponent<MeshRenderer>().material = redMaterial;
        yield return new WaitForSeconds(1f);
        selection.GetComponent<MeshRenderer>().material = originalMaterialHighlight;
    }
}
