using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretShoot : MonoBehaviour
{
    [Header("SCRIPTS")]
    [SerializeField] private BulletScript _bulletScript;
    [SerializeField] private TurretSettings turretSettings;
    [SerializeField] private TurretAim turretAim;
    [SerializeField] private TargetingScript target;

    [Header("SHOOTING VARIABLES")]
    [SerializeField] private Transform _muzzle;
    [SerializeField] private GameObject _bullet;
    [SerializeField] private float damage;
    [SerializeField] private float speedMod;
    [SerializeField] private int pierce, projectilePassThrough;
    [SerializeField] private bool richochet = false;
    [SerializeField] private bool camoDetect = false;
    [SerializeField] private Vector3 bulletScale;

    [Header("PARTICLE SYSTEM")]
    [SerializeField] private ParticleSystem turretBlast;

    [Header("Animator")]
    [SerializeField] Animator anim;

    [Header("Sound")]
    [SerializeField] AudioSource turretShotSound;

    [Header("FIRERATE")]
    [SerializeField] private float fireRateTimer;
    [SerializeField] private float fireRateStartTimer;


    [Header("RAYCAST")]
    [SerializeField] private RaycastHit hit;
    [SerializeField] private Ray ray;

    private void Start()
    {
        target = gameObject.GetComponent<TargetingScript>();

        damage = turretSettings.damage;
        speedMod = turretSettings.speedMod;

        fireRateStartTimer = turretSettings.fireRateStartTimer;
        fireRateTimer = turretSettings.fireRateTimer;
        bulletScale = turretSettings.bulletScale;

        _bullet.transform.localScale = bulletScale;
    }

    private void FixedUpdate()
    {
        
   

                if (fireRateTimer <= 0 && target.GetEnemyArrayLength() > 0) { CreateBullet(_muzzle); fireRateTimer = fireRateStartTimer; }
                else fireRateTimer -= Time.deltaTime;
            
    }

    private void CreateBullet(Transform t)
    {
        turretBlast.Play();
        turretShotSound.PlayOneShot(turretShotSound.clip, turretShotSound.volume);
        GameObject tempBullet = Instantiate(_bullet, t.position, t.rotation);
        tempBullet.GetComponent<BulletScript>().SetDamage(damage);
        _bulletScript.SetProjectilePassThrough(projectilePassThrough);
        _bulletScript.SetRichochet(richochet);
        _bulletScript.Launch(t, tempBullet, speedMod);

        if(anim != null)
         anim.SetTrigger("Shoot");
    }

    public void SetDamage(float i) => damage *= i;
    public void SetFireRate(float i) => fireRateTimer /= i;
    public void SetPierce(int i) => pierce = i;
    public void SetProjectilePassThrough(int i) => projectilePassThrough = i;
    public void SetRichochet(bool b) => richochet = b;
    public void SetCamoDetect(bool b) => camoDetect = b;
}
