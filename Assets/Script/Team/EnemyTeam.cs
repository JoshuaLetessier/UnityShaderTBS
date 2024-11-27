using UnityEngine;

public class EnemyTeam : Team
{
    private System.Random _random = new System.Random();
    public override void RequestEntityTarget(RequestEntityTargetDescriptor requestDescriptor)
    {
        int randomIndex = _random.Next(0, requestDescriptor.selectableEntities.Count);
        requestDescriptor.onTargetFound(requestDescriptor.selectableEntities[randomIndex]);
    }

    public override void Defeated()
    {
        // Game Over
    }
}
