namespace L5_U5_12
{
    /// <summary>
    /// Klasė skirta duomenims apie herojus aprašyti
    /// </summary>
    class Hero : Player
    {
        public int Strength { get; set; } //Jėga
        public int Agility { get; set; } //Vikrumas
        public int Intelligence { get; set; } //Intelektas
        public string Power { get; set; } //Ypatinga galia

        /// <summary>
        /// Konstruktorius su parametrais
        /// </summary>
        /// <param name="name"></param>
        /// <param name="role"></param>
        /// <param name="hitPoints"></param>
        /// <param name="mana"></param>
        /// <param name="damage"></param>
        /// <param name="defence"></param>
        /// <param name="strength"></param>
        /// <param name="agility"></param>
        /// <param name="intelligence"></param>
        /// <param name="power"></param>
        public Hero(string name, string role, int hitPoints, int mana, int damage, int defence, int strength, int agility, int intelligence, string power)
            : base(name, role, hitPoints, mana, damage, defence)
        {
            Strength = strength;
            Agility = agility;
            Intelligence = intelligence;
            Power = power;
        }

        /// <summary>
        /// Konstruktorius kuris kviečia herojų duomenų nuskaitymą
        /// </summary>
        /// <param name="data">Duomenys</param>
        public Hero(string data)
        : base(data)
        {
            SetData(data);
        }

        /// <summary>
        /// Herojų duomenų nuskaitymas
        /// </summary>
        /// <param name="line">Eilute</param>
        public override void SetData(string line)
        {
            base.SetData(line);
            string[] values = line.Split(',');
            Strength = int.Parse(values[7]);
            Agility = int.Parse(values[8]);
            Intelligence = int.Parse(values[9]);
            Power = values[10];
        }

        /// <summary>
        /// Ieško ar veikėjo intelekto reikšmė viršija  nurodytą dydį
        /// </summary>
        /// <param name="intelligenceLimit">Nurodytas dydis</param>
        /// <returns></returns>
        public bool IsIntelligence(int intelligenceLimit)
        {
            if (Intelligence > intelligenceLimit)
                return true;
            return false;
        }

        /// <summary>
        /// Pakeičia ToString metodą
        /// </summary>
        /// <returns>Pakeistą ToString šabloną</returns>
        public override string ToString()
        {
            return $"{Name,-5};{Role,-10};{HitPoints,10};{Mana,10};{Damage,10};{Defence,10};{Strength,-10};{Agility,-10};{Intelligence,-10};{Power,10}";
        }
    }
}