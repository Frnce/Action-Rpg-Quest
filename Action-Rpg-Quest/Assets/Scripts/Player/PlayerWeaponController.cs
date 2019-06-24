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
    public class PlayerWeaponController : MonoBehaviour
    {
        public GameObject playerHand;
        public GameObject EquippedWeapon { get; set; }
        public GameObject BareHand; //Used when NoWeapon has been equipped
        [Space]
        [Range(1,4)]
        public float attackSpeed;
        private Item currentlyEquippedWeapon;
        private IWeapon equippedWeapon;
        private Player player;
        private PlayerController playerController;
        private Vector2 mouse;
        private int playerSortingOrder;

        private EntitiesStats playerStats;

        //For Player Attack Setting
        private float timeBetweenAttack;
        float finalUseTime;
        private void Awake()
        {
            EnableBareHands();
        }
        // Start is called before the first frame update
        void Start()
        {
            player = Player.instance;
            playerController = PlayerController.instance;
            playerSortingOrder = player.PlayerSprite.sortingOrder;

            playerStats = player.GetPlayerStats();

            EquipmentManager.instance.EquipItem(ItemDatabase.Instance.GetItem("Wpn_HuntingKnife"));
            timeBetweenAttack = currentlyEquippedWeapon.UseTime / 60;
            //TODO Place it on the calculations tab
            finalUseTime = Mathf.Floor(Mathf.Round(currentlyEquippedWeapon.UseTime * (1f - (player.GetAspd / 100f)))); // 10f is attack speed modifier = 10%
            Debug.Log(finalUseTime);
        }
        private void Update()
        {
            CheckIfAttacking();
            PlayerAttack();
            Aim();
        }
        private void PlayerAttack()
        {
            if (timeBetweenAttack <= 0)
            {
                if (player.GetPlayerStates != PlayerStates.ROLLING && player.GetPlayerStates != PlayerStates.INMENU)
                {
                    if (playerController.GetAttackKey && !player.IsAttacking)
                    {
                        SoundManager.instance.PlayerAttackRandomizeSfx(EquippedWeapon.GetComponent<IWeapon>().AudioClip);
                        StartCoroutine(DefaultAttackRoutine());
                        timeBetweenAttack = finalUseTime / 60; // 60 fps
                    }
                }
            }
            else
            {
                timeBetweenAttack -= Time.deltaTime;
            }
        }
        private void Aim()
        {
            //Dont Aim when in a attacking state
            if (player.GetPlayerStates != PlayerStates.ATTACKING)
            {
                //The Weapon should be facing up or vector2.up for it to work properly
                mouse = Camera.main.ScreenToViewportPoint(Input.mousePosition);        //Mouse position
                Vector3 objpos = Camera.main.WorldToViewportPoint(playerHand.transform.position);        //Object position on screen
                Vector2 relobjpos = new Vector2(objpos.x - 0.5f, objpos.y - 0.5f);            //Set coordinates relative to object
                Vector2 relmousepos = new Vector2(mouse.x - 0.5f, mouse.y - 0.5f) - relobjpos;
                float angle = Vector2.Angle(Vector2.up, relmousepos);    //Angle calculation
                if (relmousepos.x > 0)
                    angle = 360 - angle;
                Quaternion quat = Quaternion.identity;
                quat.eulerAngles = new Vector3(0, 0, angle); //Changing angle
                playerHand.transform.rotation = quat;
                if(angle > 60 && angle < 245)
                {
                    EquippedWeapon.GetComponent<SpriteRenderer>().sortingOrder = playerSortingOrder + 1;
                }
                else
                {
                    EquippedWeapon.GetComponent<SpriteRenderer>().sortingOrder = playerSortingOrder - 1;
                }

                if(angle >= 0 && angle <= 180)
                {
                    EquippedWeapon.GetComponent<SpriteRenderer>().flipX = true;
                }
                else
                {
                    EquippedWeapon.GetComponent<SpriteRenderer>().flipX = false;
                }
            }
        }
        private void CheckIfAttacking()
        {
            if (player.IsAttacking)
            {
                player.SetPlayerStates(PlayerStates.ATTACKING);
            }
        }
        private IEnumerator DefaultAttackRoutine()
        {
            player.IsAttacking = true;
            EquippedWeapon.GetComponent<IWeapon>().PerformAttack(1 + (player.GetAspd / 100)); // 10f is attack modifier
            //player.MicroStepAction();
            yield return new WaitForSeconds(finalUseTime / 60); //Change This later - when implementing the attack speed feature
            EquippedWeapon.GetComponent<Animator>().SetBool("isAttacking", false);
            EquippedWeapon.GetComponent<IWeapon>().ResetAttackTrigger();
            player.IsAttacking = false; 
        }
        public void EquipWeapon(Item itemToEquip)
        {
            if(itemToEquip.EquipType == Enums.EquipTypes.WEAPON)
            {
                if (equippedWeapon != null)
                {
                    UnequipWeapon();
                }
                EquippedWeapon = Instantiate(Resources.Load<GameObject>("Items/Item/Equipments/Weapons/" + itemToEquip.ObjectSlug), playerHand.transform);
                equippedWeapon = EquippedWeapon.GetComponent<IWeapon>();
                equippedWeapon.Stats = itemToEquip.Stats;
                currentlyEquippedWeapon = itemToEquip;

                playerStats.AddStatBonus(itemToEquip.Stats);
                EquipmentManager.instance.EquipChanged();
                //UIEventHandler.ItemEquipped(itemToEquip);
                //UIEventHandler.StatsChanged();
            }
        }
        public void UnequipWeapon()
        {
            Destroy(EquippedWeapon.transform.gameObject);
        }
        public void InitStats()
        {
            //player.GetStats.weaponDamage.minDamage, = currentlyEquippedWeapon.StatType[0]./*finalValue*/
        }
        public void UnequipEquipment()
        {
            //Removes weapon held on avatar
            Destroy(EquippedWeapon);
        }
        public void EnableBareHands() //No Weapon Equipped
        {
            EquippedWeapon = Instantiate(BareHand, playerHand.transform);
            equippedWeapon = EquippedWeapon.GetComponent<IWeapon>();
        }
    }
}