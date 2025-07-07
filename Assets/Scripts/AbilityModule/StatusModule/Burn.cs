namespace AbilityModule.StatusModule
{
    public sealed class Burn : Status
    {
        private readonly float _damagePerTick;

        public Burn(string guid, float duration, float tickInterval, float damagePerTick)
        {
            _guid = guid;
            _duration = duration;
            _tickInterval = tickInterval;
            _damagePerTick = damagePerTick;
        }

        protected override void OnTick(float deltaTime)
        {
            if (_tickTimer >= _tickInterval)
            {
                _tickTimer = 0f;
                _target?.TakeDamage(_damagePerTick);
            }
        }
    }
}