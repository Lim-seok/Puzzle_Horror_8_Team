using System.Collections.Generic;
using UnityEngine;

public class CreatureManager : Singleton<CreatureManager>
{
    private List<CreatureBase> _creatures = new List<CreatureBase>();

    public void RegisterCreature(CreatureBase creature)
    {
        if (!_creatures.Contains(creature))
        {
            _creatures.Add(creature);
        }
    }

    public void UnregisterCreature(CreatureBase creature)
    {
        if (_creatures.Contains(creature))
        {
            _creatures.Remove(creature);
        }
    }

    private void Update()
    {
        foreach (var creature in _creatures)
        {
            creature.UpdateAI();
        }
    }
}
