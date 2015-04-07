using UnityEngine;
using System.Collections;

public class ResourcePackage {
	private Balance.ResourceTypes resourceType;
	public Balance.ResourceTypes ResourceType {
		get { return resourceType; }
	}

	private int resourceCount;
	public int ResourceCount {
		get { return resourceCount; }
		set { resourceCount = value; }
	}	

    public ResourcePackage() {
        resourceType = Balance.ResourceTypes.Wood;
        resourceCount = 0;
    }

    public ResourcePackage(Balance.ResourceTypes _resourceType, int _resourceCount) {
        resourceType = _resourceType;
        resourceCount = _resourceCount;
    }
}