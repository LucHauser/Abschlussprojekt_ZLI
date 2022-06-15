using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerScript : MonoBehaviour 
{
    public GameObject bulletPrefab;
    public GameObject shellPrefab;
    public GameObject muzzleFlash;
    public Transform spawnPoint;
    public bool ejectShell = true;
    public Transform shellSpawnPoint;
    public float shellForce = 100;
    public float bulletSpeed = 400;
    public AudioSource shotSound;

    public bool semiAutomatic = true;
    public float delay = 0.2f;
    public bool canShoot;

    private Rigidbody2D rb;
    public float thrust = -3;

    public float rotationSpeed = 2;
   
    void Start() {
        rb = GetComponent<Rigidbody2D>();
    }


    void Update() 
    {
        if (Input.GetKeyDown(KeyCode.Space) && semiAutomatic)
        {
            Shoot();
        }

        if (Input.GetKey(KeyCode.Space) && !semiAutomatic && canShoot) {
            Shoot();
            StartCoroutine(ShootDelay());
        }
    }

    private void FixedUpdate() {
        if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow)) 
        {
            transform.Rotate(0, 0, rotationSpeed);
        }

        if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
        {
            transform.Rotate(0, 0, -rotationSpeed);
        }
    }

    private void Shoot() 
    {
        shotSound.Play();

        GameObject bullet = Instantiate(bulletPrefab, spawnPoint.transform.position, spawnPoint.transform.rotation);
        bullet.GetComponent<Rigidbody2D>().AddForce(spawnPoint.transform.right * bulletSpeed);

        rb.AddForce(transform.right * thrust, ForceMode2D.Impulse);

        GameObject muzzleFlashClone = Instantiate(muzzleFlash, spawnPoint.transform.position, spawnPoint.transform.rotation);
        Destroy(muzzleFlashClone, 2);

        if (ejectShell)
        {
            GameObject shell = Instantiate(shellPrefab, shellSpawnPoint.transform.position, shellSpawnPoint.transform.rotation);
            shell.GetComponent<Rigidbody2D>().AddForce(shellSpawnPoint.transform.up * bulletSpeed);

            rb.AddForce(transform.right * thrust, ForceMode2D.Impulse);
            Destroy(shell, 3);
        }
    }

     void OnTriggerEnter2D(Collider2D collision) {
        if (collision.tag == "wall" || collision.tag == "enemy" || collision.tag == "enemy_child") {
            FindObjectOfType<GameOver>().GameEnd();
        }
    }

    IEnumerator ShootDelay() {
        canShoot = false;
        yield return new WaitForSeconds(delay);
        canShoot = true;
    }
}
        