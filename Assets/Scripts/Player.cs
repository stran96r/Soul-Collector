using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [Range(0.5f, 2f)][SerializeField] float _attackDelay        = 1f;
    [SerializeField] AudioClip collectSound;
    [SerializeField] private int _moveSpeed = 5;
    public bool isDead = false;  
    public bool reachedTheEnd = false;
    public int collectedSouls = 0;
    private Rigidbody2D _rgbd;
    private Animator _animator;
    private Vector3 _moveDir;
    private SpriteRenderer spriteRenderer;
    private WanderingSoul[] _wanderingSouls;
    private AudioSource _audioSource;
    private bool _canMove = true; 
    private float _moveDirectionX; 
    private float _moveDirectionY; 
    private Timer _timer;
    private bool _canAttack = true;
    private float _moveDirX;
    private float _moveDirY;

    [Header("Dashing Stuff!")]
    [SerializeField] private float _dashingVelocity = 14f;
    [SerializeField] private float _dashDelay = 0.5f;
    [SerializeField] private ParticleSystem _dashPS = null;
    [SerializeField] private LayerMask _dashLayerMasks = new LayerMask();


    private bool _readyForDashing;
    private bool _canDash;



    void Start()
    {
        _wanderingSouls = FindObjectsOfType<WanderingSoul>();
        _timer = FindObjectOfType<Timer>();
        _rgbd = GetComponent<Rigidbody2D>();
        _rgbd.gravityScale = 0;
        _animator = GetComponent<Animator>();
        _canDash = false;
        _readyForDashing = true;
        spriteRenderer = GetComponent<SpriteRenderer>();
        _audioSource = GetComponent<AudioSource>();
    }

    void Update()
    {
        if (isDead) return;
        // AnimationSetup();
        SetMovement();
        Dashing();
        Attacking();

    }

    private void FixedUpdate() {
        if (isDead) return;
        Movement();
        FlipSprite();
        DashMovement();
    }

    private void SetMovement()
    {
        _moveDirectionX = Input.GetAxis("Horizontal");
        _moveDirectionY = Input.GetAxis("Vertical");

        Movement();
    }

    private void Movement()
    {
        if (_canMove)
        {
            _rgbd.velocity = new Vector2(_moveDirectionX, _moveDirectionY) * _moveSpeed;
        }
    }

    private void OnTriggerEnter2D(Collider2D other) 
    {
        if (other.tag == "Killer")
        {
            _timer.timerIsRunning = false;
            isDead = true;
            // StartCoroutine(StartingRoutine());
        }
        if (other.tag == "Finish")
        {
            reachedTheEnd = true;
        }
    }

    private void Attacking()
    {
        if (!_canAttack) return;
        if (Input.GetKeyDown(KeyCode.Q))
        {
            foreach (WanderingSoul soul in _wanderingSouls)
            {
                if(soul.canCollect) { soul.Collect(); collectedSouls ++; if (collectSound) _audioSource.PlayOneShot(collectSound); }
            }
            _canAttack = false;
            _animator.SetTrigger("Attack");
            StartCoroutine(AttackDelayRoutine());
        }
    }

    private void Dashing()
    {
        if (Input.GetKeyDown(KeyCode.Space) && _readyForDashing)
        {
            StartCoroutine(DashingRoutine());
            _canDash = true;
            _readyForDashing = false;
            if (_dashPS) _dashPS.Play();
        }

        if (_canDash)
        {
            _moveDir = new Vector3(_moveDirectionX, _moveDirectionY);
            if (_moveDir == Vector3.zero) { _moveDir = new Vector3(transform.localScale.x, 0, 0); }
        }
    }

    private void DashMovement()
    {
        if (_canDash)
        {
            var _dashPos = transform.position + (_moveDir.normalized * _dashingVelocity);

            RaycastHit2D _raycastHit2D = Physics2D.Raycast(transform.position, _moveDir, _dashingVelocity, _dashLayerMasks);
            if (_raycastHit2D.collider != null) { _dashPos = _raycastHit2D.point; }

            _rgbd.MovePosition(_dashPos);
            _canDash = false;
        }
    }

    IEnumerator DashingRoutine()
    {
        yield return new WaitForSeconds(_dashDelay);
        _readyForDashing = true;
    }

    IEnumerator StartingRoutine()
    {
        yield return new WaitForSeconds(2f);
        Destroy(this.gameObject, 0.1f);
    }

    private void FlipSprite()
    {
        if (_moveDirectionX > 0)
        {
            spriteRenderer.flipX = true;
        }
        else if (_moveDirectionX < 0)
        {
            spriteRenderer.flipX = false;
        }
    }

    IEnumerator AttackDelayRoutine()
    {
        yield return new WaitForSeconds(_attackDelay);
        _canAttack = true;
    }

}
