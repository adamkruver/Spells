using System;

namespace Sources.BoundedContexts.Skills.StateMachine
{
    public class SkillStateMachine
    {
        private const float FrameDurations = 1f / 60;

        private float _activeTime;

        private uint CurrentFrame => (uint) MathF.Ceiling(_activeTime * FrameDurations);

        public void Update(float deltaTime)
        {
            _activeTime += deltaTime;
        }

        public readonly struct ActiveFrames
        {
            public byte FirstFrame { get; }
            public byte Duration { get; }
        }

        public class FrameData
        {
            public uint TotalFrames;
            public ActiveFrames[] ActiveFrames;
        }
    }
}
