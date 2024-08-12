using NavMeshPlus.Components;
using NavMeshPlus.Extensions;
using System.Collections;
using UnityEngine;
using UnityEngine.AI;
using static Tools;

public class TestManager : MonoBehaviour
{
    public GameObject TestEnemy;
    public GameObject DropTest;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.H)) {
            var gameobj = Instantiate(TestEnemy);
            gameobj.transform.position = getMousePointV2();
        }
        if (Input.GetKeyDown(KeyCode.N)) {
            for(int i = 0; i < 7; i++) {
                var gameobj = Instantiate(DropTest);
                gameobj.transform.position = getMousePointV2();
            }
        }
    }
}
