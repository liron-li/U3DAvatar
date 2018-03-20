using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AvatarSystem : MonoBehaviour {

    private GameObject girlSource; // 资源 model
    private GameObject girlTarget; // 骨架对象
    private Transform girlSourceTrans;

    private Dictionary<string, Dictionary<string, SkinnedMeshRenderer>> girlData = new Dictionary<string, Dictionary<string, SkinnedMeshRenderer>>();

    Transform[] girlHips;
    private Dictionary<string, SkinnedMeshRenderer> girlsmr = new Dictionary<string, SkinnedMeshRenderer>();

    private void Start()
    {
        this.InitSource();
        this.InitTarget();

        girlHips = girlTarget.GetComponentsInChildren<Transform>();
        this.SavaData();
    }

    void InitSource()
    {
        girlSource = Instantiate(Resources.Load("FemaleModel")) as GameObject;
        girlSourceTrans = girlSource.transform;
        //girlSource.SetActive(false);
    }

    void InitTarget()
    {
        girlTarget = Instantiate(Resources.Load("FemaleTarget")) as GameObject;
    }

    void SavaData()
    {
        if (girlSourceTrans == null)
            return;

        SkinnedMeshRenderer[] parts = girlSourceTrans.GetComponentsInChildren<SkinnedMeshRenderer>();
        foreach (var part in parts)
        {
            string[] names = part.name.Split('-');
            Debug.Log(names);
            if (!girlData.ContainsKey(names[0]))
            {
                GameObject partGo = new GameObject();
                partGo.name = names[0];
                partGo.transform.parent = girlTarget.transform;

                girlsmr.Add(names[0], partGo.AddComponent<SkinnedMeshRenderer>());
            }
        }
    }

    void ChangeMash(string part, string num)
    {

    }

    void InitAvatar()
    {

    }
}
