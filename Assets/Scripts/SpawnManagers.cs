using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class SpawnManagers : MonoBehaviour
{
    public GameObject[] managers;
    public int num;

    public void Activate(int id)
    {
        managers[id].SetActive(true);
        num = id;
    }

    public void Deactivate(int num)
    {
        managers[num].SetActive(false);
    }
}
