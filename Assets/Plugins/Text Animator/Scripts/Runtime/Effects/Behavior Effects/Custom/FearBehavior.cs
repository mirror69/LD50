using System.Collections.Generic;
using UnityEngine;

namespace Febucci.UI.Core
{
    [UnityEngine.Scripting.Preserve]
    [EffectInfo(tag: TAnimTags.bh_Fear)]
    class FearBehavior : BehaviorBase
    {
        private float strength = 1;
        private float peakDuration = 1;
        private float transitionDuration = 1;
        private float joltDuration = 0.1f;
        private float delay = 1;

        private AnimationCurve curve;
        private float halfJoltDuration;
        private float tiltValue;

        public override void Initialize(int charactersCount)
        {
            base.Initialize(charactersCount);
            halfJoltDuration = 0.5f * joltDuration;

            curve.keys = GetKeyFrames();
            curve.postWrapMode = WrapMode.Loop;
        }

        public override void SetModifier(string modifierName, string modifierValue)
        {
            switch (modifierName)
            {
                //strength of fear
                case "s": ApplyModifierTo(ref strength, modifierValue); break;
                //peak strength value duration
                case "p": ApplyModifierTo(ref peakDuration, modifierValue); break;
                //delay between visualization cycles
                case "d": ApplyModifierTo(ref delay, modifierValue); break;
                //duration of transition from normal text to tilted text and back
                case "t": ApplyModifierTo(ref transitionDuration, modifierValue); break;
                //duration of one jolt on max fear strength
                case "j": ApplyModifierTo(ref joltDuration, modifierValue); break;
            }
        }

        public override void SetDefaultValues(BehaviorDefaultValues data)
        {
            curve = data.defaults.fearCurve;
            strength = data.defaults.fearStrength;
            peakDuration = data.defaults.fearPeakDuration;
            transitionDuration = data.defaults.fearTransitionDuration;
            joltDuration = data.defaults.fearJoltDuration;
            delay = data.defaults.fearDelay;
        }

        public override void Calculate()
        {
            tiltValue = curve.Evaluate(time.timeSinceStart) * strength;
        }

        public override void ApplyEffect(ref CharacterData data, int charIndex)
        {
            data.vertices[1] += Vector3.right * tiltValue;
            data.vertices[2] += Vector3.right * tiltValue;
        }

        private Keyframe[] GetKeyFrames()
        {
            const float EasingTangent = 2;
            const float JoltKeyHeight = 0.75f;
            
            int joltKeyCount = joltDuration > 0 ? (int)(peakDuration / joltDuration) : 0;
            float edgeJoltKeyTimeShift = joltKeyCount > 0 ? 0.5f * (peakDuration - joltDuration * joltKeyCount) : 0;

            Keyframe key;
            var keys = new List<Keyframe>();

            float timePos = 0;
            key = new Keyframe(timePos, 0);
            keys.Add(key);

            timePos += transitionDuration;
            key = new Keyframe(timePos, 1);
            key.inTangent = transitionDuration > 0 ? EasingTangent / transitionDuration : 0;
            keys.Add(key);

            if (joltKeyCount > 0)
            {
                for (int i = 0; i < joltKeyCount; ++i)
                {
                    timePos += i == 0 ? edgeJoltKeyTimeShift : halfJoltDuration;
                    key = new Keyframe(timePos, JoltKeyHeight);
                    keys.Add(key);

                    timePos += i == joltKeyCount - 1 ? edgeJoltKeyTimeShift : halfJoltDuration;
                    key = new Keyframe(timePos, 1);
                    keys.Add(key);
                }
            }
            else
            {
                timePos += peakDuration;
                key = new Keyframe(timePos, 1);
                keys.Add(key);
            }

            timePos += transitionDuration;
            key = new Keyframe(timePos, 0);
            key.inTangent = transitionDuration > 0 ? -EasingTangent / transitionDuration : 0;
            keys.Add(key);

            timePos += delay;
            key = new Keyframe(timePos, 0);
            keys.Add(key);

            return keys.ToArray();
        }
    }
}