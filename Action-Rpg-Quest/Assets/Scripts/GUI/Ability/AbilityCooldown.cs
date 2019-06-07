using Advent.Controller;
using Advent.Entities;
using Advent.Entities.Abilities;
using Advent.Items;
using Advent.Manager;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Advent.UI
{
    public class AbilityCooldown : MonoBehaviour
    {
        public string AbilityButtonAxisName = "Fire1"; //Button from input manager
        public Image iconImage;
        public Image darkMask;
        public TMPro.TMP_Text cooldownTextDisplay;

        [SerializeField] private Ability ability;

        private float cooldownDuration;
        private float nextReadyTime; // for next ability can
        private float cooldownTimeLeft; // for UI
                                        // Start is called before the first frame update

        private void Awake()
        {
            //equipmentManager.onEquipmentChangedCallback += UpdateAbility;
            UIEventHandlers.OnEquipUpdate += UpdateAbility;
        }
        void Start()
        {
            iconImage.sprite = ability.icon;
        }

        public void Initialize(Ability selectedAbility)
        {
            ability = selectedAbility;

            cooldownDuration = ability.baseCooldown;
            Player.instance.gameObject.AddComponent<MeleeAttack>();
            ability.Initialize(Player.instance.gameObject);
        }

        // Update is called once per frame
        void Update()
        {
            bool coolDownComplete = (Time.time > nextReadyTime);
            if (coolDownComplete)
            {
                AbilityReady();
                if (PlayerController.instance.onButtonPressedController(AbilityButtonAxisName))
                {
                    ButtonTriggered();
                }
            }
            else
            {
                Cooldown();
            }
        }
        private void UpdateAbility()
        {
            Initialize(ability); // INitialize method will be initialized on player class select
        }
        private void AbilityReady()
        {
            cooldownTextDisplay.enabled = false;
            darkMask.enabled = false;
        }
        private void Cooldown()
        {
            cooldownTimeLeft -= Time.deltaTime;
            cooldownTextDisplay.text = cooldownTimeLeft.ToString("0.0");

            darkMask.fillAmount = (cooldownTimeLeft / cooldownDuration);
        }
        private void ButtonTriggered()
        {
            nextReadyTime = cooldownDuration + Time.time;
            cooldownTimeLeft = cooldownDuration;
            darkMask.enabled = true;
            cooldownTextDisplay.enabled = true;

            ability.TriggerAbility();
        }
    }
}