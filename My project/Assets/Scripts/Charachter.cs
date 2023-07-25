using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Charachter : MonoBehaviour
{ public int PlayerChar=0;
    public GameObject Char0;
    public GameObject Char1;
    public GameObject Char2;
    public GameObject Char3;
    public GameObject Char4;
    public GameObject hide;



    public void Button0()
  {
     Char0.GetComponent<SpriteRenderer>().enabled = true;
     Char1.GetComponent<SpriteRenderer>().enabled = false;
     Char2.GetComponent<SpriteRenderer>().enabled = false;
     Char3.GetComponent<SpriteRenderer>().enabled = false;
     Char4.GetComponent<SpriteRenderer>().enabled = false;
     hide.SetActive(false);

     Char0.GetComponent<PlayerMovement>().enabled = true;
     Char0.GetComponent<cameraController>().enabled = false;
}
        public void Button1()
        {
            Char0.GetComponent<SpriteRenderer>().enabled = false;
            Char1.GetComponent<SpriteRenderer>().enabled = true;
            Char2.GetComponent<SpriteRenderer>().enabled = false;
            Char3.GetComponent<SpriteRenderer>().enabled = false;
            Char4.GetComponent<SpriteRenderer>().enabled = false;
            hide.SetActive(false);

            Char1.GetComponent<PlayerMovement>().enabled = true;
            Char1.GetComponent<cameraController>().enabled = false;
        }
        public void Button2()
        {
            Char0.GetComponent<SpriteRenderer>().enabled = false;
            Char1.GetComponent<SpriteRenderer>().enabled = false;
            Char2.GetComponent<SpriteRenderer>().enabled = true;
            Char3.GetComponent<SpriteRenderer>().enabled = false;
            Char4.GetComponent<SpriteRenderer>().enabled = false;
            hide.SetActive(false);

            Char2.GetComponent<PlayerMovement>().enabled = true;
            Char2.GetComponent<cameraController>().enabled = false;
        }
        public void Button3()
        {
            Char0.GetComponent<SpriteRenderer>().enabled = false;
            Char1.GetComponent<SpriteRenderer>().enabled = false;
            Char2.GetComponent<SpriteRenderer>().enabled = false;
            Char3.GetComponent<SpriteRenderer>().enabled = true;
            Char4.GetComponent<SpriteRenderer>().enabled = false;
            hide.SetActive(false);

            Char3.GetComponent<PlayerMovement>().enabled = true;
            Char3.GetComponent<cameraController>().enabled = false;
        }
        public void Button4()
        {
            Char0.GetComponent<SpriteRenderer>().enabled = false;
            Char1.GetComponent<SpriteRenderer>().enabled = false;
            Char2.GetComponent<SpriteRenderer>().enabled = false;
            Char3.GetComponent<SpriteRenderer>().enabled = false;
            Char4.GetComponent<SpriteRenderer>().enabled = true;
            hide.SetActive(false);
 
            Char4.GetComponent<PlayerMovement>().enabled = true;
            Char4.GetComponent<cameraController>().enabled = false;
        }
    
}
