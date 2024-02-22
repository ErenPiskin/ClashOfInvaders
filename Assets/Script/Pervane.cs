using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pervane : MonoBehaviour
{
    public Animator _Animator;
    public float BeklemeSuresi;
    public BoxCollider _Ruzgar;
    public void AnimasyonDurum(string durum)
    {
        if (durum == "true")
        {
            _Animator.SetBool("Calistir", true);
            _Ruzgar.enabled = true;
        }
        else
        {
            _Animator.SetBool("Calistir", false);
            StartCoroutine(AnimasyonTetikle());
            _Ruzgar.enabled = false;
        } 
    }
    IEnumerator AnimasyonTetikle()
    {
        yield return new WaitForSeconds(BeklemeSuresi);
        AnimasyonDurum("true");
    }
}
