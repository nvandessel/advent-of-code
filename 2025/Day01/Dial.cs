using System;

namespace adventofcode.Y2025.D01
{
    public class Dial(int start, int min, int max, int tracked)
    {
        public int PartOneTracker { get; private set; }
        public int PartTwoTracker { get; private set; }
        
        private readonly int _size = max - min + 1;
        private int _currentValue = start;

        public void Rotate(int delta)
        {
            var previous = _currentValue;
            var fullRotations = Math.Abs(delta) / _size;
            var remainder = delta % _size;
            
            PartTwoTracker += fullRotations;
            _currentValue = ((_currentValue - min + remainder) % _size + _size) % _size + min;
            
            if (Crossed(previous, _currentValue, tracked, delta))
                PartTwoTracker++;
            
            if (_currentValue == tracked)
                PartOneTracker++;
        }

        private bool Crossed(int previous, int current, int target, int delta)
        {
            if (delta == 0) return false;

            var previousNormalized = previous - min;
            var currentNormalized = current - min;
            var targetNormalized = target - min;

            if (delta > 0)
            {
                // Clockwise rotation
                var travelled = (currentNormalized - previousNormalized + _size) % _size;
                var distanceToTarget = (targetNormalized - previousNormalized + _size) % _size;
                return distanceToTarget > 0 && travelled >= distanceToTarget;
            }
            else
            {
                // Counter-clockwise rotation
                var travelled = (previousNormalized - currentNormalized + _size) % _size;
                var distanceToTarget = (previousNormalized - targetNormalized + _size) % _size;
                return distanceToTarget > 0 && travelled >= distanceToTarget;
            }
        }
    }
}