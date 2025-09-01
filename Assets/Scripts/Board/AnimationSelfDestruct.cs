using UnityEngine;

public class AnimationSelfDestruct : MonoBehaviour {

    // Use this for initialization
    void Start()
    {
        var time = GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).length;
        Destroy(gameObject, time);
    }
    
    
}