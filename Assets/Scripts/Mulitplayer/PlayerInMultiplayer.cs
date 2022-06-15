using UnityEngine;
using Photon.Pun;
using System.Collections;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerInMultiplayer : MonoBehaviourPunCallbacks, IPunObservable 
{
    public GameObject bulletPrefab;
    public GameObject shellPrefab;
    public GameObject flashLight;
    public GameObject muzzleFlash;
    public Transform spawnPoint;
    public Transform shellSpawnPoint;
    public float bulletSpeed = 400;
    public float shellForce = 100;
    public float flashLenght = 0.1f;
    public float delay = 0.2f;
    public bool canShoot;
    public AudioSource shotSound;

    private Rigidbody2D rb;
    public float thrust = -3;

    public float rotationSpeed = 2;
    private bool isGameOver;

    public static GameObject LocalPlayerInstance;

    void Awake() {
        if (photonView.IsMine) 
        {
            PlayerInMultiplayer.LocalPlayerInstance = this.gameObject;
        }

        DontDestroyOnLoad(this.gameObject);
    }

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        canShoot = true;
    }

    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info) {}

    void Update() 
    {
        if (!photonView.IsMine && PhotonNetwork.IsConnected || isGameOver)
        {
            return;
        }

        if (Input.GetKeyDown(KeyCode.Space) && canShoot)
        {
            Shoot();
            StartCoroutine(ShootDelay());
        }
    }

    void FixedUpdate() 
    {
        if (!photonView.IsMine && PhotonNetwork.IsConnected || isGameOver) 
        {
            return;
        }

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

        GameObject bullet = PhotonNetwork.Instantiate(bulletPrefab.name, spawnPoint.position, spawnPoint.rotation);
        bullet.GetComponent<Rigidbody2D>().AddForce(spawnPoint.transform.right * bulletSpeed);

        rb.AddForce(transform.right * thrust, ForceMode2D.Impulse);

        GameObject muzzleFlashClone = PhotonNetwork.Instantiate(muzzleFlash.name, spawnPoint.position, spawnPoint.rotation);

        GameObject shell = PhotonNetwork.Instantiate(shellPrefab.name, shellSpawnPoint.position, shellSpawnPoint.rotation);
        shell.GetComponent<Rigidbody2D>().AddForce(shellSpawnPoint.transform.up * bulletSpeed);

        rb.AddForce(transform.right * thrust, ForceMode2D.Impulse);

        StartCoroutine(Flash());

        StartCoroutine(DestroyGameobject(bullet));
        StartCoroutine(DestroyGameobject(muzzleFlashClone));
        StartCoroutine(DestroyGameobject(shell));
    }

    IEnumerator Flash() 
    {
        flashLight.SetActive(true);
        flashLight.GetComponent<Animator>().Play("dim");
        yield return new WaitForSeconds(flashLenght);
        flashLight.SetActive(false);
    }

    IEnumerator DestroyGameobject(GameObject objectToDestroy) 
    {
        yield return new WaitForSeconds(3);

        if (objectToDestroy != null) 
        {
            PhotonNetwork.Destroy(objectToDestroy);
        }
        else
        {
            Debug.Log("GameObject already destroyed");
        }
    }

    void OnTriggerEnter2D(Collider2D collision) {
        if (collision.tag == "wall" || collision.tag == "bullet") {
            if (photonView.IsMine == false && PhotonNetwork.IsConnected == true) {
                FindObjectOfType<GameOverInMulitplayer>().GameOver(true);
            }
            else {
                FindObjectOfType<GameOverInMulitplayer>().GameOver(false);
            }
            isGameOver = true;
            StartCoroutine(CloseRoom());
        }
    }

    IEnumerator ShootDelay() {
        canShoot = false;
        yield return new WaitForSeconds(delay);
        canShoot = true;
    }

    public IEnumerator CloseRoom() 
    {
        for (int i = 4; i >= 0; i--) 
        {
            FindObjectOfType<GameOverInMulitplayer>().SetSecondsLeft(i);

            if (i <= 0) 
            {
                FindObjectOfType<GameManager>().LeaveRoom();
            }
            yield return new WaitForSeconds(1);
        }
    }
}
        