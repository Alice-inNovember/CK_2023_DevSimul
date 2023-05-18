using UnityEngine;

namespace Script
{
    public class SpawnerRotate : MonoBehaviour
    {
        [SerializeField] private int speed = 5;
        // Start is called before the first frame update
        void Start()
        {
        
        }

        // Update is called once per frame
        void Update()
        {
            this.transform.Rotate( Vector3.forward * (speed * Time.deltaTime));
        }
    }
}
