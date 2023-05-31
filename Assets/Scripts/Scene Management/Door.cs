namespace Creazen.Wizard.SceneManagement {
    using Creazen.Wizard.BehaviorTree;
    using Creazen.Wizard.Control;
    using UnityEngine;
    
    public class Door : MonoBehaviour {
        void OnCollisionEnter2D(Collision2D other) {
            if(!other.collider.CompareTag("Feet")) return;
            if(other.collider.GetComponentInParent<PlayerController>() == null) return;

            if(FindObjectOfType<BehaviorTreeAgent>()) return;

            FindObjectOfType<LevelManager>().LoadNextLevel();
        }
    }
}