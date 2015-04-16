using UnityEngine;
using System.Collections;

public class OilNode : Node {
    void Start() {
        // Do everything in init so we can call init on base as well
        Init();
    }

    protected override void Init() {
        NodeName = "Oil";
        // Forest starts with max wood count, and zero of other resources
        WoodCount = 0;
        FoodCount = 0;
        OilCount = Balance.OilResourceCount;

        base.Init();
    }

    public override UnitTask GetTask() {
        return new GatherTask(Balance.ResourceTypes.Oil);
    }

    public override void AcceptResources(ResourcePackage _resourcePackage) {
        throw new System.NotImplementedException();
    }
}
