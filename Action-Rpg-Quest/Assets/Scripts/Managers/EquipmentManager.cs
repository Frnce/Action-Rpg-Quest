using Advent.Entities;
using Advent.Entities.Abilities;
using Advent.Items;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Advent.Manager
{
    public class EquipmentManager : MonoBehaviour
    {
        public static EquipmentManager instance;

        public SpriteRenderer weaponSpriteRenderer;
        //Add more spriteRenderer for player Here;
        public Equipment[] defaultEquipments;
        public Equipment[] currentEquipment;
        public delegate void OnEquipmentChanged(Equipment newItem, Equipment oldItem);
        public OnEquipmentChanged onEquipmentChangedCallback;
        Player player;
        InventoryManager inventory;

        private void Awake()
        {
            if (instance != null)
            {
                Destroy(gameObject);
            }
            else
            {
                instance = this;
            }
            DontDestroyOnLoad(gameObject);


            onEquipmentChangedCallback += OnEquipmentChange;
        }

        void Start()
        {
            player = Player.instance;
            inventory = InventoryManager.instance;
            int numOfSlots = System.Enum.GetNames(typeof(EquipSlots)).Length;
            currentEquipment = new Equipment[numOfSlots];
        }
        public void OnEquipmentChange(Equipment newItem, Equipment oldItem)
        {
            //Add New Stats on EquipmentChange
            if (newItem != null)
            {
                ////player.GetStats.physicalDefense.AddModifier(newItem.defense.GetValue());
                //StatModifier dmgMinMod = new StatModifier(newItem.GetStats().rangedValue.m_Min, StatModType.FLAT);
                //StatModifier dmgMaxMod = new StatModifier(newItem.GetDamage().m_Max, StatModType.FLAT);

                //player.GetStats.weaponDamage.minDamage.AddModifier(dmgMinMod);
                //player.GetStats.weaponDamage.maxDamage.AddModifier(dmgMaxMod);
            }
            if (oldItem != null)
            {
                //StatModifier dmgMinMod = new StatModifier(oldItem.GetDamage().m_Min, StatModType.FLAT);
                //StatModifier dmgMaxMod = new StatModifier(oldItem.GetDamage().m_Max, StatModType.FLAT);
                //player.GetStats.weaponDamage.minDamage.RemoveModifier(dmgMinMod);
                //player.GetStats.weaponDamage.maxDamage.RemoveModifier(dmgMaxMod);
            }
        }
        public void Equip(Equipment newItem)
        {
            int slotIndex = (int)newItem.GetSlots;
            Equipment oldItem = null;
            if (currentEquipment[slotIndex] != null)
            {
                oldItem = currentEquipment[slotIndex];
                inventory.AddItem(oldItem);
            }

            if (onEquipmentChangedCallback != null)
            {
                onEquipmentChangedCallback.Invoke(newItem, oldItem);
            }

            currentEquipment[slotIndex] = newItem;

            switch (newItem.slots)
            {
                case EquipSlots.WEAPON:
                    weaponSpriteRenderer.sprite = newItem.icon;
                    break;
                case EquipSlots.HEAD:
                    break;
                case EquipSlots.BODY:
                    break;
                case EquipSlots.LEGS:
                    break;
                case EquipSlots.FOOT:
                    break;
                case EquipSlots.ARMS:
                    break;
                case EquipSlots.ACCESORY:
                    break;
                default:
                    break;
            }
        }

        public void SwapEquip(int slotIndex)
        {
            if (currentEquipment[slotIndex] != null)
            {
                Equipment oldItem = currentEquipment[slotIndex];
                inventory.AddItem(oldItem);
                currentEquipment[slotIndex] = null;

                if (onEquipmentChangedCallback != null)
                {
                    onEquipmentChangedCallback.Invoke(null, oldItem);
                }
            }
        }

        public void EquipDefaults()
        {
            if (defaultEquipments.Length >= 0)
            {
                for (int i = 0; i < defaultEquipments.Length; i++)
                {
                    Equip(defaultEquipments[i]);
                }
            }
        }
    }
}