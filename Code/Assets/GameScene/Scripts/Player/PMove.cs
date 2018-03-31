using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;

public class PMove : NetworkBehaviour
{
    private RGameScript GameState;


    public bool bItemBomb;
    public bool bItemWall;
    public bool bMoving;
    public bool bDamage;

    public bool bSuper;

    public int PlayerColor;

    private float fBlinkTime;
    public float moveSpeed;
    public float cameraRotate;

    private Rigidbody rb;
    public Animator Anim;
    public RNetworkItem NItem;

    public Image BombImage;
    public Image WallImage;

    public GameObject itemBomb;
    public GameObject itemWall;

    private AudioSource Ado_GetItem;

    float h, v, r;

    private void OnEnable()
    {
        ClientScene.RegisterPrefab(itemBomb);
        ClientScene.RegisterPrefab(itemWall);
    }
    void Start()
    {
        bItemBomb = false; bItemWall = false;
        bMoving = false; bDamage = false;
        bSuper = false;

        fBlinkTime = 0;

        moveSpeed = 4.5f;
        cameraRotate = 2f;

        Anim = transform.GetChild(0).GetComponent<Animator>();
        rb = GetComponent<Rigidbody>();
       
    }

    void Update()
    {
        // This scene is Game Scene! (After Trans Scene from Menu)
        // This [ if ] instead actived Init Func
        if (SceneManager.GetActiveScene().buildIndex == 1 &&
            BombImage == null)
        {
            GameState = GameObject.Find("GamePanel").GetComponent<RGameScript>();

            BombImage = GameObject.Find("Canvas/Game/Img_Bomb").GetComponent<Image>();
            BombImage.color = Color.gray;
            WallImage = GameObject.Find("Canvas/Game/Img_Wall").GetComponent<Image>();
            WallImage.color = Color.gray;

            gameObject.GetComponent<RCameraScript>().enabled = false;
            Ado_GetItem = GameObject.Find("Sound/GetItem").GetComponent<AudioSource>();
        }

        // if, PlayerColor is Default... -> Get Player Color
        if (PlayerColor == 0)
        {
            PlayerColor = GetComponent<RNetCharaInfo>().CharaCV;
        }
        AnimFunc();

        // if, this Player is [ SUPER MODE ]
        if (bSuper)
        {
            fBlinkTime += Time.deltaTime;

            // Blink
            if (fBlinkTime > 0.07f)
            {
                GameObject PModel = transform.GetChild(0).gameObject;

                if (PModel.activeSelf)
                    PModel.SetActive(false);
                else
                    PModel.SetActive(true);

                fBlinkTime = 0;
            }
        }
        // if, SUPER MODE is End...
        else
        {
            // if Player's Model Object is disapper...
            if (!transform.GetChild(0).gameObject.activeSelf)
                transform.GetChild(0).gameObject.SetActive(true);
        }
    }

    void FixedUpdate()
    {
        if (!isLocalPlayer) return;
        if (bDamage) return;
        if (SceneManager.GetActiveScene().buildIndex == 2) return;
        if (SceneManager.GetActiveScene().buildIndex == 1)
        {
            if (!GameState) return;
            else if (GameState.gs != RGameScript.GameState.Play)
            {
                bMoving = false;
                return;
            }
        }

        r = 0;

        if (Input.GetAxis("Horizontal") > 0 || Input.GetAxis("Vertical") > 0)
        {
            bMoving = true;
        }
        else if (Input.GetAxis("Horizontal") <= 0 && Input.GetAxis("Vertical") <= 0)
        {
            bMoving = false;
        }
        h = Input.GetAxis("Horizontal");
        v = Input.GetAxis("Vertical");

        if (Input.GetKey("joystick button 6") || Input.GetKey(KeyCode.Q))
        {
            r = -1;
        }
        if (Input.GetKey("joystick button 7") || Input.GetKey(KeyCode.E))
        {
            r = +1;
        }

        transform.Translate(new Vector3(h, 0, v) * moveSpeed * Time.smoothDeltaTime);

        transform.Rotate(new Vector3(0, r * cameraRotate, 0));

        // Bomb Button
        if (Input.GetKey("joystick button 4") ||
            Input.GetKeyDown(KeyCode.Space))
        {
            if (bItemBomb)
            {
                CmdBombInit();
                bItemBomb = false;
                BombImage.color = Color.gray;
            }
        }

        // Wall Button
        if (Input.GetKey("joystick button 5") ||
            Input.GetKeyDown(KeyCode.Z))
        {
            if (bItemWall)
            {
                CmdWallInit();
                bItemWall = false;
                WallImage.color = Color.gray;
            }
        }

    }

    // Bomb Initalizing... Clinet -> Server
    [Command]
    void CmdBombInit()
    {
        Vector3 BPos = transform.TransformDirection(Vector3.forward);
        BPos += transform.position;
        GameObject Bomb = Instantiate(itemBomb, BPos, Quaternion.Euler(0, 0, 0));

        NetworkServer.Spawn(Bomb);

        Bomb.transform.GetChild(0).GetComponent<Bombarea>().CV = PlayerColor;
    }

    // Bomb Initalizing... Clinet -> Server
    [Command]
    void CmdWallInit()
    {
        Vector3 BPos = transform.TransformDirection(Vector3.forward);
        BPos += transform.position;
        GameObject Wall = Instantiate(itemWall, BPos, transform.rotation);

        NetworkServer.Spawn(Wall);

        //Wall.transform.GetChild(0).GetComponent<Bombarea>().CV = PlayerColor;
    }


    void WakeUp()
    {
        Debug.Log("WakeUp");
        bDamage = false;
        SuperMode();
    }

    void SuperMode()
    {
        // Super Mode ... OFF
        if (!bSuper)
        {
            Invoke("SuperMode", 1.0f);
        }
        bSuper = !bSuper;
    }


    private void AnimFunc()
    {
        if (bMoving)
        {
            Anim.SetBool("Walk", true);
            Anim.SetBool("Run", true);
        }
        else
        {
            Anim.SetBool("Walk", false);
            Anim.SetBool("Run", false);
        }
    }

    void OnTriggerEnter(Collider other)
    {
        // Get Item
        if (other.tag == "Item")
        {
            Destroy(other.gameObject);

            if (isLocalPlayer)
            {
                Ado_GetItem.Play();

                if (other.name == "Ring_Bomb(Clone)")
                {
                    bItemBomb = true;
                    BombImage.color = Color.white;
                }
                else if (other.name == "Ring_Wall(Clone)")
                {
                    bItemWall = true;
                    WallImage.color = Color.white;
                }
            }
        }
    }
}
