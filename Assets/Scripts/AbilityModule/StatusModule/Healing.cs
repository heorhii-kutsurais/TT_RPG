namespace AbilityModule.StatusModule
{
    public sealed class Healing : Status
    {
        private readonly float _healPerTick;

        public Healing(string guid, float duration, float tickInterval, float healPerTick)
        {
            _guid = guid;
            _duration = duration;
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