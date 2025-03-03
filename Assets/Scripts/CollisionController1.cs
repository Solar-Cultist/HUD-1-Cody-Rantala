using TMPro;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CollisionController : MonoBehaviour
{
    // Let's you change the color of an object upon collision
    public bool changeColor;
    public Color myColor;
    
    // States of GameObjects to destroy them upon collision
    public bool destroyEnemy;
    public bool destroyCollectibles;
        
    // Allows you to add an audio file that's played on collision
    public AudioClip collisionAudio;
    private AudioSource audioSource;
    public float pushPower;

    public TMPro.TMP_Text scoreUI;
    public int increaseScore = 1;
    public int decreaseScore = 1; 
    
    private int score = 0;
    
    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();

    }
    
    // Update is called once per frame
    void Update()
    {
        if (scoreUI != null)
        {
            scoreUI.text = score.ToString();
        }
    }
    
    void OnCollisionEnter(Collision other)
    {
        if (changeColor == true)
        {
            gameObject.GetComponent<Renderer>().material.color = myColor;
        }
        
        if (audioSource != null && !audioSource.isPlaying)
        {
            audioSource.PlayOneShot(collisionAudio, 0.5F);
        }
        
        if ((destroyEnemy && other.gameObject.tag == "Enemy") || (destroyCollectibles && other.gameObject.tag == "Collectible"))
        {
            Destroy(other.gameObject);
        }
        // If scoreUI has an object and the GameObject collided with is tagged a "Collectible", then set score to increase by the value of increaseScore
        if (scoreUI != null && other.gameObject.tag == "Collectible")
        {
            score += increaseScore;
        }
        // If scoreUI has an object and the GameObject collides with is tagged an "Enemy", then set score to decrease by the value of decreaseScore
        if (scoreUI != null && other.gameObject.tag == "Enemy")
        {
            score -= decreaseScore;
        }
    }
    
    // only for GameObjects with a character controller applied
    void OnControllerColliderHit(ControllerColliderHit hit)
    {
        Rigidbody body = hit.collider.attachedRigidbody;
        
        // If no Rigidbody or is Kinematic, do nothing
        if (body == null || body.isKinematic)
        {
            return;
        }
        
        
        // Don't push ground or platform GameObjects below character
        if (hit.moveDirection.y < -0.3)
        {
            return;
        }
        // Calculate push direction from move direction, only along x and z axes - no vertical or y-axis pushing
        Vector3 pushDir = new Vector3(hit.moveDirection.x, 0, hit.moveDirection.z);
        
        // Apply the push power and pushing direction to the pushed object's velocity
        if (hit.gameObject.tag == "Object")
        {
            body.velocity = pushDir * pushPower;
        }
        if(audioSource != null && !audioSource.isPlaying)
        {
            audioSource.PlayOneShot(collisionAudio, 0.5F);
        }
        
        if(destroyEnemy == true && hit.gameObject.tag == "Enemy" || destroyCollectibles == true && hit.gameObject.tag == "Collectible")
        {
            Destroy(hit.gameObject);
        }
        // If scoreUI has an object and the GameObject hit is tagged a "Collectible", then set score to increase by the value of increaseScore
        if (scoreUI != null && hit.gameObject.tag == "Collectible")
        {
            score += increaseScore;
        }
        // If scoreUI has an object and the GameObject hit is tagged a "Enemy", then set score to decrease by the value of decreaseScore
if (scoreUI != null && hit.gameObject.tag == "Enemy")
{
    score -= decreaseScore;
}
    }

    
    
}