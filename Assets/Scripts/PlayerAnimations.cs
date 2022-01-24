using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimations : MonoBehaviour
{
    Animator animator;
    [SerializeField] private GameObject FailUI;
    [SerializeField] private GameObject FinishUI;
    [SerializeField] private GameObject MenLeg;
    [SerializeField] private GameObject WomenLeg;
    [SerializeField] private GameObject MenShirt;
    [SerializeField] private GameObject WomenShirt;
    [SerializeField] private GameObject EndJump;

    private bool isWomen = false;
    private bool isMen = true;
    private bool mov = false;

    private string currentState;
    private string precedentState;
    
    void ControlsSpeed(float sp)
    {
        GetComponent<PlayerMovement>().speed = sp;
    }

    void Failed()
    {
        FailUI.SetActive(true);
        animator.Play("Fail");
        ControlsSpeed(0);
    }

    void Start()
    {
        animator = GetComponent<Animator>();
        ControlsSpeed(0);
    }
    void FixedUpdate()
    {
        if (mov)
        {
            transform.position += transform.forward * 0.04f;
            StartCoroutine(ZiplineDurations());
        }
    }

    void ChangeAnimationState(string newState)
    {
        if (currentState == newState) return;
        precedentState = currentState;

        animator.Play(newState);

        currentState = newState;
    }
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Finish"))
        {
            ChangeAnimationState("Dancing");
            FinishUI.SetActive(true);
            ControlsSpeed(0f);
        }

        if (other.CompareTag("Respawn"))
        {
            ChangeAnimationState("Men_Walk");
            ControlsSpeed(2f);
        }

        if (other.CompareTag("Women_Leg"))
        {
            MenLeg.SetActive(false);
            WomenLeg.SetActive(true);
            isWomen = true;
            isMen = false;
            Destroy(other.gameObject);
            ChangeAnimationState("Women_Walk");
            ControlsSpeed(2f);
        }

        if (other.CompareTag("Men_Leg"))
        {

            WomenLeg.SetActive(false);
            MenLeg.SetActive(true);
            isMen = true;
            isWomen = false;
            Destroy(other.gameObject);
            ChangeAnimationState("Men_Walk");
            ControlsSpeed(2f);
        }

        if (other.CompareTag("Women_Shirt"))
        {
            MenShirt.SetActive(false);
            WomenShirt.SetActive(true);
            isWomen = true;
            isMen = false;
            Destroy(other.gameObject);
        }

        if (other.CompareTag("Men_Shirt"))
        {
            WomenShirt.SetActive(false);
            MenShirt.SetActive(true);
            isMen = true;
            isWomen = false;
            Destroy(other.gameObject);
        }

        if(other.CompareTag("Glass"))
        {
            if (isWomen)
            {
                ChangeAnimationState("Walk_Glass");
            }
            else
            {
                Failed();
            }
        }

        if (other.CompareTag("Start_Punsh"))
        {
            if (isMen)
            {
                ChangeAnimationState("Punch");
                StartCoroutine(CountStart());
            }
            else
            {
                Failed();
            } 
        }

        if (other.CompareTag("Start_Jump"))
        {
            if (isMen)
            {
                ChangeAnimationState("Jump");
                StartCoroutine(Advance());
            }
            else
            {
                Failed();
            }


        }

        if (other.CompareTag("Start_Zip"))
        {
            if (isWomen)
            {
                ChangeAnimationState("Take_Zip");
                mov = true;
                FixedUpdate();
            }
            else
            {
                Failed();
            }
            
        }

        if (other.CompareTag("Continue"))
        {
            ChangeAnimationState(precedentState);
        }
        if (other.CompareTag("Failed"))
        {
            Failed();
        }
    }

    IEnumerator ZiplineDurations()
    {
        yield return new WaitForSeconds(4f);
        mov = false;
    }
    IEnumerator Advance()
    {
        mov = true;
        
        yield return new WaitForSeconds(0.6f);
        FixedUpdate();
        mov = false;
        yield return new WaitForSeconds(1.5f);
        EndJump.SetActive(true);
    }
    IEnumerator CountStart()
    {
        yield return new WaitForSeconds(0.7f);
        
        ChangeAnimationState(precedentState);
    }
    // Update is called once per frame
    
}
