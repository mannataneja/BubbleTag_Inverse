using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class BubbleSpawner : MonoBehaviour
{
    public Bubbles bubble;

    public void SpawnBubble()
    {
        Instantiate(bubble, transform);
    }
}
