using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourceNode
{
    private Transform resourceNodeTransform;
    private GameResource.ResourceType resourceType;
    private int resourceAmount;
    private int resourceAmountMax;
    public ResourceNode(Transform resourceNodeTransform,GameResource.ResourceType resourceType)
    {
        this.resourceNodeTransform = resourceNodeTransform;
        this.resourceType = resourceType;
        resourceAmountMax = 3;
        resourceAmount = resourceAmountMax;
    }  
    public Vector3 GetPositon()
    {
        return resourceNodeTransform.position;
    }
    public GameResource.ResourceType GetResourceType()
    {
        return resourceType;
    }
    public GameResource.ResourceType GrabResource()
    {
        resourceAmount -= 1;
        if (resourceAmount <= 0)
        {
            UpdateMaterialResource();
            ResetResourceAmount();               
        }
        return resourceType;
    }
    public bool HasResources()
    {
        return resourceAmount > 0;
    }
    public void ResetResourceAmount()
    {
        
        resourceAmount = resourceAmountMax;
        UpdateMaterialResource();
    }
    private void UpdateMaterialResource()
    {
        if (resourceAmount > 0)
        {
            switch (resourceType)
            {
                default:
                case GameResource.ResourceType.Gold:
                    resourceNodeTransform.GetComponent<MeshRenderer>().material.color = Color.yellow;
                    break;
                case GameResource.ResourceType.Wood:
                    resourceNodeTransform.GetComponent<MeshRenderer>().material.color = Color.green;
                    break;
            }
        }
        else
        {
            switch (resourceType)
            {
                default:
                case GameResource.ResourceType.Gold:
                    resourceNodeTransform.GetComponent<MeshRenderer>().material.color = Color.black;
                    break;
                case GameResource.ResourceType.Wood:
                    resourceNodeTransform.GetComponent<MeshRenderer>().material.color = Color.red;
                    break;
            }            
        }
    }
}
