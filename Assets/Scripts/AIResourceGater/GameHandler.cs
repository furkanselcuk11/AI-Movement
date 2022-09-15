using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameHandler : MonoBehaviour
{
    private static GameHandler instance;
    
    [SerializeField] private Transform[] goldNodeTransformArray;
    [SerializeField] private Transform[] treeNodeTransformArray;
    [SerializeField] private Transform storageNodeTransform;

    private List<ResourceNode> resourceNodeList;
    private void Awake()
    {
        instance = this;
        GameResource.Init();

        resourceNodeList = new List<ResourceNode>();
        foreach (Transform goldNodeTransform in goldNodeTransformArray)
        {
            resourceNodeList.Add(new ResourceNode(goldNodeTransform,GameResource.ResourceType.Gold));
        }
        foreach (Transform treeNodeTransform in treeNodeTransformArray)
        {
            resourceNodeList.Add(new ResourceNode(treeNodeTransform, GameResource.ResourceType.Wood));
        }
    }
    
    private ResourceNode GetResourceNode()
    {
        List<ResourceNode> tmpResourceNodeList = new List<ResourceNode>(resourceNodeList);
        for (int i = 0; i < tmpResourceNodeList.Count; i++)
        {
            if (!tmpResourceNodeList[i].HasResources())
            {
                // No more resources
                tmpResourceNodeList.RemoveAt(i);
                i--;
            }
        }
        if (tmpResourceNodeList.Count > 0)
        {
            return tmpResourceNodeList[UnityEngine.Random.Range(0, tmpResourceNodeList.Count)];
        }
        else
        {
            return null;
        }        
    }
    public static ResourceNode GetResourceNode_Static()
    {
        return instance.GetResourceNode();
    }
    private Transform GetStorage()
    {
        return storageNodeTransform;
    }
    public static Transform GetStorage_Static()
    {
        return instance.GetStorage();
    }
}
