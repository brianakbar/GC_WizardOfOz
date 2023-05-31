namespace Creazen.Wizard.Combat.BehaviorTree.Action {
    using Creazen.Wizard.BehaviorTree;

    public class AttackNode : ActionNode {
        Fighter fighter;

        bool? finishedAttack = null;

        protected override void OnStart() {
            finishedAttack = null;
            if(fighter == null) fighter = gameObject.GetComponent<Fighter>();
        }

        protected override State OnUpdate() {
            if(finishedAttack == true) {
                finishedAttack = null;
                return State.Success;
            }
            if(finishedAttack == false) return State.Running;

            if(!fighter.StartAttack(() => finishedAttack = true)) return State.Failure;

            finishedAttack = false;
            return State.Running;
        }
    }
}
