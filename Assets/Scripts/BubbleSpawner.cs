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
        Vector3 temp = bubble.transform.position;
        temp.x += Random.Range(-0.3f, 0.3f);
        bubble.transform.position = temp;
        temp = new Vector3(0, 0 , 0);   
    }
}
