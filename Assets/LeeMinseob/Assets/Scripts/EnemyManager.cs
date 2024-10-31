using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : Singleton<EnemyManager>
{
    private List<Enemy> enemies = new List<Enemy>();

    public void RegisterEnemy(Enemy enemy)
    {
        if (!enemies.Contains(enemy))
        {
            enemies.Add(enemy);
        }
    }

    public void UnregisterEnemy(Enemy enemy)
    {
        if (enemies.Contains(enemy))
        {
            enemies.Remove(enemy);
        }
    }

    public void TriggerHorrorBehavior()
    {
        foreach (var enemy in enemies)
        {
            enemy.EnterAggressiveState();
        }
    }
}
