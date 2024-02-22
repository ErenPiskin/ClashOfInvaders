using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimatorIcin : MonoBehaviour
{

    public Animator _Animator;
    
    public void KendiniPasiflestir()
    {
        _Animator.SetBool("ok", false);
    }
}
