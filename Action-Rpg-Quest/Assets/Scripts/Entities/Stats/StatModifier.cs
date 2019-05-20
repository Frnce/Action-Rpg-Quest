using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Advent.Entities
{
    public class StatModifier
    {
        public readonly float value;
        public readonly StatModType type;
        public readonly int order;
        public readonly object source;


        public StatModifier(float _value,   StatModType _type,int _order, object _source)
        {
            value = _value;
            type = _type;
            order = _order;
            source = _source;
        }

        // Requires Value and Type. Calls the "Main" constructor and sets Order and Source to their default values: (int)type and null, respectively.
        public StatModifier(float value, StatModType type) : this(value, type, (int)type, null) { }

        // Requires Value, Type and Order. Sets Source to its default value: null
        public StatModifier(float value, StatModType type, int order) : this(value, type, order, null) { }

        // Requires Value, Type and Source. Sets Order to its default value: (int)Type
        public StatModifier(float value, StatModType type, object source) : this(value, type, (int)type, source) { }
    }
}