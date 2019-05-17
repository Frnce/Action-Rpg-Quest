using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Advent.UI
{
    public class FloatingTextScript : MonoBehaviour
    {
        [SerializeField]
        private float destroyTime = 1f;
        [SerializeField]
        private Vector3 positionOffset = new Vector3(0, 2);
        // Start is called before the first frame update
        void Start()
        {
            Destroy(gameObject, destroyTime);

            transform.localPosition += positionOffset;
        }
    }
}
