using System.Linq;

namespace L5_U5_12
{
    /// <summary>
    /// Herojų konteineris
    /// </summary>
    class HeroContainer
    {
        private const int MaxHeroes = 100;
        Hero[] Heroes;
        public int Count { get; private set; }

        /// <summary>
        /// Konstruktorius
        /// </summary>
        public HeroContainer()
        {
            Heroes = new Hero[MaxHeroes];
            Count = 0;
        }

        /// <summary>
        /// Prideda herojus į masyvą
        /// </summary>
        /// <param name="hero"></param>
        public void AddHero(Hero hero)
        {
            Heroes[Count++] = hero;
        }

        /// <summary>
        /// Paimas herojus iš masyvo pagal nurodytą indeksą
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>
        public Hero GetHero(int index)
        {
            return Heroes[index];
        }

        /// <summary>
        /// Ieško ar masyve yra nurodytas herojus
        /// </summary>
        /// <param name="hero"></param>
        /// <returns></returns>
        public bool Contains(Hero hero)
        {
            return Heroes.Contains(hero);
        }

        /// <summary>
        /// Išrikiuoja herojus
        /// </summary>
        public void SortHeroes()
        {
            for (int i = 0; i < Count - 1; i++)
            {
                var minValueHero = Heroes[i];
                int minValueIndex = i;
                for (int j = i + 1; j < Count; j++)
                {
                    if (Heroes[j] <= minValueHero)
                    {
                        minValueHero = Heroes[j];
                        minValueIndex = j;
                    }
                }
                Heroes[minValueIndex] = Heroes[i];
                Heroes[i] = minValueHero;
            }
        }

        /// <summary>
        /// Išrikiuoja herojus pagal intelekto dydį
        /// </summary>
        public void SortHeroesByIntelligence()
        {
            for (int i = 0; i < Count; i++)
            {
                for (int j = 0; j < Count; j++)
                {
                    if (Heroes[i].Intelligence >= Heroes[j].Intelligence)
                    {
                        var temp = Heroes[i];
                        Heroes[i] = Heroes[j];
                        Heroes[j] = temp;
                    }
                }
            }
        }
    }
}
