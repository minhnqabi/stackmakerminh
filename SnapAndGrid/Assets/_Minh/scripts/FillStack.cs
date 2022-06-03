using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FillStack : MonoBehaviour
{
    // Start is called before the first frame update
    bool filled = false;
    public GameObject renderFill;
    public void Fill()
    {
        this.filled = true;
        renderFill.SetActive(false);

    }
    public bool IsFilled()
    {
        return filled;
    }
}
