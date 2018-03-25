using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AvatarToggle : MonoBehaviour {


    public void OnValueChange(bool isOn)
    {
        if (isOn)
        {
            string[] names = gameObject.name.Split('-');
            AvatarSystem._instance.OnChangePeople(names[0], names[1]);

            switch (names[0])
            {
                case "pants":
                    PlayAnimation("item_pants");
                    break;
                case "shoes":
                    PlayAnimation("item_boots");
                    break;
                case "top":
                    PlayAnimation("item_shirt");
                    break;
                default:
                    break;
            }
        }
    }

    public void PlayAnimation(string animName)
    {
        Animation anim = GameObject.FindWithTag("Player").GetComponent<Animation>();
        if (!anim.IsPlaying(animName))
        {
            anim.Play(animName);
            anim.PlayQueued("idle1");
        }
    }
}
