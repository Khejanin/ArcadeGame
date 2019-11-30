using UnityEngine;

public class MovingScript : MonoBehaviour
{

    public float speed = .5f;
    public Conveyor[] conveyors;

    void Start()
    {
        conveyors = GetComponentsInChildren<Conveyor>();
        for(int i = 0; i < conveyors.Length; i++)
        {
            conveyors[i].speed = speed;
            conveyors[i].conveyors = conveyors;
            conveyors[i].index = i;
        }
    }

    // Update is called once per frame
    void Update()
    {

    }
}
