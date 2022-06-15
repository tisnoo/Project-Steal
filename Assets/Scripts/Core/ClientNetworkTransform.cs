using Unity.Netcode.Components;
using Unity.Netcode;
using UnityEngine;



    [DisallowMultipleComponent]
    public class ClientNetworkTransform : NetworkTransform
    {

        public override void OnNetworkSpawn()
        {
            base.OnNetworkSpawn();
            CanCommitToTransform = IsOwner;
        }

        protected override void Update()
        {
            CanCommitToTransform = IsOwner;
            base.Update();
            if (NetworkManager.Singleton != null && (NetworkManager.Singleton.IsConnectedClient || NetworkManager.Singleton.IsListening))
            {
                if (CanCommitToTransform)
                {
                    TryCommitTransformToServer(transform, NetworkManager.LocalTime.Time);
                }
            }
        }

        protected override bool OnIsServerAuthoritatitive()
        {
            return false;
        }
    }