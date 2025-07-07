using EntityModule;
using System;

namespace AbilityModule.StatusModule
{
    public abstract class Status
    {
        protected string _guid;
        protected float _duration;
        protected float _timer;
        protected Entity _target;
        protected float _tickInterval;
        protected float _tickTimer;

        public string Guid => _guid;
        public Entity Target => _target;

        public event Action<Status> OnEnd;

        public virtual void OnApply(Entity target)
        {
            _target = target;
            _timer = 0f;
            _tickTimer = 0f;
        }

        public virtual void OnRefresh()
        {
            _timer = 0f;
        }

        public void UpdateLogic(float deltaTime)
        {
            _timer += deltaTime;
            _tickTimer += deltaTime;

            OnTick(deltaTime);

            if (_timer >= _duration)
            {
                OnEnd?.Invoke(this);
            }
        }

        protected abstract void OnTick(float deltaTime);
    }
}