using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FinishPoint : MonoBehaviour
{
    [SerializeField] private Animator Finish;
    [SerializeField] private AudioSource finishpoint;
    [SerializeField] private GameObject nextPlat;
    [SerializeField] private float time = 10f;
    private bool IsLevelComplete = false;

    void Start()
    {
        Finish = GetComponent<Animator>();
        finishpoint = GetComponent<AudioSource>();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player") && IsLevelComplete == false)
        {
            finishpoint.Play();
            Finish.enabled = true;
            IsLevelComplete = true;
            nextPlat.SetActive(true);
            Invoke("next", time);
        }
    }
    private void next()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }



}
