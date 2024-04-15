using System.Collections;
using UnityEngine;

public class BulletScript : MonoBehaviour
{
    [Header("BULLET INFO")]
    [SerializeField] private ParticleSystem _hitEffect = null; // Particle System
    [SerializeField] private TrailRenderer trailRenderer; // Trail Renderer

    [Header("FLOATS")]
    [SerializeField] private float _speed, _lifetime, _damage; // Bullet Speed, Bullet Lifetime, Bullet Damage

    [Header("BOOLS")]
    [SerializeField] private int _canHit = 0; // If Bullet Can Hit
    [SerializeField] private int projectilePassThrough = 0, pierce;
    [SerializeField] private bool _canRichochet;
    [SerializeField] private bool _isSeeking = false;
    private GameObject enemy;

    private void Start()
    {
        StartCoroutine(DestroyOverLifeTime()); // Starts Coroutine to destroy bullet
        _canHit = 0; // Sets Can Hit to 0
        trailRenderer.startWidth = transform.localScale.x / 10; // Scales trail renderer down
    }

    private void Update()
    {
        if(_isSeeking)
        {
            float step = _speed * Time.deltaTime;
            Vector3 target = new Vector3(enemy.transform.position.x, transform.position.y, enemy.transform.position.z);
            transform.LookAt(target);
            gameObject.transform.position = Vector3.MoveTowards(transform.position, new Vector3(enemy.transform.position.x, enemy.transform.position.y, enemy.transform.position.z), step);
        }
        if(!enemy)
        {
            Destroy(gameObject);
        }
    }

    //----------------------------------------------------
    // Launch functions determines how fast the bullet travels along z axis
    // Takes Transform of Gun Muzzle and GameObject of Bullet Clone
    public void Launch(Transform t, GameObject clone)
    {
        // Sets velocity of Bullet Clone to variable _speed
        clone.GetComponent<Rigidbody>().velocity = t.forward * _speed;
    }

    // OVERLOAD WITH SPEED MODIFIER
    public void Launch(Transform t, GameObject clone, float speed)
    {
        // Sets velocity of Bullet Clone to variable _speed
        clone.GetComponent<Rigidbody>().velocity = t.forward * speed;
    }
    //----------------------------------------------------------

    //----------------------------------------------------------
    // Timer function to destroy bullet
    // Lifetime of bullet is determined by variable _lifetime
    private IEnumerator DestroyOverLifeTime()
    {
        // Wait for _lifetime in seconds
        yield return new WaitForSeconds(_lifetime);

        // Destroys bullet clone
        Destroy(gameObject);
    }
    //-----------------------------------------------------------

    //-----------------------------------------------------------
    // Collision detection method
    // Detects if collided with tag "enemy"
    private void OnTriggerEnter(Collider other)
    {
        // If Bullet collides with enemy && Bullet Can Hit
        if (other.tag == "enemy" && _canHit <= projectilePassThrough)
        {
            //Debug.Log("Enemy");
            _canHit++; // Adds 1 to CanHit

            // If enemy has DamageEvent Attatched, Damage enemy
            if (other.GetComponentInParent<EnemyArrayIndex>())
                EventManagerScript.EnemyHit(other.GetComponentInParent<EnemyArrayIndex>().Index, _damage, pierce);
        }

        if (_canHit > projectilePassThrough) Destroy(gameObject);
    }
    private void OnCollisionEnter(Collision collision)
    { 
        if (collision.collider.CompareTag("ground") && !_canRichochet)
        {
            //Debug.Log("Ground");
            Destroy(gameObject);
        }
    }

    //-- SET DAMAGE --\\
    // FLOAT: Bullet Damage
    // Sets the bullet's damage
    public void SetDamage(float damage) => _damage = damage;
    public void SetProjectilePassThrough(int mod) => projectilePassThrough = mod;

    public void SetPierce(int mod) => pierce = mod;
    public void SetRichochet(bool b) => _canRichochet = b;

    public void SetTarget(GameObject t) => enemy = t;
}
