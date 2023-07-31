using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParallaxCAM : MonoBehaviour
{
    public Transform Player;
    public Vector3 Offset;

    // Update is called once per frame
    void Update()
    {
        transform.position = Player.position + Offset;
    }
}
