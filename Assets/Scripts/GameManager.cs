using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using TMPro;

public class GameManager : MonoBehaviour
{
    public float timeBetweenSpawns = 1f;
    public BubbleSpawner[] bubbleSpawners;
    public string[] animalTags;
    public int[] animalIndex = {0, 1, 2, 3, 4};

    public int currentAnimalIndex;
    public string currentAnimalTag;
    public bool currentAnimalExists = false;

    public TMP_Text currentAnimalText;
    public TMP_Text scoreText;

    bool started = false;

    public int score = 0;
    public int missed = 0;
    public int wrong = 0;

    public TMP_Text correctText;
    public TMP_Text missedText;
    public TMP_Text wrongText;

    public Slider progressBarImage;
    public ParticleSystem progressBarParticleSystem;
    public UnityEvent onAddScore;

    public Canvas dataCanvas;
    public Canvas HUD;

    // Start is called before the first frame update
    void Start()
    {
        onAddScore.AddListener(UpdateProgressBar);

        dataCanvas.enabled = false;
        ChangeCurrentAnimal();
        CallSpawners();
    }

    void UpdateProgressBar()
    {
        if (score <= 100)
        {
            progressBarImage.value = score*10;
            progressBarParticleSystem.Play();
            // Debug.Log(score);
        }

    }
    //Updates the current animal to be selected
    public void ChangeCurrentAnimal()
    {
        currentAnimalIndex = Random.Range(0, animalTags.Length);
        currentAnimalTag = animalTags[currentAnimalIndex];

        Debug.Log("Current Animal : " + currentAnimalTag);

        currentAnimalText.text = currentAnimalTag;
    }
    //Each spawner has animal index which decides which animal to spawn
    //This function randomizes the animal indexes
    public void AnimalIndexShuffle()
    {
        for (int i = 0; i < animalIndex.Length; i++)
        {
            int tmp = animalIndex[i];
            int r = Random.Range(i, animalIndex.Length);
            animalIndex[i] = animalIndex[r];
            animalIndex[r] = tmp;
        }
    }
    //Assign the shuffled animal indexes to each spawner and call the spawner
    public void CallSpawners()
    {
        StartCoroutine(SpawnAnimals());
    }
    public IEnumerator SpawnAnimals()
    {
        while(true)
        {
            AnimalIndexShuffle();
            for (int i = 0; i < bubbleSpawners.Length; i++)
            {
                if (animalIndex[i] == currentAnimalIndex && !currentAnimalExists) //there should be only one current animal per game loop
                {
                    currentAnimalExists = true;
                }
                bubbleSpawners[i].bubble.animalIndex = animalIndex[i];
                bubbleSpawners[i].SpawnBubble(); //Only spawn the bubble in BubbleSpawner. The script is written so that each animal is spawned as a child within each bubble. The game logic checks for child of selected bubble. 
                yield return new WaitForSeconds(timeBetweenSpawns);

                correctText.text = "Correct : " + score.ToString();
                missedText.text = "Missed : " + missed.ToString();
                wrongText.text = "Wrong : " + wrong.ToString();
            }
        }
    }
    //Increase score if correct animal is selected
    public void AddScore()
    {
       
        score++;
        scoreText.text = "Score : " + score.ToString();
        onAddScore?.Invoke();
        ChangeCurrentAnimal();
    }
    public void EnableDataCanvas()
    {
        Time.timeScale = 0;
        dataCanvas.enabled = true;
        HUD.enabled = false;
        
    }
    public void DisableDataCanvas()
    {
        dataCanvas.enabled = false;
        HUD.enabled = true;
        Time.timeScale = 1;
    }
}
