using UnityEngine;
using System.Collections;

public class LegAmortization : MonoBehaviour
{
    Animator legAnimator;

    private bool shouldAmortize = false;

    [SerializeField]
    private float cooldownMax = 2f;
    private bool isOnCooldown = false;

    private float currentCooldown = 0;

    void Update()
    {
        if (currentCooldown > 0)
        {
            currentCooldown -= Time.deltaTime;
        }
        if (currentCooldown <= 0)
        {
            shouldAmortize = false;
            PerformAmortize();
        }
    }

    void Awake()
    {
        legAnimator = GetComponent<Animator>();
    }

    void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.name == "Terrain")
        {
            currentCooldown = cooldownMax;
            shouldAmortize = true;
            PerformAmortize();
            Debug.Log("collision enter");
        }
    }

    void OnTriggerStay(Collider collision)
    {
        if (collision.gameObject.name == "Terrain")
        {
            currentCooldown = cooldownMax;
        }
    }

    //void OnTriggerExit(Collider collision)
    //{
    //    if (collision.gameObject.name == "Terrain")
    //    {
    //        Debug.Log("collision exit");
    //        if (currentCooldown <= 0 && !isOnCooldown)
    //        {
    //            shouldAmortize = false;
    //            PerformAmortize();
    //            StartCooldown();
    //        }
    //    }
    //}

    void PerformAmortize()
    {
        if (shouldAmortize)
        {
            legAnimator.SetTrigger("Amortize");
            legAnimator.ResetTrigger("FinishAmortize");
        }
        else
        {
            legAnimator.SetTrigger("FinishAmortize");
            legAnimator.ResetTrigger("Amortize");
        }
    }
}
