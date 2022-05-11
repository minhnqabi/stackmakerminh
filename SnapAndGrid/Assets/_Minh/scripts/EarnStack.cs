using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EarnStack : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject renderEarn;
  public  bool earned=false;
  public void Earn()
  {
      earned=true;
      renderEarn.SetActive(false);
  }
   
}
