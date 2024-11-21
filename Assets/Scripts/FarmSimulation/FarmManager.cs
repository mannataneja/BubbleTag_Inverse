using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FarmManager : MonoBehaviour
{
    public class AnimalScore
    {
        public string animalName;
        public int score;
    }

    public static FarmManager instance;

    public List<GameObject> animalPrefabs = new List<GameObject>(); // Animal Prefabs
    public List<GameObject> buildingPrefabs = new List<GameObject>(); // Building Prefabs

    public Transform farmParentObject;
    public float generationRadius = 25f; // Generate animals and buildings within this radius
    public float objectRadius = 3f; // The radius of buildings. Buildings cannot overlap.

    public GameObject animalCreationEffect;
    public GameObject animalUpgradeEffect;
    public GameObject buildingCreationEffect;

    private List<FarmAnimal> animals = new List<FarmAnimal>(); // Existing Animals
    private List<FarmBuilding> buildings = new List<FarmBuilding>(); // Existing Buildings
    private List<AnimalScore> animalScores = new List<AnimalScore>(); // Animal Scores

    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(this.gameObject);
        }
    }

    void Start()
    {
        // Initialize scores for all animal types
        foreach (GameObject prefab in animalPrefabs)
        {
            var farmAnimal = prefab.GetComponent<FarmAnimal>();
            if (farmAnimal != null)
            {
                animalScores.Add(new AnimalScore { animalName = farmAnimal.animalName, score = 0 });
            }
        }
    }

    void Update()
    {
        // Example usage:
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            AddAnimal("Chicken");
        }
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            AddAnimal("Cow");
        }
        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            AddAnimal("Dog");
        }
        if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            AddAnimal("Duck");
        }
        if (Input.GetKeyDown(KeyCode.Alpha5))
        {
            AddAnimal("Horse");
        }
        if (Input.GetKeyDown(KeyCode.Alpha6))
        {
            AddAnimal("Pig");
        }
        if (Input.GetKeyDown(KeyCode.Alpha7))
        {
            AddAnimal("Sheep");
        }
    }

    public void AddAnimal(string animalName)
    {
        // Generate a new level 1 animal
        GenerateNewAnimal(animalName);

        // Perform recursive merging checks
        RecursiveMergeAnimals(animalName);

        // Increase the score for the corresponding animal type
        AnimalScore score = animalScores.Find(s => s.animalName == animalName);
        if (score != null)
        {
            score.score++;

            // Check if a building can be placed
            CheckAndGenerateBuilding(animalName, score.score);
        }
    }

    private void RecursiveMergeAnimals(string animalName)
    {
        FarmAnimal lastRemainingAnimal = null;

        // Continue merging until no further merges can occur
        while (true)
        {
            FarmAnimal mergedAnimal = CheckAndMergeAnimals(animalName);
            if (mergedAnimal == null)
            {
                break; // Exit loop if no merge occurred
            }
            lastRemainingAnimal = mergedAnimal; // Update the last remaining animal after each merge
        }

        // Trigger the upgrade effect only on the final remaining animal
        if (lastRemainingAnimal != null)
        {
            Instantiate(animalUpgradeEffect, lastRemainingAnimal.transform.position, lastRemainingAnimal.transform.rotation);
        }
    }


    public FarmAnimal CheckAndMergeAnimals(string animalName)
    {
        // Find all animals with the same name
        var sameAnimals = animals.FindAll(a => a.animalName == animalName);

        foreach (var animal in sameAnimals)
        {
            int level = animal.level;

            // Skip animals that have already reached the maximum level
            if (level >= animal.maxLevel)
            {
                continue;
            }

            int requiredToMerge = animal.mergingRequirement;

            // Find animals with the same level
            var sameLevelAnimals = sameAnimals.FindAll(a => a.level == level);

            // Check if there are enough animals to perform a merge
            if (sameLevelAnimals.Count >= requiredToMerge)
            {
                // Remove extra animals required for the merge
                for (int i = 1; i < requiredToMerge; i++)
                {
                    Destroy(sameLevelAnimals[i].gameObject);
                    animals.Remove(sameLevelAnimals[i]);
                }

                // Upgrade the remaining animal
                int newLevel = level + 1;
                var mergedAnimal = sameLevelAnimals[0];
                mergedAnimal.level = newLevel;
                mergedAnimal.transform.localScale = Vector3.one * (1 + (newLevel - 1) * mergedAnimal.sizeScale);

                // Return the upgraded animal to indicate a merge occurred
                return mergedAnimal;
            }
        }

        // Return null if no merge occurred
        return null;
    }


    public void GenerateNewAnimal(string animalName)
    {
        // Find the corresponding prefab for the animal
        var animalPrefab = animalPrefabs.Find(prefab => prefab.GetComponent<FarmAnimal>().animalName == animalName);
        if (animalPrefab != null)
        {
            // Instantiate the animal
            var newAnimalObject = Instantiate(animalPrefab, GetRandomPosition(), Quaternion.Euler(0, Random.Range(0, 360), 0), farmParentObject);
            var newAnimal = newAnimalObject.GetComponent<FarmAnimal>();
            newAnimal.level = 1;

            // Check if the new animal will trigger a merge
            if (!CanTriggerImmediateMerge(newAnimal))
            {
                // Play the creation effect only if no immediate merge occurs
                Instantiate(animalCreationEffect, newAnimalObject.transform.position, newAnimalObject.transform.rotation);
            }

            // Add the new animal to the list
            animals.Add(newAnimal);
        }
    }

    // Check if the newly generated animal will trigger a merge
    private bool CanTriggerImmediateMerge(FarmAnimal newAnimal)
    {
        var sameAnimals = animals.FindAll(a => a.animalName == newAnimal.animalName && a.level == newAnimal.level);
        return sameAnimals.Count + 1 >= newAnimal.mergingRequirement;
    }

    public void CheckAndGenerateBuilding(string animalName, int currentScore)
    {
        // Find the corresponding building prefab
        var buildingPrefab = buildingPrefabs.Find(prefab => prefab.GetComponent<FarmBuilding>().animalName == animalName); ;
        if (buildingPrefab == null) return;

        // Check if the current score meets the required points for this building
        var buildingComponent = buildingPrefab.GetComponent<FarmBuilding>();
        if (currentScore % buildingComponent.requiredScores == 0)
        {
            // Generate a building
            var newBuildingObject = Instantiate(buildingPrefab, GetRandomPosition(), Quaternion.Euler(0, Random.Range(0, 360), 0), farmParentObject);
            var newBuilding = newBuildingObject.GetComponent<FarmBuilding>();
            Instantiate(buildingCreationEffect, newBuildingObject.transform.position, newBuildingObject.transform.rotation);
            buildings.Add(newBuilding);

            Debug.Log($"Built a {newBuilding.buildingName} for {animalName}!");
        }
    }

    private Vector3 GetRandomPosition()
    {
        if (farmParentObject == null)
        {
            Debug.LogError("Farm Parent Object is not assigned!");
            return Vector3.zero;
        }

        // Generate a random offset within the specified radius
        Vector3 randomOffset = Random.insideUnitSphere * generationRadius;
        randomOffset.y = 0; // Keep the position on the ground (flat surface)

        // Add the offset to the farmParentObject's position
        Vector3 position = farmParentObject.position + randomOffset;

        // Ensure no overlap with existing buildings
        foreach (var building in buildings)
        {
            if (Vector3.Distance(position, building.transform.position) < objectRadius)
            {
                // Retry if overlap detected
                return GetRandomPosition();
            }
        }

        return position;
    }

    public List<FarmBuilding> GetBuildingList(string animalName)
    {
        List<FarmBuilding> buildingList = new List<FarmBuilding>();

        foreach(var building in buildings)
        {
            if(building.animalName == animalName)
            {
                buildingList.Add(building);
            }
        }

        return buildingList;
    }
}
