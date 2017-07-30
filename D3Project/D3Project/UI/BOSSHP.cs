namespace D3Project.UI
{
    static class BOSSHP
    {
        private static float maxValue;              //最大値
        private static float currentValue;          //現在の値

        static BOSSHP()
        {
            maxValue = 2000;
            currentValue = 2000;

        }

        public static void Initialize() { currentValue = maxValue; }

        public static void SubValue(float Damage)
        {
            currentValue -= Damage;
        }

        public static float GetHP()
        {
            return (currentValue / maxValue) * 100;
        }
    }
}
