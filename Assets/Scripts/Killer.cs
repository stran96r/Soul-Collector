using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Killer : MonoBehaviour
{
    [SerializeField] private float attackRange = 5f;
    [SerializeField] private float speed = 1f;
    public LayerMask playerLayer;
    private Rigidbody2D _rg;
    private int _yDir; 
    // private Player _player;
    // private bool _canRun = false;


    void Start()
    {
        // _player = FindObjectOfType<Player>();
        _rg = GetComponent<Rigidbody2D>();
        _yDir = (Random.Range(0,2) * 2) - 1;
    }

    private void FixedUpdate() {
        Run();
    }

    private void Run()
    {
        Collider2D other = Physics2D.OverlapCircle(transform.position, attackRange, playerLayer);
        if (other)
        {
            float step = speed * Time.deltaTime;
            transform.position = Vector2.MoveTowards(transform.position, other.transform.position, step);
            // var distance = _player.transform.position.x - transform.position.x;
            // _rg.velocity = new Vector2((distance > 0 ? -1 : 1), _yDir);
        } 
    }

    private void OnDrawGizmosSelected() {
        Gizmos.DrawWireSphere(transform.position, attackRange);
    }


}
