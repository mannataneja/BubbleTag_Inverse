using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Animal : MonoBehaviour
{
    GameManager manager;

    string animalTag;

    bool rotate = false;
    bool floating = false;
    float floatingSpeed = 5f;

    public bool selectable = false;

    public bool isCurrentAnimal = false;
    // Start is called before the first frame update
    void Start()
    {
        animalTag = gameObject.tag;
        manager = GameObject.FindAnyObjectByType<GameManager>();
        CheckForCurrentAnimal();
        StartCoroutine(PlayAnimalSound());
    }

    // Update is called once per frame
    void Update()
    {
        //Make animal float away
        if (floating)
        {
            transform.Translate(Vector3.up * floatingSpeed * Time.deltaTime);
        }
        //Make animal rotate when it floats away
        if (rotate)
        {
            Debug.Log("roatating");
            transform.Rotate(1, 1, 1);
        }
    }
    //Check for animal colliding with the ground when it falls after bubble pop
    //Make animal spin and float away before destroying
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Ground")
        {
            Debug.Log("Ground Collision");
            gameObject.GetComponent<Rigidbody>().useGravity = false;
            StopAnimalSound();
            rotate = true;
            floating = true;
            Destroy(gameObject, 2f);
        }
    }
    public void CheckForCurrentAnimal()
    {
        if (animalTag == manager.currentAnimalTag)
        {
            isCurrentAnimal = true; 
        }
    }
    //Play sound of current animal
    public IEnumerator PlayAnimalSound()
    {
        yield return new WaitForSeconds(3f);
        selectable = true;
        gameObject.transform.parent.GetComponent<Bubbles>().SetCurrentBubbleMaterial();
        gameObject.GetComponent<AudioSource>().Play();
        yield return new WaitForSeconds(2.5f);
        selectable = false;
        gameObject.transform.parent.GetComponent<Bubbles>().SetDullBubbleMaterial();
        gameObject.GetComponent<AudioSource>().Stop();
    }
    public void StopAnimalSound()
    {
        gameObject.GetComponent<AudioSource>().Stop();
    }
}
