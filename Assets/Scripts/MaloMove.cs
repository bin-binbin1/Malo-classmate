using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System.Security.Cryptography;

public class MaloMove : MonoBehaviour
{
    private Rigidbody2D rb;
    public float speed;
    private GameObject hold;
    public GameObject itemFather;
    private List<Transform> items = new List<Transform>();
    private List<int> currentItems = new List<int>();
    private int currentItemIndex;
    private Transform[] malodata;
    private GameObject holddata;
    private Material tempMaterial;
    public SpriteRenderer leftBar;
    private float angerBar=0;
    private bool nearWindow=false;
    public float angerDropPerSecend;
    // 初始时记录左侧 SpriteRenderer 的位置和大小
    private Vector3 leftInitialPosition;
    private Vector3 leftInitialScale;
    private Animator animator;
    private bool isinteract = false,taunt=false;
    public float interactTime;
    private Vector3 initialScale;
    public GameObject gameEnd;
    public GameObject randomAudio;
    private AudioSource[] audios;
    int isbed = 0;
    Sprite self;
    List<Vector2> pointList = new List<Vector2>();
    Vector2 org;

    void Start()
    {
       
        rb = GetComponent<Rigidbody2D>();
        rb.constraints = RigidbodyConstraints2D.FreezeRotation;
        Transform[] t=itemFather.GetComponentsInChildren<Transform>();

        for(int i=1; i<t.Length; i++)
        {
            items.Add(t[i]);
        }
        malodata = rb.GetComponentsInChildren<Transform>();
        holddata = malodata[1].gameObject;
        holddata.GetComponent<Renderer>().enabled = false;
        leftInitialPosition = leftBar.transform.localPosition;
        leftInitialScale = leftBar.transform.localScale;
        animator = GetComponent<Animator>();
        initialScale = transform.localScale;
        self= gameObject.GetComponent<SpriteRenderer>().sprite;
        audios = randomAudio.GetComponentsInChildren<AudioSource>();
       
    }

