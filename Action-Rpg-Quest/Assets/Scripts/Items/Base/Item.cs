using Advent.Entities;
using Advent.Enums;
using Advent.Interfaces;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace Advent.Items
{
    [System.Serializable]
    public class Item
    {
        public List<BaseStat> StatType { get; set; }
        public string ObjectSlug { get; set; } // AKA Prefab name
        public string itemIcon { get; set; }
        public string Description { get; set; }
        [JsonConverter(typeof(Newtonsoft.Json.Converters.StringEnumConverter))]
        public ItemTypes ItemType { get; set; }
        public EquipTypes EquipType { get; set; }
        public WeaponTypes WeaponType { get; set; }
        public ItemRarity ItemRarity { get; set; }
        public int ItemId { get; set; }
        public string ActionName { get; set; }
        public string ItemName { get; set; }
        public bool isStackable { get; set; }
        public bool ItemModifier { get; set; } //if true - it changes something in the stats 

        public Item() { }
        public Item(List<BaseStat> _StatType, string _ObjectSlug)
        {
            StatType = _StatType;
            ObjectSlug = _ObjectSlug;
        }

        [JsonConstructor]
        public Item(List<BaseStat> _Stats, string _ObjectSlug, string _itemIcon, string _Description, ItemTypes _ItemType, WeaponTypes _weaponType,EquipTypes _EquipType, int _itemId, string _ActionName, string _ItemName,bool _isStackable, bool _ItemModifier)
        {
            StatType = _Stats;
            ObjectSlug = _ObjectSlug;
            itemIcon = _itemIcon;
            Description = _Description;
            ItemType = _ItemType;
            WeaponType = _weaponType;
            EquipType = _EquipType;
            ItemId = _itemId;
            ActionName = _ActionName;
            ItemName = _ItemName;
            isStackable = _isStackable;
            ItemModifier = _ItemModifier;
        }

        public virtual void OnEquip(){}
        public virtual void OnConsume(){}
    }   
}