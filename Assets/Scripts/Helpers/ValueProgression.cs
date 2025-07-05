using Sirenix.OdinInspector;
using UnityEngine;

namespace Helpers
{
    [System.Serializable]
    public class ValueProgression
    {
        public string Values => LogValues(6);

        private int _progressionInterval = 1;

        [HorizontalGroup(LabelWidth = 35, Width = 120)]
        [SerializeField]
        private Rarity _rarity = Rarity.Common;

        [HorizontalGroup(Title = "$Values", LabelWidth = 70)]
        [SerializeField]
        private float _startValue = 0;

        [HorizontalGroup(LabelWidth = 65)]
        [SerializeField]
        private float _addValue = 0;

        [HorizontalGroup(LabelWidth = 60)]
        [SerializeField]
        private float _multiplier = 1;

        public Rarity Rarity => _rarity;
        
        public string LogValues(int levelsAmount)
        {
            var debugText = string.Empty;

            for (int i = 0; i <= levelsAmount; i++)
            {
                debugText += $"Lv_{i + 1}: {GetValueAtLevel(i):G5}";

                if (i < levelsAmount)
                {
                    debugText += ",  ";
                }
            }

            return debugText;
        }

        public float GetValueAtLevel(int targetLevel)
        {
            if (targetLevel == 0)
            {
                return _startValue;
            }

            if (_progressionInterval <= 0)
            {
                _progressionInterval = 1;
            }

            float value = _startValue;

            for (int i = 0; i < targetLevel; i++)
            {
                if (i % _progressionInterval == 0)
                {
                    value *= _multiplier;
                    value += _addValue;
                }
            }

            return value;
        }

        public int GetLevelFromValue(float targetValue)
        {
            if (targetValue < _startValue)
            {
                Debug.LogError("Значение меньше начального значения.");
                return -1;
            }

            float value = _startValue;
            int level = 0;

            while (value <= targetValue)
            {
                level++;

                if (level % _progressionInterval == 0)
                {
                    value *= _multiplier;
                    value += _addValue;
                }

                if (value >= targetValue)
                {
                    return level;
                }
            }

            return level;
        }
    }
}
