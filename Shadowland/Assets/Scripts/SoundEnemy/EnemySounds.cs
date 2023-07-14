using CUAS.MMT;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySounds : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void BearAttack()
    {
        SoundManager.Instance.PlaySound(SoundManager.Sound.BearAttack);
    }


    public void BearImpact()
    {
        SoundManager.Instance.PlaySound(SoundManager.Sound.BearImpact);

    }

    public void SkeletonAttack()
    {
        SoundManager.Instance.PlaySound(SoundManager.Sound.SkeletonAttack);
    }



    public void SkeletonImpact()
    {
        SoundManager.Instance.PlaySound(SoundManager.Sound.SkeletonImpact);

    }

    public void MetalonAttack()
    {
        SoundManager.Instance.PlaySound(SoundManager.Sound.MetalonAttack);
    }

    public void MetalonImpact()
    {
        SoundManager.Instance.PlaySound(SoundManager.Sound.MetalonImpact);

    }


}