    void Update()
    {
        angerBar -= Time.deltaTime*angerDropPerSecend;
        if(angerBar < 0)
        {
            angerBar = 0;
        }
        float moveHorizontal = Input.GetAxis("Horizontal")*isbed;
        float moveVertical = Input.GetAxis("Vertical")*isbed;
        if (moveHorizontal > 0)
        {
          
            transform.localScale = new Vector3(-initialScale.x, initialScale.y, initialScale.z);
        }
        else if (moveHorizontal < 0)
        {
            transform.localScale =initialScale;
        }

        animator.SetBool("walking", moveVertical!=0||moveHorizontal!=0);

        Vector2 movement;
        if (isinteract)
        {
            movement = Vector2.zero;
        }
        else
        {
            movement= new Vector2(moveHorizontal, moveVertical);
        }
        rb.velocity = movement * speed;
        if (isbed==0&&Input.GetKeyUp(KeyCode.F))
        {
            Vector2 MoveSpeed = (new Vector3(8,0,0) - gameObject.transform.position).normalized * 10;
            //定义一个列表存放所有的计算的点
            Debug.Log("movespeed" + MoveSpeed);
            pointList.Add(gameObject.transform.position);
            Debug.Log(gameObject.transform.position);
            for (int i = 1; i < 50; i++)
            {
                float time = i * 0.02f * 5;
                float timePow = time * time;
                //下一个点
                Vector2 point = new Vector2(pointList.First().x + MoveSpeed.x * time, pointList.First().y + MoveSpeed.y * time - 0.5f * Physics2D.gravity.magnitude * timePow);
                pointList.Add(point);//加入到点的列表中

            }
            
            StartCoroutine(getup());
        }

        if (Input.GetKeyUp(KeyCode.Q))
        {
            if (hold != null)
            {
                animator.SetBool("hold", false);
                holddata.GetComponent<Renderer>().enabled = false;
                hold.SendMessage("dropItem");
                if (nearWindow)
                {
                    hold.SendMessage("dropToWindow");
                    Destroy(hold);
                }
                else
                {
                    hold.GetComponent<Renderer>().enabled = true;
                    hold.GetComponent<Collider2D>().enabled = true;
                    //丢弃物品
                    float x = this.transform.position.x;
                    float y = this.transform.position.y;
                    hold.transform.position = new Vector3(x, y, 0);
                    DontDestroyOnLoad(hold);
                }
                hold = null;
            }
            else//拿起物品
            {
                Debug.Log("currentItems.Count" + currentItems.Count);
                if (currentItems.Count > 0)
                {
                    animator.SetBool("hold", true);
                    hold = items[currentItems[currentItemIndex]].gameObject;
                    hold.SendMessage("getItem");
                    hold.GetComponent<Renderer>().enabled = false;
                    hold.GetComponent<Collider2D>().enabled = false;
                    //转换到拿起的图片

                    Texture2D tt = hold.GetComponent<SpriteRenderer>().sprite.texture;

                    holddata.GetComponent<SpriteRenderer>().sprite = Sprite.Create(tt, hold.GetComponent<SpriteRenderer>().sprite.textureRect, new Vector2(0.5f, 0.5f), 250);

                    holddata.GetComponent<Renderer>().enabled = true;
                }
            }
        }
        if(Input.GetKeyUp(KeyCode.E))
        {
            if (hold != null)
            {
                animator.SetBool("hold", false);
                holddata.GetComponent<Renderer>().enabled = false;
                //丢弃物品
                hold.SendMessage("useItem");
                float x = this.transform.position.x;
                float y = this.transform.position.y;
                hold.transform.position = new Vector3(x, y, 0);
                DontDestroyOnLoad(hold);
                hold = null;
            }
        }
        if (Input.GetKeyUp(KeyCode.I))
        {
            if(currentItems.Count > 0)
            {
                unselectItem(currentItemIndex);
                currentItemIndex = (currentItemIndex + 1) % currentItems.Count;
                selectItem(currentItemIndex);
            }
        }
        if(Input.GetKey(KeyCode.J)) {
            if (!taunt)
            {
                taunt = true;
                animator.SetBool("taunt", true);
            }
        }
        else
        {
            if(taunt)
            {
                taunt = false;
                animator.SetBool("taunt", false);
            }
        }
        
        float progress = angerBar / 100f;

        leftBar.transform.localScale = new Vector3(leftInitialScale.x * progress, leftInitialScale.y, leftInitialScale.z);
        float deltaX = (1 - progress) * leftInitialScale.x / 2;
        leftBar.transform.localPosition = new Vector3(leftInitialPosition.x - deltaX, leftInitialPosition.y , leftInitialPosition.z);
    }
    public void setCurrentItem(int itemType)
    {
        currentItems.Add(itemType);
        if(currentItems.Count == 0) {
            selectItem(0);
        }
    }
    public void releaseItem(int itemType)
    {
        currentItems.Remove(itemType);
        
        if (currentItemIndex > 0)
        {
            int t = currentItemIndex;
            currentItemIndex %= currentItems.Count;
            if (t != currentItemIndex)
            {
                unselectItem(t);
                selectItem(currentItemIndex);
            }
        }
    }
    public void angerChange(int anger)
    {
        Debug.Log(anger);
        angerBar += anger;
        if (angerBar >= 100f)
        {
            gameEnd.SetActive(true);
        }
    }
    private void unselectItem(int index)
    {
        items[currentItems[index]].GetComponent<Renderer>().material=tempMaterial;
    }
    private void selectItem(int index)
    {

        Renderer rend = items[currentItems[index]].GetComponent<Renderer>();

        Material newMaterial = new Material(rend.material);
        // 设置材质的颜色为白色
        newMaterial.color = Color.white;
        tempMaterial = rend.material;
        rend.material = newMaterial;
    }
    public void interact()
    {
        audios[Random.Range(0, audios.Length)].Play();
        //if(!animator.GetBool("walking")&&!animator.GetBool("interact")){
        //    animator.SetBool("interact", true);
        //}
        
    }
    public void wearGlass()
    {
        animator.SetBool("wearG", true);
    }
    public void gotoWindow()
    {
        nearWindow= true;
    }
    public void leaveWindow()
    {
        nearWindow = false;
    }
    public void JumptoFan(float y)
    {
        float dis = (y-transform.position.y ) / 10;
        StartCoroutine("up",dis);
    }

    public void Jumptopodium(object[] obj)
    {
        float x = (float)obj[0];
        float y = (float)obj[1];
        Vector2 pos = new Vector2(x, y);
        org=transform.position;
        transform.position=pos;
        Invoke("back", 1);

    }

    void back()
    {
        
        transform.position = org;
    }

    IEnumerator interacting()
    {
        yield return new WaitForSeconds(interactTime);
        animator.SetBool("interact", false);
    }

    IEnumerator getup()
    {
        int num = 0;

        while (true)
        {
            yield return new WaitForSeconds(0.05f);
            if (isbed==0)
            { transform.position = pointList[num];
                num++;
                animator.SetBool("getup",isbed==0);
                if (num == 11)
                {
                    
                    isbed = 1;
                    animator.SetBool("getup", false);
                    break;
                }
            }
        }
        
    }
    IEnumerator up(float dis)
    {
        Vector3 pos = transform.position;
        int num = 0;
        
        while (true)
        {
            yield return new WaitForSeconds(0.05f);
            
            num++;
            if (num < 11)
            {
                pos.y += dis;
                animator.SetBool("getup", true);
            }
            if (num == 11)
            {
                animator.SetBool("getup", false);
            }
            if (num > 11&&num<30)
            {
                Debug.Log("onfan");
            }else if (num >= 30&&num<40)
            {
                pos.y -= dis;
            }else if (num == 40)
            {
                break;
            }
            transform.position = pos;
            Debug.Log("POSY"+transform.position.y);
        }
    }


}
