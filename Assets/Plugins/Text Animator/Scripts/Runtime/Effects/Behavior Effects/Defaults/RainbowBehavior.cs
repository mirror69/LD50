using UnityEngine;

namespace Febucci.UI.Core
{
    [UnityEngine.Scripting.Preserve]
    [EffectInfo(tag: TAnimTags.bh_Rainb)]
    class RainbowBehavior : BehaviorBase
    {
        float hueShiftSpeed = 0.8f;
        float hueShiftWaveSize = 0.08f;

        public override void SetDefaultValues(BehaviorDefaultValues data)
        {
            hueShiftSpeed = data.defaults.hueShiftSpeed;
            hueShiftWaveSize = data.defaults.hueShiftWaveSize;
        }

        public override void SetModifier(string modifierName, string modifierValue)
        {
            switch (modifierName)
            {
                //frequency
                case "f": ApplyModifierTo(ref hueShiftSpeed, modifierValue); break;
                //wave size
                case "s": ApplyModifierTo(ref hueShiftWaveSize, modifierValue); break;
            }
        }

        public override void ApplyEffect(ref CharacterData data, int charIndex)
        {
            for (byte i = 0; i < data.colors.Length; i++)
            {
                var c = Mathf.PingPong(time.timeSinceStart * hueShiftSpeed + charIndex * hueShiftWaveSize, 0.9f);
                data.colors[i] = new Color(c,c,c);
            }
        }


        public override string ToString()
        {
            return $"hueShiftSpeed: {hueShiftSpeed}\n" +
                $"hueShiftWaveSize: {hueShiftWaveSize}" +
                $"\n{base.ToString()}";
        }

    }
}