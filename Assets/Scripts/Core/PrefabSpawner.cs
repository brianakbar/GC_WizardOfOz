namespace Creazen.Wizard.Core {
    using UnityEngine;
    
    public class PrefabSpawner : MonoBehaviour {
        [SerializeField] GameObject prefabToSpawn;
        [SerializeField] Vector3 offset;

        public GameObject Spawn() {
            return Instantiate(prefabToSpawn, transform.position + offset, Quaternion.identity);
        }
    }
}