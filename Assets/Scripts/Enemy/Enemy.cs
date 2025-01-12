using UnityEngine;

public class Enemy : Unit
{
    public override void Initialize(string name, int health, int attack)
    {
        base.Initialize(name, health, attack);
        Debug.Log($"Player {name} initialized with {health} HP, {attack} Attack.");
    }

    // Add Enemy-specific methods here (e.g., abilities, inventory management)
}
