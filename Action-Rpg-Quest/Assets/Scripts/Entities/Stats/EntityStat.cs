using System;
using System.Collections.ObjectModel;
using System.Collections.Generic;


namespace Advent.Entities
{
    public enum StatModType
    {
        FLAT = 100,
        PERCENTADDITIVE = 200,
        PERCENTMULTIPLICATIVE = 300
    }
    [Serializable]
    public class EntityStat
    {
        public float baseValue;

        protected bool isDirty = true;
        protected float lastBaseValue = float.MinValue;
        protected float _value;
        protected readonly List<StatModifier> statModifiers;
        public readonly ReadOnlyCollection<StatModifier> StatModifiers;

        public EntityStat()
        {
            statModifiers = new List<StatModifier>();
            StatModifiers = statModifiers.AsReadOnly();
        }
        public EntityStat(float _baseValue) : this()
        {
            baseValue = _baseValue;
        }

        public float getValue
        {
            get
            {
                if (isDirty || lastBaseValue != baseValue)
                {
                    lastBaseValue = baseValue;
                    _value = CalculateFinalValue();
                    isDirty = false;
                }
                return _value;
            }
        }

        public virtual void AddModifier(StatModifier mod)
        {
            isDirty = true;
            statModifiers.Add(mod);
            statModifiers.Sort(CompareModifierOrder);
        }
        public virtual bool RemoveModifier(StatModifier mod)
        {
            if (statModifiers.Remove(mod))
            {
                isDirty = true;
                return true;
            }
            return false;
        }
        public virtual bool RemoveAllModifiersFromSource(object _source)
        {
            bool didRemove = false;

            for (int i = statModifiers.Count - 1; i >= 0; i--)
            {
                if (statModifiers[i].source == _source)
                {
                    isDirty = true;
                    didRemove = true;
                    statModifiers.RemoveAt(i);
                }
            }
            return didRemove;
        }
        protected virtual float CalculateFinalValue()
        {
            float finalValue = baseValue;
            float sumPercentAdd = 0f;
            for (int i = 0; i < statModifiers.Count; i++)
            {
                StatModifier mod = statModifiers[i];
                if (mod.type == StatModType.FLAT)
                {
                    finalValue += mod.value;
                }
                else if (mod.type == StatModType.PERCENTADDITIVE)
                {
                    sumPercentAdd += mod.value;
                    if(i + 1 >= statModifiers.Count || statModifiers[i+1].type != StatModType.PERCENTADDITIVE)
                    {
                        finalValue *= 1 + sumPercentAdd;
                        sumPercentAdd = 0;
                    }
                }
                else if (mod.type == StatModType.PERCENTMULTIPLICATIVE)
                {
                    finalValue *= 1 + mod.value;
                }
            }
            return (float)Math.Round(finalValue, 4);
        }
        protected virtual int  CompareModifierOrder(StatModifier a,StatModifier b)
        {
            if(a.order < b.order)
            {
                return -1;
            }else if(a.order > b.order)
            {
                return 1;
            }
            return 0; //if a.order == b.order
        }
    }

}