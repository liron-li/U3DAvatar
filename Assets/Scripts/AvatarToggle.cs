using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AvatarToggle : MonoBehaviour {


    public void OnValueChange(bool isOn)
    {
        if (isOn)
        {
            string[] name = gameObject.name.Split('-');
            AvatarSystem._instance.OnChangePeople(name[0], name[1]);
        }
    }
}
