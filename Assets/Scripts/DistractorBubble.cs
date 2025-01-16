using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DistractorBubble : MonoBehaviour
{
    public GameObject[] animals;
    public float distractorSpeed = 5f;
    float distractorMaxHeight = 3f;
    GameObject animal;

    private void Start()
    {
       // Instantiate(animals[Random.Range(0, (animals.Length - 1))], transform);
        animal = transform.GetChild(transform.childCount - 1).gameObject;
        animal.GetComponent<Rigidbody>().useGravity = true;
    }

    void Update()
    {
        transform.Translate(Vector3.up * distractorSpeed * Time.deltaTime);
         
        if (transform.position.y >= distractorMaxHeight) 
        {
            GameObject.FindAnyObjectByType<DistractorBubbleSpawner>().spawnDistractorBubble();
            Destroy(gameObject);
        }
    }
}
