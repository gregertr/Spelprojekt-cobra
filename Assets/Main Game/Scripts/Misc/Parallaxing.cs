using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Parallaxing : MonoBehaviour
{
    public Transform[] Prefab;
    private List<Transform> backgrounds;
    public float Smoothing = 1f;

    private float[] parallaxScales;
    private new Transform camera;
    private Vector3 prevCameraPos;

    // Called before Start()
    void Awake()
    {
        camera = Camera.main.transform;
        backgrounds = new List<Transform>();

        foreach (var t in Prefab)
        {
            var transforms = t.GetComponentsInChildren(typeof(Transform));
            if (transforms.Any())
            {
                foreach (var component in transforms)
                {
                    backgrounds.Add(component.transform);
                }
            }
            else
            {
                backgrounds.Add(t.transform);
            }
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        prevCameraPos = camera.position;
        parallaxScales = new float[backgrounds.Count];

        for (int i = 0; i < backgrounds.Count; i++)
        {
            parallaxScales[i] = backgrounds[i].position.z * -1;
        }
    }

    // Update is called once per frame
    void Update()
    {
        for (int i = 0; i < backgrounds.Count; i++)
        {
            var parallax = (prevCameraPos.x - camera.position.x) * parallaxScales[i];
            var backgroundTargetX = backgrounds[i].position.x + parallax;
            var backgroundTargetPos = new Vector3(backgroundTargetX, backgrounds[i].position.y, backgrounds[i].position.z);
         
            backgrounds[i].position = Vector3.Lerp(backgrounds[i].position, backgroundTargetPos, Smoothing * Time.deltaTime);
        }

        prevCameraPos = camera.position;
    }
}
