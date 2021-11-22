using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Player
{
    public class Interactor : MonoBehaviour
    {
        [SerializeField]
        private LayerMask _layerMask;


        public bool Interact(Vector2 myPosition, Vector2 direciton, out GameObject gO)
        {
            gO = null;
            RaycastHit2D hit = Physics2D.Raycast(myPosition, direciton, 1f, _layerMask);

            if (hit.collider != null)
            {
                gO = hit.transform.gameObject;
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}