using UnityEngine;

public class Explosion : MonoBehaviour {

    // Use this for initialization
    void Start()
    {
        var time = GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).length;
        GetComponent<AudioSource>().Play();
        Destroy(gameObject, time);
    }
    
    
}