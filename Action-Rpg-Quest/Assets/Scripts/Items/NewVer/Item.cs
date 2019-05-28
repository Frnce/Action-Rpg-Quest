using Advent.Entities;
using Advent.Enums;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace Advent.Items.New
{
    public class Item
    {
        public List<BaseStat> Stats { get; set; }
        public string ObjectSlug { get; set; } // AKA Prefab name
        public string Description { get; set; }
        [JsonConverter(typeof(Newtonsoft.Json.Converters.StringEnumConverter))]
        public ItemTypes ItemType { get; set; }
        public ItemRarity ItemRarity { get; set; }
        public string ActionName { get; set; }
        public string ItemName { get; set; }
        public bool ItemModifier { get; set; } //if true - it changes something in the stats 

        public Item(List<BaseStat> _Stats, string _ObjectSlug)
        {
            Stats = _Stats;
            ObjectSlug = _ObjectSlug;
        }

        [JsonConstructor]
        public Item(List<BaseStat> _Stats, string _ObjectSlug, string _Description, ItemTypes _ItemType, string _ActionName, string _ItemName, bool _ItemModifier)
        {
            Stats = _Stats;
            ObjectSlug = _ObjectSlug;
            Description = _Description;
            ItemType = _ItemType;
            ActionName = _ActionName;
            ItemName = _ItemName;
            ItemModifier = _ItemModifier;
        }
    }
}