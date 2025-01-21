using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DistractorBubbleSpawner : MonoBehaviour
{
    public DistractorBubble distractorBubble;
    public Transform[] spawnDistractorLocations = new Transform[4];

    [SerializeField] GameManager gameManager;

    private void Start()
    {
        distractorBubble.gameManager = gameManager;
        spawnDistractorBubble();
    }

    public void spawnDistractorBubble()
    {
        Debug.Log("sntg");
        Instantiate(distractorBubble, spawnDistractorLocations[Random.Range(0, (spawnDistractorLocations.Length - 1))]);
        Vector3 temp = distractorBubble.transform.position;
        temp.x += Random.Range(-0.3f, 0.3f);
        distractorBubble.transform.position = temp;
        temp = new Vector3(0, 0, 0);
    }
}
