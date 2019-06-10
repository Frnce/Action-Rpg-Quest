using Advent.Controller;
using Advent.Interfaces;
using Advent.Items;
using Advent.Manager;
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
        Player player;
        PlayerController playerController;
        // Start is called before the first frame update
        void Start()
        {
            player = Player.instance;
            playerController = PlayerController.instance;
            UseBareHands();
        }
        private void Update()
        {
            if(player.GetPlayerStates != PlayerStates.ROLLING && player.GetPlayerStates != PlayerStates.INMENU)
            {
                if (playerController.GetAttackKey)
                {
                    SoundManager.instance.PlayerAttackRandomizeSfx(EquippedWeapon.GetComponent<IWeapon>().AudioClip);
                    StartCoroutine(DefaultAttackRoutine());
                    //timebetweeen attack . - implement the attack speed feature
                }
            }
        }
        private IEnumerator DefaultAttackRoutine()
        {
            player.SetPlayerStates(PlayerStates.ATTACKING);
            EquippedWeapon.GetComponent<IWeapon>().PerformAttack();
            player.MicroStepAction();
            yield return new WaitForSeconds(0.2f); //Change This later - when implementing the attack speed feature
            player.SetPlayerStates(PlayerStates.IDLE);
            EquippedWeapon.GetComponent<IWeapon>().ResetAttackTrigger();
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
        public void UnequipEquipment()
        {
            //Removes weapon held on avatar
            Destroy(EquippedWeapon);
        }
        public void UseBareHands() //No Weapon Equipped
        {
            EquippedWeapon = Instantiate(BareHand, playerHand.transform);
            equippedWeapon = EquippedWeapon.GetComponent<IWeapon>();
        }
    }
}