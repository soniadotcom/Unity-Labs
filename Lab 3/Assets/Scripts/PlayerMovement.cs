using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour, IPlayer
{
    [SerializeField] private Transform[] _sensors;
    [SerializeField] private LayerMask _groundCheckLayerMask;

    [SerializeField] private float _groundCheckDistance = 0.1f;

    private Rigidbody2D _rigidBody;
    public float moveSpeed = 8f;
    public float jumpSpeed = 8f;

    private IPlayerState playerState;

    public int PlayerHealth { get; set; }


    private void Start()
    {
        PlayerHealth = 3;
        _rigidBody = GetComponent<Rigidbody2D>();
    }

    void Awake()
    {
        playerState = new PlayerJumpedState();
    }

    void Update()
    {
        playerState.updatePlayerState(this);

        Vector3 movement = new Vector3(Input.GetAxis("Horizontal"), 0f, 0f);
        _rigidBody.velocity = new Vector2(Input.GetAxis("Horizontal") * moveSpeed, _rigidBody.velocity.y);
    }

    public void Jump()
    {
        _rigidBody.AddForce(new Vector2(0f, jumpSpeed), ForceMode2D.Force);
    }

    public void SetState(IPlayerState playerState)
    {
        this.playerState = playerState;
        Debug.Log(playerState.ToString());
    }

    private bool RaycastFromSensor(Transform sensor)
    {
        RaycastHit2D hit;
        var position = sensor.position;
        var forward = sensor.forward;
        hit = Physics2D.Raycast(position, forward, _groundCheckDistance, _groundCheckLayerMask);
        if (hit.collider != null)
        {
            return true;
        }
        return false;
    }

    public bool RaycastFromAllSensors()
    {
        foreach (var sensor in _sensors)
        {
            if (RaycastFromSensor(sensor)) return true;
        }
        return false;
    }
}








