using System.Collections.Generic;
using UnityEngine;

namespace Febucci.UI.Core
{
    [UnityEngine.Scripting.Preserve]
    [EffectInfo(tag: TAnimTags.bh_Irrit)]
    class IrritBehavior : BehaviorBase
    {
        private float strength = 1;
        private float peakDuration = 1;
        private float transitionDuration = 1;
        private float joltDuration = .04f;
        private float delay = 1;

        private float timePassed;
        private int randIndex;
        int lastRandomIndex;
        private float effectInfluence;

        private AnimationCurve curve;

        public override void SetDefaultValues(BehaviorDefaultValues data)
        {
            curve = data.defaults.irritCurve;
            delay = data.defaults.irritDelay;
            strength = data.defaults.irritStrength;
            peakDuration = data.defaults.irritPeakDuration;
            joltDuration = data.defaults.irritJoltDuration;
            transitionDuration = data.defaults.irritTransitionDuration;
            ClampValues();
        }

        void ClampValues()
        {
            delay = Mathf.Clamp(delay, 0.002f, 500);
        }

        public override void Initialize(int charactersCount)
        {
            base.Initialize(charactersCount);

            randIndex = Random.Range(0, TextUtilities.fakeRandomsCount);
            lastRandomIndex = randIndex;

            curve.keys = GetKeyFrames();
            curve.postWrapMode = WrapMode.Loop;
        }


        public override void SetModifier(string modifierName, string modifierValue)
        {
            switch (modifierName)
            {
                //amplitude
                case "s": ApplyModifierTo(ref strength, modifierValue); break;
                //delay
                case "d": ApplyModifierTo(ref delay, modifierValue); break;
                //peakDuration
                case "p": ApplyModifierTo(ref peakDuration, modifierValue); break;
                //transitionDuration
                case "t": ApplyModifierTo(ref transitionDuration, modifierValue); break;
            }

            ClampValues();
        }

        public override void Calculate()
        {
            timePassed += time.deltaTime;
            //Changes the shake direction if enough time passed
            if (timePassed >= joltDuration)
            {
                timePassed = 0;

                randIndex = Random.Range(0, TextUtilities.fakeRandomsCount);

                //Avoids repeating the same index twice 
                if (lastRandomIndex == randIndex)
                {
                    randIndex++;
                    if (randIndex >= TextUtilities.fakeRandomsCount)
                        randIndex = 0;
                }

                lastRandomIndex = randIndex;
            }

            effectInfluence = curve.Evaluate(time.timeSinceStart);
        }



        public override void ApplyEffect(ref CharacterData data, int charIndex)
        {
            Vector3 randomDir = TextUtilities.fakeRandoms[
                                            Mathf.RoundToInt((charIndex + randIndex) % (TextUtilities.fakeRandomsCount - 1))
                                            ] * strength * uniformIntensity;

            data.vertices.MoveChar(Vector3.Lerp(Vector3.zero, randomDir, effectInfluence));
        }

        private Keyframe[] GetKeyFrames()
        {
            Keyframe key;
            var keys = new List<Keyframe>();

            float timePos = 0;
            key = new Keyframe(timePos, 0);
            keys.Add(key);

            timePos += transitionDuration;
            key = new Keyframe(timePos, 1);
            key.inTangent = transitionDuration > 0 ? 3 / transitionDuration : 0;
            keys.Add(key);

            timePos += peakDuration;
            key = new Keyframe(timePos, 1);
            key.outTangent = transitionDuration > 0 ? -3 / transitionDuration : 0;
            keys.Add(key);

            timePos += transitionDuration;
            key = new Keyframe(timePos, 0);
            keys.Add(key);

            timePos += delay;
            key = new Keyframe(timePos, 0);
            keys.Add(key);

            return keys.ToArray();
        }
    }
}