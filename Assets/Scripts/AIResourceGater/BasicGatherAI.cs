using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicGatherAI : MonoBehaviour
{
    private enum State
    {
        Idle,
        MovingToResourceNoe,
        GatheringResources,
        MovingToStorage,
    }

    private IUnit unit;
    private State state;
    private ResourceNode resourceNode;
    private Transform storageTransform;
    [SerializeField] private float stopDistance;
    private Dictionary<GameResource.ResourceType, int> inventoryAmountDictionary;

    private void Awake()
    {
        unit = gameObject.GetComponent<IUnit>();
        state = State.Idle;
        inventoryAmountDictionary = new Dictionary<GameResource.ResourceType, int>();
        foreach (GameResource.ResourceType resourceType in System.Enum.GetValues(typeof(GameResource.ResourceType)))
        {
            inventoryAmountDictionary[resourceType] = 0;
        }
    }
    private int GetTotalInventoryAmount()
    {
        int total = 0;
        foreach (GameResource.ResourceType resourceType in System.Enum.GetValues(typeof(GameResource.ResourceType)))
        {
            total += inventoryAmountDictionary[resourceType];
        }
        return total;
    }
    private bool IsInventoryFull()
    {
        return GetTotalInventoryAmount() >= 3;
    }
    private void DropInventoryAmountGameResource()
    {
        foreach (GameResource.ResourceType resourceType in System.Enum.GetValues(typeof(GameResource.ResourceType)))
        {
            GameResource.AddResourceAmount(resourceType, inventoryAmountDictionary[resourceType]);
            inventoryAmountDictionary[resourceType] = 0;
        }
        
    }
    void Start()
    {
        
    }
    void Update()
    {        
        switch (state)
        {
            case State.Idle:
                resourceNode = GameHandler.GetResourceNode_Static();
                if(resourceNode!=null)
                    state = State.MovingToResourceNoe;
                break;
            case State.MovingToResourceNoe:
                if (unit.IsIdle())
                {
                    unit.MoveTo(resourceNode.GetPositon(), stopDistance, () =>
                    {
                        state = State.GatheringResources;
                    });
                }
                break;
            case State.GatheringResources:
                if (unit.IsIdle())
                {
                    if (IsInventoryFull())
                    {
                        // Move to Storage
                        storageTransform = GameHandler.GetStorage_Static();
                        state = State.MovingToStorage;
                    }
                    else
                    {
                        // Gather resource
                        GrabResourceFromNode();
                    }
                }
                break;
            case State.MovingToStorage:
                if (unit.IsIdle())
                {
                    unit.MoveTo(storageTransform.position, stopDistance, () =>
                    {
                        DropInventoryAmountGameResource();
                        state = State.Idle;
                    });
                }
                break;
        }
    }
    private void GrabResourceFromNode()
    {
        GameResource.ResourceType resourceType = resourceNode.GrabResource();
        inventoryAmountDictionary[resourceType]++;        
    }
}
