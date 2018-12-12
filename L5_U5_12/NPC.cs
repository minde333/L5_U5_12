namespace L5_U5_12
{
    /// <summary>
    /// Klasė skirta duomenims apie žaidėjus aprašyti
    /// </summary>
    class NPC : Player
    {
        public string Guild { get; set; } //Gildija

        /// <summary>
        /// Konstruktorius su parametrais
        /// </summary>
        /// <param name="name"></param>
        /// <param name="role"></param>
        /// <param name="hitPoints"></param>
        /// <param name="mana"></param>
        /// <param name="damage"></param>
        /// <param name="defence"></param>
        /// <param name="guild"></param>
        public NPC(string name, string role, int hitPoints, int mana, int damage, int defence, string guild)
            : base(name, role, hitPoints, mana, damage, defence)
        {
            Guild = guild;
        }

        /// <summary>
        /// Konstruktorius kuris kviečia NPCs duomenų nuskaitymą
        /// </summary>
        /// <param name="data">Duomenys</param>
        public NPC(string data)
        : base(data)
        {
            SetData(data);
        }

        /// <summary>
        /// NPCs duomenų nuskaitymas
        /// </summary>
        /// <param name="line">Eilute</param>
        public override void SetData(string line)
        {
            base.SetData(line);
            string[] values = line.Split(',');
            Guild = values[7];
        }

        /// <summary>
        /// Ieško ar veikėjo žalos taškai neviršija  nurodyto dydžio
        /// </summary>
        /// <param name="damagePoint">Nurodytas dydis</param>
        /// <returns></returns>
        public bool IsNotDamaged(int damagePoint)
        {
            if (Damage <= damagePoint)
                return true;
            return false;
        }

        /// <summary>
        /// Pakeičia ToString metodą
        /// </summary>
        /// <returns>Pakeistą ToString šabloną</returns>
        public override string ToString()
        {
            return $"{Name,-5};{Role,-10};{HitPoints,10};{Mana,10};{Damage,10};{Defence,10};{Guild,-10}";
        }
    }
}
