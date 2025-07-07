namespace AbilityModule.StatusModule
{
    public sealed class Heal : Status
    {
        private readonly float _healPerTick;

        public Heal(string guid, float tickInterval, float healPerTick)
        {
            _guid = guid;
            _duration = tickInterval;
            _tickInterval = tickInterval;
            _healPerTick = healPerTick;
        }

        protected override void OnTick(float deltaTime)
        {
            if (_tickTimer >= _tickInterval)
            {
                _tickTimer = 0f;
                _target?.TakeHeal(_healPerTick);
            }
        }
    }
}