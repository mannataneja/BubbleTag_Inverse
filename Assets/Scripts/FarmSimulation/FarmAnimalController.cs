using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FarmAnimalController : MonoBehaviour
{
    public Animator animator;
    
    public bool isCollected = false;
    int action = 0;

    // Movement settings
    private Vector3 targetPosition;
    private bool isMoving = false;
    private float moveSpeed = 2f; // Movement speed
    private float rotationSpeed = 5f; // Speed for smooth rotation
    private float waitTime; // Time to wait before next action
    private float actionTimer; // Timer to control actions
    private float houseFocusProbability = 0.7f; // 70% probability to target near the house

    private List<Transform> buildingPositions = new List<Transform>(); // Cache of building positions

    void Start()
    {
        if (animator == null)
        {
            animator = GetComponent<Animator>();
        }

        // Initialize first action
        SetNextAction();
    }

    void Update()
    {
        // Handle movement and actions
        if (isMoving)
        {
            MoveTowardsTarget();
        }
        else
        {
            HandleIdleActions();
        }
    }

    private void MoveTowardsTarget()
    {
        // Rotate towards the target position
        RotateTowardsTarget(targetPosition);

        // Move towards the target position
        transform.position = Vector3.MoveTowards(transform.position, targetPosition, moveSpeed * Time.deltaTime);

        // Check if reached the target
        if (Vector3.Distance(transform.position, targetPosition) < 0.1f)
        {
            isMoving = false;
            SetNextAction();
        }
    }

    private void RotateTowardsTarget(Vector3 target)
    {
        // Calculate direction towards the target
        Vector3 direction = (target - transform.position).normalized;

        // Ignore Y axis for rotation
        direction.y = 0;

        // Rotate smoothly towards the target
        if (direction != Vector3.zero)
        {
            Quaternion targetRotation = Quaternion.LookRotation(direction);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
        }
    }

    private void HandleIdleActions()
    {
        // Reduce the action timer
        actionTimer -= Time.deltaTime;

        // If eating, ensure eating ends properly
        if (animator.GetBool("IsEating") && actionTimer <= 0)
        {
            animator.SetBool("IsEating", false); // Reset eating animation
        }

        // When the timer expires, decide the next action
        if (actionTimer <= 0 && !animator.GetBool("IsEating"))
        {
            SetNextAction();
        }
    }

    private void SetNextAction()
    {
        // Randomly decide whether to move, idle, or perform another action
        // int action = Random.Range(0, 3);
        if (isCollected) 
        {
            action = Random.Range(0, 3);
        }

        switch (action)
        {
            case 0: // Idle
                isMoving = false;
                animator.SetInteger("Speed", 0); // Idle animation
                animator.SetInteger("IdleState", Random.Range(0, 3)); // Random idle animation
                actionTimer = Random.Range(2f, 5f); // Idle for a random duration
                break;

            case 1: // Move
                isMoving = true;
                animator.SetInteger("Speed", 1); // Walking animation
                targetPosition = GetRandomPositionWithBuildingBias();
                break;

            case 2: // Eating
                isMoving = false;
                animator.SetBool("IsEating", true); // Eating animation
                actionTimer = Random.Range(3f, 6f); // Eat for a random duration
                break;
        }
    }

    private Vector3 GetRandomPositionWithinRange()
    {
        if (FarmManager.instance == null || FarmManager.instance.farmParentObject == null)
        {
            Debug.LogError("FarmManager or farmParentObject is not assigned!");
            return transform.position;
        }

        // Generate a random position within the defined radius
        Vector3 randomOffset = Random.insideUnitSphere * FarmManager.instance.generationRadius;
        randomOffset.y = 0; // Keep the animal on the ground level

        return FarmManager.instance.farmParentObject.position + randomOffset;
    }

    private Vector3 GetRandomPositionWithBuildingBias()
    {
        if (FarmManager.instance == null)
        {
            Debug.LogError("FarmManager instance is not assigned!");
            return transform.position;
        }
        UpdateBuildingPositions();
        // Decide whether to target a building or a general farm location
        if (Random.value < houseFocusProbability && buildingPositions.Count > 0)
        {
            // Pick a random building position
            Transform buildingTransform = buildingPositions[Random.Range(0, buildingPositions.Count)];
            Vector3 randomOffset = Random.insideUnitSphere * FarmManager.instance.objectRadius; // Add some randomness around the building
            randomOffset.y = 0; // Keep the animal on the ground level
            return buildingTransform.position + randomOffset;
        }
        else
        {
            // Fallback to general farm area
            Vector3 randomOffset = Random.insideUnitSphere * FarmManager.instance.generationRadius;
            randomOffset.y = 0; // Keep the animal on the ground level
            return FarmManager.instance.farmParentObject.position + randomOffset;
        }
    }

    private void UpdateBuildingPositions()
    {
        if (FarmManager.instance == null) return;

        // Clear the existing building list
        buildingPositions.Clear();

        // Get buildings for this animal type
        List<FarmBuilding> buildings = FarmManager.instance.GetBuildingList(GetComponent<FarmAnimal>().animalName);
        foreach (var building in buildings)
        {
            if (building != null)
            {
                buildingPositions.Add(building.transform);
            }
        }
    }
}
