using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpikeHead : EnemyDamage
{
    [Header("SpikeHead Attributes")]
    [SerializeField] private float range;
    [SerializeField] private float speed;
    [SerializeField] private float checkDelay;
    [SerializeField] private LayerMask playerlayer;
    [SerializeField] private AudioClip impactSound;
    private Vector3[] directions = new Vector3[4];
    private Vector3 destination;
    private float checkTimer;
    private bool attacking;
    
    

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    private void Update()
    {
        if(attacking)
            transform.Translate(destination * Time.deltaTime * speed);
        else
        {
            checkTimer += Time.deltaTime;
            if(checkTimer > checkDelay)
            {
                CheckForPlayer();
            }
        }
    }

    private void OnEnable()
    {
        Stop();
    }
    private void CheckForPlayer()
    {
        CalculatorDirection();

        for(int i=0; i<directions.Length; i++)
        {
            Debug.DrawRay(transform.position, directions[i], Color.red );
            RaycastHit2D hit = Physics2D.Raycast(transform.position, directions[i], range, playerlayer);

            if(hit.collider != null && !attacking)
            {
                attacking = true;
                destination = directions[i];
                checkTimer = 0;
            }    
        }
    }

    private void CalculatorDirection()
    {
        directions[0] = transform.right * range;
        directions[1] = -transform.right * range;
        directions[2] = transform.up * range;
        directions[3] = -transform.up * range;
    }

    private void Stop()
    {
        destination = transform.position;
        attacking = false;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        SoundManager.instance.PlaySound(impactSound);
        base.OnTriggerEnter2D(collision);
        Stop();
    }
}
