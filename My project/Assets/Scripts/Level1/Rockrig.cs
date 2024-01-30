using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rockrig : MonoBehaviour
{
    private Rigidbody2D rig;
    [SerializeField]private float Speed=100;
    [SerializeField] private float time=10;
    // Start is called before the first frame update
    void Start()
    {
        rig=gameObject.GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        rig.velocity=new Vector2(gameObject.transform.position.x * -Speed * Time.deltaTime,0);
        Destroy(gameObject, time);
    }
}
