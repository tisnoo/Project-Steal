using Assets.Scripts;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.Netcode;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : Singleton<UIManager>
{
    [SerializeField]
    private Button startServerButton;

    [SerializeField]
    private Button startHostButton;

    [SerializeField]
    private Button startClientButton;

    [SerializeField]
    private TMP_InputField joinCodeInput;

    private void Awake()
    {
        Cursor.visible = true;
    }

    private void Start()
    {
        startServerButton?.onClick.AddListener(() =>
        {
            NetworkManager.Singleton.StartServer();
        });

        startHostButton?.onClick.AddListener(async () =>
        {
            if (RelayManager.Instance.IsRelayEnabled)
            {
                await RelayManager.Instance.SetupRelay();
            }
            NetworkManager.Singleton.StartHost();
        });

        startClientButton?.onClick.AddListener(async () =>
        {
            if (RelayManager.Instance.IsRelayEnabled)
            {
                await RelayManager.Instance.JoinRelay(joinCodeInput.text);
            }

            NetworkManager.Singleton.StartClient();
        });
    }
}
