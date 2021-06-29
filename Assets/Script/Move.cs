using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;
using Photon.Pun;

public class Move : MonoBehaviour
{
    public PhotonView _photonView;


    [SerializeField] private Transform spawnBomb;
    [SerializeField] private GameObject bomba;
    private Rigidbody rdb;
    public float speed;
    public float forçaImpulso;
    public bool impulse;
    public bool isGround;
    public int limiteGrowUp;
    public float forcaPulo;
    public int limitePowerUpSpeed;
    public int contbomba;

    [SerializeField] private CinemachineVirtualCamera vcam;
    [SerializeField] private Camera cam;

    public static Transform _transform;
    public static Move move;

    void Start()
    {
        _photonView = GetComponent<PhotonView>();
        contbomba = 0;
        rdb = GetComponent<Rigidbody>();
        _transform = GetComponent<Transform>();
        forçaImpulso = 2;
        limiteGrowUp = 0;
        limitePowerUpSpeed = 0;



        if (_photonView.IsMine)
        {
            _transform = transform;
            Instantiate(cam);
            Instantiate(vcam);
        }

    }


    void Update()
    {
        if (_photonView.IsMine)
        {
           
            tag = "Player";
            Mover();
            if (transform.position.y < 0.5)
            {
                rdb.mass = 20;
            }
            if (Input.GetKey(KeyCode.Space) && isGround)
            {
                rdb.AddForce(transform.up * forcaPulo);
                isGround = false;
            }

            if (Input.GetButtonDown("Fire1"))
            {
                Impulso();
                StartCoroutine("VerBol");

            }
            if (Input.GetButtonDown("Fire2") && contbomba > 0)
            {
                Tiro();
                contbomba--;
            }
        }
        else
        {
            tag = "Opponent";
        }


    }
    [PunRPC]
    public void Mover()
    {
        if (_photonView.IsMine)
        {
            if (Input.GetKey(KeyCode.W))
                transform.Translate(new Vector3(0, 0, speed * Time.deltaTime));
            if (Input.GetKey(KeyCode.S))
                transform.Translate(new Vector3(0, 0, -3 * Time.deltaTime));
            if (Input.GetKey(KeyCode.A))
                transform.Rotate(new Vector3(0, -100 * Time.deltaTime, 0));
            if (Input.GetKey(KeyCode.D))
                transform.Rotate(new Vector3(0, 100 * Time.deltaTime, 0));
        }

    }

    [PunRPC]
    public void Impulso()
    {
        rdb.AddRelativeForce(Vector3.forward * forçaImpulso * speed, ForceMode.Impulse);
    }

    [PunRPC]
    public void GrowUp()
    {
        if (limiteGrowUp <= 3)
        {
            limiteGrowUp++;
            transform.localScale = new Vector3(transform.localScale.x + 0.5f, transform.localScale.y + 0.5f, transform.localScale.z + 0.5f);
            rdb.mass += 2;
            if (limiteGrowUp == 1)
            {
                forçaImpulso += 3;
                speed += 2;
                forcaPulo = 1000;
            }
            else if (limiteGrowUp == 2)
            {
                forçaImpulso += 6;
                speed += 2;
                forcaPulo = 1500;
            }
            else if (limiteGrowUp == 3)
            {
                forçaImpulso += 9;
                speed += 2;
                forcaPulo = 2000;
            }

        }

    }
    [PunRPC]
    public void Tiro()
    {
        Instantiate(bomba, spawnBomb.transform.position, transform.rotation);
    }
    [PunRPC]
    private void OnCollisionEnter(Collision col)
    {
        if (col.gameObject.tag == "chao")
        {
            isGround = true;
        }
        if (col.gameObject.tag == "Opponent" && impulse == true)
        {

            rdb.constraints = RigidbodyConstraints.FreezeAll;
            rdb.constraints = RigidbodyConstraints.None;
            rdb.constraints = RigidbodyConstraints.FreezeRotation;
        }

    }

    IEnumerator VerBol()
    {
        impulse = true;
        yield return new WaitForSeconds(2);
        impulse = false;
    }



}