using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Advent.Entities.Abilities
{
    public abstract class Ability : ScriptableObject
    {
        new public string name = "New Ability";
        public float baseCooldown = 1f;
        public float staminaCost; //TODO Implement this 
        public Sprite icon;

        public abstract void Initialize(GameObject obj);
        public abstract void TriggerAbility();
    }
}