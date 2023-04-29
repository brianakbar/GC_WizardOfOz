namespace Creazen.Wizard.Movement {
    using UnityEngine;
    using UnityEngine.Rendering;

    public class TopDownDepthSorter : MonoBehaviour {
        [SerializeField] int sortingOrderFactor = 100;

        SortingGroup sorter;

        void Awake() {
            sorter = GetComponent<SortingGroup>();
        }

        void Update() {
            sorter.sortingOrder = -1 * Mathf.RoundToInt(transform.position.y * sortingOrderFactor);
        }
    }
}