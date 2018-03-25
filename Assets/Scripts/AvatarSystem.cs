using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AvatarSystem : MonoBehaviour {

    public static AvatarSystem _instance;

    // 原始的模型transform
    private Transform originGirlSourceTrans;
    // 新模型对象
    private GameObject girlTarget;
    // 小女孩所有的资源信息   //部位的名字，部位编号，部位对应的skm
    private Dictionary<string, Dictionary<string, SkinnedMeshRenderer>> girlData = new Dictionary<string, Dictionary<string, SkinnedMeshRenderer>>();
    // 部位的名字，部位对应的skm
    private Dictionary<string, SkinnedMeshRenderer> girlSmr = new Dictionary<string, SkinnedMeshRenderer>();// 换装骨骼身上的skm信息
    //部位的名字，部位对应的skm
    private string[,] girlStr = new string[,] { { "eyes", "1" }, { "hair", "1" }, { "top", "1" }, { "pants", "1" }, { "shoes", "1" }, { "face", "1" } };

    Transform[] girlHips; //小女孩骨骼信息

    void Awake()
    {
        _instance = this;
        DontDestroyOnLoad(this); //不删除游戏物体
    }

    private void Start()
    {
        this.InstantiateGirl();
        SaveData(originGirlSourceTrans, girlData, girlTarget, girlSmr);
        InitAvatarGirl();
    }

    void InitSource()
    {

    }

    void SaveData(Transform souceTrans, Dictionary<string, Dictionary<string, SkinnedMeshRenderer>> data,
        GameObject target, Dictionary<string, SkinnedMeshRenderer> smr)
    {

        data.Clear();
        smr.Clear();

        SkinnedMeshRenderer[] parts = souceTrans.GetComponentsInChildren<SkinnedMeshRenderer>();
        foreach(var part in parts)
        {
            string[] names = part.name.Split('-');
            if (!data.ContainsKey(names[0]))
            {
                GameObject partGo = new GameObject();
                partGo.name = names[0];
                partGo.transform.parent = target.transform;

                smr.Add(names[0], partGo.AddComponent<SkinnedMeshRenderer>());
                data.Add(names[0], new Dictionary<string, SkinnedMeshRenderer>());
            }
            data[names[0]].Add(names[1], part);
        }
    }

    void ChangeMesh(string part, string num, Dictionary<string, Dictionary<string, SkinnedMeshRenderer>> data,
        Transform[] hips, Dictionary<string, SkinnedMeshRenderer> smr, string[,] str)
    { //传入部位，编号，从data里边拿取对应的skm 

        SkinnedMeshRenderer skm = data[part][num];//要更换的部位

        List<Transform> bones = new List<Transform>();
        foreach (var trans in skm.bones)
        {
            foreach (var bone in hips)
            {
                if (bone.name == trans.name)
                {
                    bones.Add(bone);
                    break;
                }
            }
        }
        //换装实现
        smr[part].bones = bones.ToArray();//绑定骨骼
        smr[part].materials = skm.materials;//替换材质
        smr[part].sharedMesh = skm.sharedMesh;//更换mesh
    }

    void InstantiateGirl()
    {
        // 实例化初始任务模型
        GameObject go = Instantiate(Resources.Load("FemaleModel")) as GameObject;
        // 保存原始模型transform
        this.originGirlSourceTrans = go.transform;
        // 禁用模型
        go.SetActive(false);
        girlTarget = Instantiate(Resources.Load("FemaleTarget")) as GameObject;
        girlHips = girlTarget.GetComponentsInChildren<Transform>();
        girlTarget.SetActive(false);
        Invoke("EnableModel", 1.0f);
    }

    void EnableModel()
    {
        girlTarget.SetActive(true);
    }

    void InitAvatarGirl()
    {
        // 获得行数
        int length = this.girlStr.GetLength(0);
        for (int i = 0; i < length; i++)
        {
            ChangeMesh(girlStr[i, 0], girlStr[i, 1], girlData, girlHips, girlSmr, girlStr); //穿上衣服
        }
    }

    public void OnChangePeople(string part, string num)
    {
        Debug.Log(part);
        Debug.Log(num);
        this.ChangeMesh(part, num, girlData, girlHips, girlSmr, girlStr);
    }
}
