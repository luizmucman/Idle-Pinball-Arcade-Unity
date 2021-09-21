using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropTargetManager : ObjectManager
{
    // Drop Targets Data
    public int dropTargetsHit;
    public int dropTargetMax;

    public List<DropTarget> dropTargets;

    public override void Awake()
    {
        base.Awake();
        dropTargetsHit = 0;
        dropTargetMax = dropTargets.Count;
    }

    public void TargetHit(Ball ball)
    {
        dropTargetsHit++;
        if(dropTargetsHit >= dropTargetMax)
        {

        }
    }

    public void ResetDropTargets()
    {
        foreach (DropTarget target in dropTargets)
        {
            target.ResetDropTarget();
        }
    }
}
