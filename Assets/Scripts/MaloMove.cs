using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
        DontDestroyOnLoad(this); animator = GetComponent<Animator>();
        initialScale = transform.localScale;
    }

    void Update()
    {
        angerBar -= Time.deltaTime*angerDropPerSecend;
        if(angerBar < 0)
        {
            angerBar = 0;
        }
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");
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
        if (Input.GetKeyUp(KeyCode.Q))
        {
            if (hold != null)
            {
                
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
                    DontDestroyOnLoad(hold) ;
                }
                hold = null;
            }
            else//拿起物品
            {
                
                hold = items[currentItems[currentItemIndex]].gameObject;
                hold.SendMessage("getItem");
                Debug.Log(currentItemIndex+ hold.name);
                hold.GetComponent<Renderer>().enabled = false;
                hold.GetComponent<Collider2D>().enabled = false;
                //转换到拿起的图片

                Texture2D tt = hold.GetComponent<SpriteRenderer>().sprite.texture;

                holddata.GetComponent<SpriteRenderer>().sprite = Sprite.Create(tt, hold.GetComponent<SpriteRenderer>().sprite.textureRect, new Vector2(0.5f, 0.5f), 250);

                holddata.GetComponent<Renderer>().enabled = true;
            }
        }
        if(Input.GetKeyUp(KeyCode.E))
        {
            if (hold != null)
            {
                hold.SendMessage("useItem");
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
            //GameEnd
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

        if(!animator.GetBool("walking")&&!animator.GetBool("interact")){
            animator.SetBool("interact", true);
        }
        
    }
    public void gotoWindow()
    {
        nearWindow= true;
    }
    public void leaveWindow()
    {
        nearWindow = false;
    }
    IEnumerator interacting()
    {
        yield return new WaitForSeconds(interactTime);
        animator.SetBool("interact", false);
    }
}
