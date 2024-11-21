using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Selection : MonoBehaviour
{
    public GameManager gameManager;
    public Material highlightMaterial;
    public Material dullMaterial;
    public Material redMaterial;

    private Material originalMaterialHighlight;
    private Material originalMaterialSelection;
    private Transform highlight;
    private Transform selection;
    private RaycastHit raycastHit;

    void Update()
    {
        // Highlight
        if (highlight != null)
        {
            highlight.GetComponent<MeshRenderer>().sharedMaterial = originalMaterialHighlight;
            highlight = null;
        }
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (!EventSystem.current.IsPointerOverGameObject() && Physics.Raycast(ray, out raycastHit)) //Make sure you have EventSystem in the hierarchy before using EventSystem
        {
            highlight = raycastHit.transform;
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
        }

        // Selection
        if (Input.GetMouseButtonDown(0) && !EventSystem.current.IsPointerOverGameObject())
        {

            if (highlight)
            {
                if (selection != null)
                {
                    selection.GetComponent<MeshRenderer>().material = originalMaterialSelection;
                }
                selection = raycastHit.transform;
                if (selection.GetComponent<MeshRenderer>().material != dullMaterial)
                {
                    originalMaterialSelection = originalMaterialHighlight;
                    selection.GetComponent<MeshRenderer>().material = dullMaterial;
                    if(selection.transform.parent.GetComponent<Bubbles>().animalTag == gameManager.currentAnimalTag)
                    {
                        //Correct anial selected, so we add to score and pop the bubble
                        Debug.Log("Correct Selection");
                       // gameManager.AddScore();
                        StartCoroutine(selection.transform.parent.GetComponent<Bubbles>().PopBubble());
                    }
                    else
                    {
                        Debug.Log("Wrong Selection");
                        AudioManager.instance.playSFX(AudioManager.instance.wrongSelection);
                        StartCoroutine(SetWrongSelectionColor());
                        GameData.playerWrong++;

                    }
                    
                }
                highlight = null;
            }
            else
            {
                if (selection)
                {
                    selection.GetComponent<MeshRenderer>().material = originalMaterialSelection;
                    selection = null;
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