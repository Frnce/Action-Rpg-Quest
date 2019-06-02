using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Advent.UI
{
    public class InventoryUIItem : MonoBehaviour,IBeginDragHandler,IDragHandler,IEndDragHandler
    {
        private Vector3 startPosition;
        private Transform originalParent;

        private Canvas parentCanvas;
        private InventoryUISlot parentSlot;

        private void Awake()
        {
            parentCanvas = GameObject.FindGameObjectWithTag("MainCanvass").GetComponent<Canvas>();
            parentSlot = GetComponentInParent<InventoryUISlot>();
        }
        public InventoryUISlot ParentSlot { get { return parentSlot; } }

        public void OnBeginDrag(PointerEventData eventData)
        {
            startPosition = transform.position;
            originalParent = transform.parent;
            transform.SetParent(parentCanvas.transform);
            GetComponent<CanvasGroup>().blocksRaycasts = false;
        }

        public void OnDrag(PointerEventData eventData)
        {
            transform.position = eventData.position;
        }

        public void OnEndDrag(PointerEventData eventData)
        {
            transform.position = startPosition;
            transform.SetParent(originalParent);
            GetComponent<CanvasGroup>().blocksRaycasts = true;

            if (!EventSystem.current.IsPointerOverGameObject())
            {
                //discard item
            }
        }
    }
}