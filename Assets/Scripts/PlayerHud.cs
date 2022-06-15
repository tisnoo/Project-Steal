using Assets.Scripts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TMPro;
using Unity.Collections;
using Unity.Netcode;
using UnityEngine;

class PlayerHud : NetworkBehaviour
    {

        private NetworkVariable<NetworkString> playersName = new NetworkVariable<NetworkString>();

        private bool overlaySet = false;

        public override void OnNetworkSpawn()
        {
            if (IsServer)
            {
                playersName.Value = $"Player {OwnerClientId}";


            }
        }

        public void SetOverlay()
        {
            var localPlayerOverlay = gameObject.GetComponentInChildren <TextMeshPro>();
            localPlayerOverlay.text = playersName.Value;

        }

        private void Update()
        {
            if (!overlaySet && !string.IsNullOrEmpty(playersName.Value))
            {
                SetOverlay();
                overlaySet = true;
            }
        }
    }
