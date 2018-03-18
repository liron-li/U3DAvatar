using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AvatarSystem : MonoBehaviour {

    private GameObject girlSource; // 资源model
    private GameObject girlTarget; // 骨架对象
    private Transform girlSourceTrans;

    private Dictionary<string, Dictionary<string, SkinnedMeshRenderer>> girlData = new Dictionary<string, Dictionary<string, SkinnedMeshRenderer>>();

    Transform[] girlHips;

    private void Start()
    {
        this.InitSource();
        this.InitTarget();

        girlHips = girlTarget.GetComponentsInChildren<Transform>();
    }

    void InitSource()
    {
        girlSource = Instantiate(Resources.Load("FemaleModel")) as GameObject;
        girlSourceTrans = girlSource.transform;
        girlSource.SetActive(false);
    }

    void InitTarget()
    {
        girlTarget = Instantiate(Resources.Load("FemaleTarget")) as GameObject;
    }
}
