using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Platformer.Core.Simulation;


namespace Platformer.Mechanics
{
    public class CharCollisionBlocker : MonoBehaviour
    {  
        public Collider2D charCollider;
        public Collider2D charBlockerCollider;

        void Start()
        {
            Physics2D.IgnoreCollision(charCollider, charBlockerCollider, true);
        }

    }
}
