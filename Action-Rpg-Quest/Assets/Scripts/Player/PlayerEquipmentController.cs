using Advent.Interfaces;
using Advent.Items;
using Advent.Utilities;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Advent.Entities
{
    public class PlayerEquipmentController : MonoBehaviour
    {
        public GameObject playerHand;
        public GameObject EquippedWeapon { get; set; }
        public GameObject BareHand; //Used when NoWeapon has been equipped

        Item currentlyEquippedItem;
        IWeapon equippedWeapon;
        Stats playerStats;
        // Start is called before the first frame update
        void Start()
        {
            playerStats = Player.instance.GetStats;
            UseBareHands();
        }
        private void Update()
        {
            if (Input.GetButtonDown("Fire1"))
            {
                EquippedWeapon.GetComponent<IWeapon>().PerformAttack(0);
            }
        }
        public void EquipWeapon(Item itemToEquip)
        {
            if(itemToEquip.EquipType == Enums.EquipTypes.WEAPON)
            {
                if (equippedWeapon != null)
                {
                    //Unequip Weapon
                }

                EquippedWeapon = Instantiate(Resources.Load<GameObject>("Items/Weapons/" + itemToEquip.ObjectSlug), playerHand.transform);
                equippedWeapon = EquippedWeapon.GetComponent<IWeapon>();
            }
        }
        public void UseBareHands() //No Weapon Equipped
        {
            EquippedWeapon = Instantiate(BareHand, playerHand.transform);
            equippedWeapon = EquippedWeapon.GetComponent<IWeapon>();
        }
    }
}