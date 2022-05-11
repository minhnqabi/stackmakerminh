using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManage : MonoBehaviour
{
    public Transform triggerMoveContain;
    public GameObject Up,Down,Left,Right;
    public Transform renderPlayer;
    public Animator animatorPlayer;
    
    // Start is called before the first frame update
    Vector3 direct;
    public float distanceRay;
    public float speed;
    public float highStack;
    int _layerMask=1<<8;
    public int stackCount;
    void Start()
    {
        Gizmos.color=Color.blue;
        _layerMask=~_layerMask;
        stackCount=1;
        
    }
    bool canMove=false;
    bool isMoving=false;
    Vector3 target;

    // Update is called once per frame
    void Update()
    {
        Debug.DrawRay(transform.position,direct*distanceRay,Color.green);
        if(canMove)
        {
            isMoving=true;
            transform.position=Vector3.MoveTowards(transform.position,target,speed*Time.deltaTime);
        }
        if(Physics.Raycast(transform.position,this.direct,out hit,this.distanceRay,1))
        {
            Debug.Log("Hit: "+hit.transform.name);
           if(!hit.transform.name.Contains("dimian2"))
           {
               
                  canMove=true;
                   target=hit.transform.position;
               
           }
           else
           {
               //canMove=false;
               isMoving=false;
           }
        }
    }
    RaycastHit hit;
    public void PlayerMoveControl(InputID _input)
    {
        if(!isMoving)
        {
        switch(_input)
        {
            case InputID.SWIPE_UP:
            
            direct=Vector3.forward;
            this.Up.SetActive(true);
            
            break;
             case InputID.SWIPE_DOWN:
             direct=Vector3.back;
             this.Down.SetActive(true);
            break;
             case InputID.SWIPE_LEFT:
             direct=Vector3.left;
             this.Left.SetActive(true);
            break;
             case InputID.SWIPE_RIGHT:
             direct=Vector3.right;
             this.Right.SetActive(true);
            break;
            
        }
        }
    }
    public void ResetPlayerDetechMove()
    {
      for(int i=0;i<triggerMoveContain.childCount;i++)
      {
          triggerMoveContain.GetChild(i).gameObject.SetActive(false);
      }

    }
    private void OnTriggerEnter(Collider other) {
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
        if(other.CompareTag("Block"))
        {
            if(other.GetComponent<EarnStack>())
            {
                if(!other.GetComponent<EarnStack>().earned)
                {
                other.GetComponent<EarnStack>().Earn();
                this.EarnStack();
                }
                return;
            }
            if(other.GetComponent<FillStack>())
            {

                if(!other.GetComponent<FillStack>().filled)
                {
                Debug.LogError("fill");
                other.GetComponent<FillStack>().Fill();
                this.FillStack();
                }
            }
        }

    }
    public void EarnStack()
    {   
        renderPlayer.transform.position=Vector3.Lerp(renderPlayer.position,renderPlayer.position+Vector3.up*highStack,0.5f);
        animatorPlayer.Play("Jump");
        this.stackCount++;

    }
    public void FillStack()
    {
        renderPlayer.transform.position=Vector3.Lerp(renderPlayer.position,renderPlayer.position+Vector3.down*highStack,0.5f);
        animatorPlayer.Play("Jump");
        this.stackCount--;
    }

}
