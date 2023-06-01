namespace Creazen.Wizard.ActionScheduling.Targets {
    using UnityEngine;
    using UnityEngine.AI;

    [CreateAssetMenu(fileName = "New Forward Target", menuName = "Combat/Aim/Target/Forward", order = 0)]
    public class ForwardTarget : Target {
        public override Vector3 GetTargetPosition(GameObject user) {
            if(user.TryGetComponent<NavMeshAgent>(out NavMeshAgent agent)) {
                float forward = Mathf.Sign(agent.velocity.x);
                if(agent.velocity.x == 0) {
                    forward = 0;
                }
                return new Vector3(
                    user.transform.position.x + forward,
                    user.transform.position.y,
                    user.transform.position.z
                );
            }
            return default;
        }
    }
}