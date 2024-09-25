using System.Collections;
using System.Collections.Generic;
using Managers;
using UnityEngine;

public class AnimationSoundController : MonoBehaviour
{
    // This is a wrapper script, since I'm playing the footstep sounds through the animation using animation events and it needs a script on the object that has the animation to work


    public void PlayFootStepSound() {
        SoundManager.Instance.PlayFootStepSound();
    }
}
