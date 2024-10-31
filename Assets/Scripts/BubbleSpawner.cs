using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class BubbleSpawner : MonoBehaviour
{
    //public GameObject bubble;
    public Bubbles bubble;
    public float minTime;
    public float maxTime;
    float randomTime;

    public float innerRadius;
    public float outerRadius;
    

    bool start = false;

    //SpawnAnimals new bubble
    public void SpawnBubble()
    {
        Instantiate(bubble, transform);
    }
}
