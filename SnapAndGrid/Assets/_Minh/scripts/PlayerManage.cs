using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManage : SingletonMonoBehaviour<PlayerManage>
{
    public Transform triggerMoveContain;
    public GameObject Up, Down, Left, Right;
    public Transform renderPlayer;
    public Animator animatorPlayer;

    // Start is called before the first frame update
    Vector3 direct;
    public float distanceRay;
    public float speed;
    public float highStack;
    int _layerMask = 1 << 8;
    public int stackCount;
    Vector3 renderOriginPost;
    private Transform trans;
    void Start()
    {
        Gizmos.color = Color.blue;
        _layerMask = ~_layerMask;
        stackCount = 1;
        renderOriginPost = renderPlayer.transform.position;
        trans = transform;
        //this.PlayerWin();
    }
    bool canMove = false;
    bool isMoving = false;
    Vector3 target;
    bool canJump = true;


    // Update is called once per frame
    void Update()
    {
        Debug.DrawRay(trans.position, direct * distanceRay, Color.green);
        if (canMove)
        {
            isMoving = true;
            trans.position = Vector3.MoveTowards(trans.position, target, speed * Time.deltaTime);
        }
        if (Physics.Raycast(trans.position, this.direct, out hit, this.distanceRay, 1))
        {
            Debug.Log("Hit: " + hit.transform.name);
            if (!hit.transform.CompareTag(MConfig.TAG_WALL))
            {

                canMove = true;
                target = hit.transform.position;
                canJump = true;

            }
            else
            {
                //canMove=false;
                isMoving = false;
                if (canJump)
                {
                    animatorPlayer.Play(MConfig.ANIM_JUMP);
                    canJump = false;
                }
            }
        }
    }
    RaycastHit hit;
    public void PlayerMoveControl(InputID _input)
    {
        if (!isMoving)
        {
            switch (_input)
            {
                case InputID.SWIPE_UP:

                    direct = Vector3.forward;
                    //this.Up.SetActive(true); Move by trigger

                    break;
                case InputID.SWIPE_DOWN:
                    direct = Vector3.back;
                    //this.Down.SetActive(true);
                    break;
                case InputID.SWIPE_LEFT:
                    direct = Vector3.left;
                   // this.Left.SetActive(true);
                    break;
                case InputID.SWIPE_RIGHT:
                    direct = Vector3.right;
                    //this.Right.SetActive(true);
                    break;

            }
        }
    }

    //Move by trigger
    public void ResetPlayerDetechMove()
    {
        for (int i = 0; i < triggerMoveContain.childCount; i++)
        {
            triggerMoveContain.GetChild(i).gameObject.SetActive(false);
        }

    }
    private void OnTriggerEnter(Collider other)
    {
        //Move by trigger
        // if(other.CompareTag("Block"))
        // {
        //     if(other.GetComponent<BlockManage>().canMove)
        //     {
        //        transform.position=Vector3.Lerp(transform.position,other.transform.position,1.0f);
        //     }
        //     else
        //     {
        //         this.ResetPlayerDetechMove();

        //     }

        // }
        if (other.CompareTag(MConfig.TAG_BLOCK))
        {
            
            if (other.GetComponent<EarnStack>())
            {
                EarnStack objEarnStack=other.GetComponent<EarnStack>();
                if (!objEarnStack.IsEarned())
                {
                    objEarnStack.Earn();
                    this.EarnStack();
                }
                return;
            }
            if (other.GetComponent<FillStack>())
            {
                FillStack objFillStack=other.GetComponent<FillStack>();

                if (!objFillStack.IsFilled())
                {
                    // Debug.LogError("fill");
                    objFillStack.Fill();
                    this.FillStack();
                }
            }
        }
        if (other.transform.CompareTag(MConfig.TAG_Wintrigger))
        {
            this.PlayerWin();
        }

    }
    public void EarnStack()
    {
        //Debug.LogError("earn");
        renderPlayer.transform.position = Vector3.Lerp(renderPlayer.position, renderPlayer.position + Vector3.up * highStack, 0.5f);
        // animatorPlayer.Play("Jump");
        this.stackCount++;

    }
    public void FillStack()
    {
        renderPlayer.transform.position = Vector3.Lerp(renderPlayer.position, renderPlayer.position + Vector3.down * highStack, 0.5f);
        //animatorPlayer.Play("Jump");
        this.stackCount--;
    }
    public void PlayerWin()
    {
        animatorPlayer.Play(MConfig.ANIM_DANCE);
        animatorPlayer.transform.rotation = Quaternion.Euler(180, -180, -180);
        renderPlayer.position = new Vector3(renderPlayer.position.x, this.renderOriginPost.y, renderPlayer.position.z);
        MConfig.UpdateLevelUp();
        StartCoroutine(ActiveEndGame());

    }
    IEnumerator ActiveEndGame()
    {
        yield return new WaitForSeconds(2f);
        ManageUI.instance.ShowPanelEndGame();
    }

}
