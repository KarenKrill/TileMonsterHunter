using UnityEngine;

using KarenKrill.InteractionSystem.Abstractions;

namespace TileMonsterHunter.Interactions
{
    [RequireComponent(typeof(Rigidbody))]
    public class PickupInteractable : OutlineInteractableBase, IInteractable
    {
        public Rigidbody Rigidbody { get; private set; }

        protected override void Awake()
        {
            base.Awake();
            Rigidbody = GetComponent<Rigidbody>();
        }

        protected override void OnInteraction()
        {

        }
    }
}
