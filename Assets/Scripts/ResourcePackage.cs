using UnityEngine;
using System.Collections;

public class ResourcePackage {
    public enum ResourceType { Wood, Iron, Food }
    public ResourceType resourceType;			// TODO: this should be private, fix after testing

	private int resourceCount;
	public int ResourceCount {
		get { return resourceCount; }
		set { resourceCount = value; }
	}	

    public ResourcePackage() {
        resourceType = ResourceType.Wood;
        resourceCount = Balance.WoodResourceCount;
    }

    public ResourcePackage(ResourceType _resourceType, int _resourceCount) {
        resourceType = _resourceType;
        resourceCount = _resourceCount;
    }
}