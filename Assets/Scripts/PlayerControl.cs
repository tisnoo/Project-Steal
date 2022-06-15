using Unity.Netcode;
using UnityEngine;

public class PlayerControl : NetworkBehaviour
{
    [SerializeField]
    private float walkSpeed = 3f;

    [SerializeField]
    private Vector2 defaultPositionRange = new Vector2(-4, 4);

    [SerializeField]
    private NetworkVariable<float> upDownPosition = new NetworkVariable<float>();

    [SerializeField]
    private NetworkVariable<float> leftRightPosition = new NetworkVariable<float>();

    // client caches positions
    private float oldUpDownPosition;
    private float oldLeftRightPosition;

    void Start()
    {
        transform.position = new Vector3(Random.Range(defaultPositionRange.x, defaultPositionRange.y),
               Random.Range(defaultPositionRange.x, defaultPositionRange.y), 0);
    }

    void Update()
    {
        if (IsServer)
            UpdateServer();

        if (IsClient && IsOwner)
            UpdateClient();
    }

    private void UpdateServer()
    {
        Debug.Log(leftRightPosition.Value);
        Debug.Log(upDownPosition.Value);

        transform.position = new Vector3(transform.position.x + leftRightPosition.Value,
            transform.position.y + upDownPosition.Value, transform.position.z);
    }

    private void UpdateClient()
    {

        float upDown = Input.GetAxisRaw("Vertical") * Time.deltaTime * walkSpeed;
        float leftRight = Input.GetAxisRaw("Horizontal") * Time.deltaTime * walkSpeed;

        if (oldUpDownPosition != upDown ||
            oldLeftRightPosition != leftRight)
        {
            UpdateClientPositionServerRpc(upDown, leftRight);
            oldUpDownPosition = upDown;
            oldLeftRightPosition = leftRight;
        }
    }

    [ServerRpc]
    public void UpdateClientPositionServerRpc(float forwardBackward, float leftRight)
    {
        upDownPosition.Value = forwardBackward;
        leftRightPosition.Value = leftRight;
    }
}