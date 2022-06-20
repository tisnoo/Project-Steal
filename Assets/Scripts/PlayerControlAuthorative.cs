using Unity.Netcode;
using UnityEngine;

public class PlayerControlAuthorative : NetworkBehaviour
{
    public enum PlayerState
    {
        Idle,
        Walk,
    }

    [SerializeField]
    private float walkSpeed = 3f;

    [SerializeField]
    private Vector2 defaultPositionRange = new Vector2(-4, 4);

    [SerializeField]
    private NetworkVariable<PlayerState> networkPlayerState = new NetworkVariable<PlayerState>();

    private Animator animator;

    private float updownMovement = 0;
    private float leftRightMovement = 0;

    // client caches animation states
    private PlayerState oldPlayerState = PlayerState.Idle;

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    void Start()
    {
        if (IsClient && IsOwner)
        {
            transform.position = new Vector3(Random.Range(defaultPositionRange.x, defaultPositionRange.y),
       Random.Range(defaultPositionRange.x, defaultPositionRange.y), 0);
        }
    }

    void Update()
    {
        if (IsClient && IsOwner)
        {
            updownMovement = Input.GetAxisRaw("Vertical");
            leftRightMovement = Input.GetAxisRaw("Horizontal");
        }

        ClientVisuals();
    }

    private void FixedUpdate()
    {
       

        transform.position = new Vector3(transform.position.x + leftRightMovement * Time.deltaTime * walkSpeed,
           transform.position.y + updownMovement * Time.deltaTime * walkSpeed);

        if (leftRightMovement != 0 || updownMovement != 0)
        {
            UpdatePlayerStateServerRpc(PlayerState.Walk);
        }
        else
        {
            UpdatePlayerStateServerRpc(PlayerState.Idle);
        }



        if (leftRightMovement < 0)
        {
            transform.localScale = new Vector3(-1, 1, 1);
        }
        else if (leftRightMovement > 0)
        {

            transform.localScale = new Vector3(1, 1, 1);
        }
        else
        {

            transform.localScale = transform.localScale;
        }
    }

    private void ClientVisuals()
    {
        if (networkPlayerState.Value == PlayerState.Walk)
        {
            animator.SetFloat("Walk", 1);
        }
        else
        {
            animator.SetFloat("Walk", 0);
        }
    }

    [ServerRpc]
    public void UpdatePlayerStateServerRpc(PlayerState state)
    {
        networkPlayerState.Value = state;
    }
}