using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityStandardAssets.CrossPlatformInput;

public class PlayerMovement : MonoBehaviour
{
    public float speed = 2f;
    [SerializeField] private GameObject Score;
    [SerializeField] private float direction;
    [SerializeField] private AudioSource snd;
    private int p;
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 move = new Vector3(CrossPlatformInputManager.GetAxis("Horizontal") * direction, 0, 0);
        transform.Translate(move * Time.deltaTime * speed);
        //transform.Rotate(move * 0 * Time.deltaTime);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Collects"))
        {
            p++;
            
            Destroy(other.gameObject);
            snd.Play();
            Score.GetComponent<Text>().text = p.ToString();
            StartCoroutine(CountStart());
            
        }
    }

    IEnumerator CountStart()
    {
        Score.SetActive(false);
        yield return new WaitForSeconds(0.01f);
        Score.SetActive(true);
    }
}
