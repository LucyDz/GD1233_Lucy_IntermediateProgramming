using UnityEngine;

public class SnakeChaseState : EnemyState
{
    private readonly SnakeBrain _brain;

    public SnakeChaseState(SnakeBrain brain, EnemyStateMachine machine) : base(machine)
    {
        _brain = brain;
    }

    public override void Tick()
    {
        // 1. Get the player's position
        var target = _brain.TargetProvider.GetTarget();
        if (target == null || !_brain.Detection.IsTargetInDetectionRange(target))
        {
            Machine.ChangeState(new SnakeIdleState(_brain, Machine));
            return;
        }

        // 2. Tell the mover to go there
        _brain.Mover?.SetDestination(target.position);

        // 3. Update animations based on movement speed
        _brain.AnimatorDriver.SetSpeed(_brain.Mover?.Velocity.magnitude ?? 0f);

        var distance = Vector3.Distance(_brain.transform.position, target.position);
        if (distance <= _brain.AttackRange)
            Machine.ChangeState(new SnakeAttackState(_brain, Machine));
    }
}
