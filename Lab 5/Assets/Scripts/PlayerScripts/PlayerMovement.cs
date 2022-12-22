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

    [SerializeField] public int PlayerHealth { get; set; }


    private void Start()
    {
        Debug.Log("PlayerMovement Start");
        PlayerHealth = 3;
        _rigidBody = GetComponent<Rigidbody2D>();
        //Time.timeScale = 1;
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
    }

    private bool RaycastFromSensor(Transform sensor)
    {
        RaycastHit2D hit;
        var position = sensor.position;
        var forward = sensor.forward;
        hit = Physics2D.Raycast(position, forward, _groundCheckDistance, _groundCheckLayerMask);
        if (hit.collider != null)
        {
            Debug.DrawRay(position, forward * _groundCheckDistance, Color.magenta);
            return true;
        }
        else
        {
            Debug.DrawRay(position, forward * _groundCheckDistance, Color.green);
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

    void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.layer == 10)
        {
            PlayerHealth -= 1;
            if (!(PlayerHealth <= 0))
            {
                Debug.Log("Taking trap damage, health: " + PlayerHealth);
                _rigidBody.AddForce(new Vector2(0f, jumpSpeed/2), ForceMode2D.Force);
                StartCoroutine(Invulnerability());
            }
            
        }

        if (collision.gameObject.layer == 11)
        {
            int damage = collision.gameObject.GetComponent<EnemyDamage>().Damage;
            PlayerHealth -= damage;
            if (!(PlayerHealth <= 0))
            {
                Debug.Log("Taking enemy damage, health: " + PlayerHealth);
                //_rigidBody.AddForce(new Vector2(0f, jumpSpeed / 2), ForceMode2D.Force);
                StartCoroutine(Invulnerability());
            }
        }
    }

    private IEnumerator Invulnerability()
    {
        Debug.Log("Becoming invincible");
        Physics2D.IgnoreLayerCollision(9, 10, true);
        Physics2D.IgnoreLayerCollision(9, 11, true);
        yield return new WaitForSeconds(0.66f);
        Physics2D.IgnoreLayerCollision(9, 10, false);
        Physics2D.IgnoreLayerCollision(9, 11, false);
        Debug.Log("Exiting invincibility");
    }
}








