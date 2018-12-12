using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectsPool : MonoBehaviour
{
    //Пул объектов для улучшения производительности

    public static ObjectsPool Instanse { get; private set; }

    [SerializeField] private GameObject[] objects;

    private Dictionary<string, Queue<IPoolable>> objectsDic = new Dictionary<string, Queue<IPoolable>>();

    private void Awake()
    {
        // проверка один ли пул на сцене
        if(Instanse)
        {
            DestroyImmediate(this);
        }
        else
        {
            Instanse = this;
        }
    }

    private void Start()
    {
        foreach(var o in objects)
        {
            IPoolable poolObj = o.GetComponent<IPoolable>();
            if(poolObj == null)
            {
                continue;
            }
            Queue<IPoolable> queue = new Queue<IPoolable>();
            for(int i = 0; i < poolObj.ObjectsCount; i++)
            {
                GameObject go = Instantiate(o);
                go.SetActive(false);
                queue.Enqueue(go.GetComponent<IPoolable>());
            }

            objectsDic.Add(poolObj.PoolId, queue);
            
        }
    }

    public IPoolable GetObject(string poolId)
    {
        if(string.IsNullOrEmpty(poolId)) // если строка с пул Id пустая
        {
            return null;
        }
         
        if(!objectsDic.ContainsKey(poolId)) //если нет такого пул Id
        {
            return null;
        }
        IPoolable p = objectsDic[poolId].Dequeue();
        objectsDic[poolId].Enqueue(p);

        return p;
    }
}
