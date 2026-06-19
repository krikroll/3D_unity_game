using UnityEngine;
using UnityEngine.SceneManagement;

public class Death : MonoBehaviour
{
    bool dead = false;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Enemy Body"))
        {
            Reset();
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            Reset();
        }
        if (transform.position.y < -1f)
        {
            Reset();
        }
    }

    void Reset()
    {
        if (!dead)
        {
            dead = true;
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
            Debug.Log("Died");
        }
    }
}
