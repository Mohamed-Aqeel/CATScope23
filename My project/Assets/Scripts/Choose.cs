using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Choose : MonoBehaviour
{
    public GameObject ChoosenOne;
    private int n = 0;
    private void OnMouseEnter()
    {
        n =+ n;
        
            if (n == 1)
            {
                ChoosenOne.SetActive(true);
            }
            else if (n > 1 || n < 0)
            {
            ChoosenOne.SetActive(false);
            n = 0;
            }
    }
   
}
