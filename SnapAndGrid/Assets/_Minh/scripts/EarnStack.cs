using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EarnStack : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject renderEarn;
    bool earned = false;
    public void Earn()
    {
        earned = true;
        renderEarn.SetActive(false);
    }
    public bool IsEarned()
    {
        return earned;
    }

}
