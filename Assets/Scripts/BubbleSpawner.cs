using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class BubbleSpawner : MonoBehaviour
{
    public Bubbles bubble;
    public bool distractorBubble = false;
    public Transform[] spawnDistractorLocations = new Transform[4];


    public void SpawnBubble()
    {
        if (distractorBubble)
        {
            bubble.SetFlickerMaterial();
            Transform transform = SpawnDistractorBubble();
            Instantiate(bubble, transform);
        }
        else
        {
            Instantiate(bubble, transform);
        }
        Vector3 temp = bubble.transform.position;
        temp.x += Random.Range(-0.3f, 0.3f);
        bubble.transform.position = temp;
        temp = new Vector3(0, 0 , 0);   
    }

    public Transform SpawnDistractorBubble()
    {
        return spawnDistractorLocations[Random.Range(0, spawnDistractorLocations.Length)];
    }
}
