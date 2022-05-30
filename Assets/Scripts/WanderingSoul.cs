using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WanderingSoul : MonoBehaviour
{
    [SerializeField] private float _runRange = 5f;
    public LayerMask playerLayer;
    private Player _player;
    private Rigidbody2D _rg;
    public bool canCollect = false;
    public bool isCollected = false;    
    public bool didRunaway = false;   
    public bool canRunaway = true;   
    private int _yDir; 
    // private bool _canRun = false;


    void Start()
    {
        _player = FindObjectOfType<Player>();
        _rg = GetComponent<Rigidbody2D>();
        _yDir = (Random.Range(0,2) * 2) - 1;
    }

    void Update()
    {
        
    }

    private void FixedUpdate() {
        if (canRunaway) Run();
    }

    private void Run()
    {
        Collider2D other = Physics2D.OverlapCircle(transform.position, _runRange, playerLayer);
        if (other)
        {
            var distance = _player.transform.position.x - transform.position.x;
            _rg.velocity = new Vector2((distance > 0 ? -1 : 1), _yDir);
        } 
    }


    private void OnTriggerEnter2D(Collider2D other) {
        if (other.tag == "Player") { canCollect = true; }
        if (other.tag == "Runaway") { didRunaway = true; Destroy(this.gameObject, 0.2f);}
    }

    private void OnTriggerExit2D(Collider2D other) {
        if (other.tag == "Player") { canCollect = false; }
    }


    public void Collect()
    {
        Destroy(this.gameObject);
    }

    private void OnDrawGizmosSelected() {
        Gizmos.DrawWireSphere(transform.position, _runRange);
    }

}
