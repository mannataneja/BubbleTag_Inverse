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

    public List<GameObject> animalPrefabs = new List<GameObject>(); // Animal Prefabs
    public List<GameObject> buildingPrefabs = new List<GameObject>(); // Building Prefabs

    public Transform farmParentObject;
    public float generationRadius = 10f; // Generate animals and buildings within this radius
    public float objectRadius = 2f; // The radius of buildings. Buildings cannot overlap.

    private List<FarmAnimal> animals = new List<FarmAnimal>(); // Existing Animals
    private List<FarmBuilding> buildings = new List<FarmBuilding>(); // Existing Buildings
    private List<AnimalScore> animalScores = new List<AnimalScore>(); // Animal Scores

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
        if (Input.GetKeyDown(KeyCode.C))
        {
            AddAnimal("Chicken");
        }
        if (Input.GetKeyDown(KeyCode.D))
        {
            AddAnimal("Duck");
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
        bool merged = true;

        // Perform merging until no more merges are possible
        while (merged)
        {
            merged = CheckAndMergeAnimals(animalName);
        }
    }

    public bool CheckAndMergeAnimals(string animalName)
    {
        // Find animals with the same name
        var sameAnimals = animals.FindAll(a => a.animalName == animalName);

        // Group by level and check mergingRequirement
        foreach (var animal in sameAnimals)
        {
            int level = animal.level;

            // Skip merging if the animal has reached max level
            if (level >= animal.maxLevel)
            {
                continue;
            }

            int requiredToMerge = animal.mergingRequirement;

            // Find animals with the same level
            var sameLevelAnimals = sameAnimals.FindAll(a => a.level == level);

            if (sameLevelAnimals.Count >= requiredToMerge)
            {
                // Remove the required number of animals
                for (int i = 0; i < requiredToMerge; i++)
                {
                    Destroy(sameLevelAnimals[i].gameObject);
                    animals.Remove(sameLevelAnimals[i]);
                }

                // Create a new higher-level animal
                int newLevel = level + 1;
                var animalPrefab = animalPrefabs.Find(prefab => prefab.GetComponent<FarmAnimal>().animalName == animalName);
                if (animalPrefab != null)
                {
                    var newAnimalObject = Instantiate(animalPrefab, GetRandomPosition(), Quaternion.identity, farmParentObject);
                    var newAnimal = newAnimalObject.GetComponent<FarmAnimal>();
                    newAnimal.level = newLevel;
                    newAnimal.transform.localScale = Vector3.one * (1 + (newLevel - 1) * newAnimal.sizeScale);
                    animals.Add(newAnimal);
                }

                // Return true to indicate a merge happened
                return true;
            }
        }

        // Return false if no merge occurred
        return false;
    }

    public void GenerateNewAnimal(string animalName)
    {
        var animalPrefab = animalPrefabs.Find(prefab => prefab.GetComponent<FarmAnimal>().animalName == animalName);
        if (animalPrefab != null)
        {
            var newAnimalObject = Instantiate(animalPrefab, GetRandomPosition(), Quaternion.identity, farmParentObject);
            var newAnimal = newAnimalObject.GetComponent<FarmAnimal>();
            newAnimal.level = 1;
            animals.Add(newAnimal);
        }
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
            var newBuildingObject = Instantiate(buildingPrefab, GetRandomPosition(), Quaternion.identity, farmParentObject);
            var newBuilding = newBuildingObject.GetComponent<FarmBuilding>();
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
}
