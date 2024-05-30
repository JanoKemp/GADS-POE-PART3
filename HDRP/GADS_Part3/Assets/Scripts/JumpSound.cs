using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

public class JumpSound : MonoBehaviour
{
    public List<AudioClip> jumpClips;
    public AudioSource audioSource;
    public GameObject door1;
    public GameObject door2;
    public float moveDuration = 1f;

    public List<Canvas> canvases; 
    public float canvasDisplayTime = 3f; 

    private Queue<Canvas> canvasQueue; 
    

    private void Awake()
    {
        // Hide all canvases initially
        foreach (Canvas canvas in canvases)
        {
            canvas.enabled = false;
        }

        // Initialize the canvas queue
        
    }
    private void Start()
    {
        audioSource = GetComponent<AudioSource>();

        // Initialize the canvas queue
        canvasQueue = new Queue<Canvas>(canvases);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("JumpSound"))
        {
            if (jumpClips.Count > 0)
            {
                audioSource.PlayOneShot(jumpClips[Random.Range(0, jumpClips.Count)]);
            }
            else
            {
                Debug.LogWarning("No jump sound clips assigned!");
            }

            StartCoroutine(ShowNextCanvas());
        }

        if (other.CompareTag("OpenDoor"))
        {
            StartCoroutine(MoveDoors());
        }

        if (other.CompareTag("End"))
        {
            SceneManager.LoadScene(2);
        }
    }

    private IEnumerator MoveDoors()
    {
        Vector3 initialPosDoor1 = door1.transform.position;
        Vector3 initialPosDoor2 = door2.transform.position;
        Vector3 targetPosDoor1 = initialPosDoor1 + new Vector3(0, 0, -2);
        Vector3 targetPosDoor2 = initialPosDoor2 + new Vector3(0, 0, 2);

        float elapsedTime = 0f;

        while (elapsedTime < moveDuration)
        {
            float t = elapsedTime / moveDuration;

            door1.transform.position = Vector3.Lerp(initialPosDoor1, targetPosDoor1, t);
            door2.transform.position = Vector3.Lerp(initialPosDoor2, targetPosDoor2, t);

            elapsedTime += Time.deltaTime;
            yield return null;
        }

        door1.transform.position = targetPosDoor1;
        door2.transform.position = targetPosDoor2;
    }

    private IEnumerator ShowNextCanvas()
    {
        if (canvasQueue.Count > 0)
        {
            Canvas nextCanvas = canvasQueue.Dequeue();
            nextCanvas.enabled = true;

            yield return new WaitForSeconds(canvasDisplayTime);

            nextCanvas.enabled = false;
            canvasQueue.Enqueue(nextCanvas);
        }
        else
        {
            Debug.LogWarning("No canvases left in the queue!");
        }
    }
}
