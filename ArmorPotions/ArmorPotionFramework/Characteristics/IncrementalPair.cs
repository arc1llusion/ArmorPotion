using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ArmorPotionFramework.Characteristics
{
    public enum IncrementalValue
    {
        None = 0,
        Quarter = 25,
        Half = 50,
        ThreeQuarters = 75,
        Full = 100
    }

    public class IncrementalPair
    {
        #region Field Region

        int currentValue;
        int maximumValue;

        #endregion

        #region Property Region

        public int CurrentValue
        {
            get { return currentValue; }
        }

        public int MaximumValue
        {
            get { return maximumValue; }
        }

        public static IncrementalPair Zero
        {
            get { return new IncrementalPair(); }
        }

        #endregion

        #region Constructor Region

        private IncrementalPair()
        {
            currentValue = 0;
            maximumValue = 0;
        }

        public IncrementalPair(IncrementalValue maxValue, int modifier)
        {
            currentValue = (int)maxValue * modifier;
            maximumValue = currentValue;
        }

        #endregion

        #region Method Region

        public void Heal(IncrementalValue value, int modifier = 1)
        {
            if (modifier < 1) modifier = 1;

            currentValue += (int)value * modifier;
            if (currentValue > maximumValue)
                currentValue = maximumValue;
        }

        public void Damage(IncrementalValue value, int modifier = 1)
        {
            if (modifier < 1) modifier = 1;

            currentValue -= (int)value * modifier;
            if (currentValue < 0)
                currentValue = 0;
        }

        public void SetCurrent(IncrementalValue value, int modifier = 1)
        {
            if (modifier < 1) modifier = 1;

            currentValue = (int)value * modifier;
            if (currentValue > maximumValue)
                currentValue = maximumValue;
        }

        public void SetMaximum(IncrementalValue value, int modifier = 1)
        {
            if (modifier < 1) modifier = 1;

            maximumValue = (int)value * modifier;
            if (currentValue > maximumValue)
                currentValue = maximumValue;
        }

        #endregion
    }
}
