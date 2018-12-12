namespace L5_U5_12
{
    /// <summary>
    /// Klasė, skirta duomenims apie filialus  aprašyti
    /// </summary>
    class Branch
    {
        public const int MaxNumberOfPlayers = 100; //Maksimalus žaidėjų skaičius
        public string Race { get; set; } //Rasė
        public string Town { get; set; } //Miestas
        public HeroContainer Heroes { get; set; } //Herojų konteineris
        public NPCContainer NPCs { get; set; } //NPCs konteineris

        /// <summary>
        /// Filialo konstruktorius
        /// </summary>
        public Branch()
        {
            Heroes = new HeroContainer();
            NPCs = new NPCContainer();
        }

        /// <summary>
        /// Filialo konstruktorius su parametrais
        /// </summary>
        /// <param name="race">Rasė</param>
        /// <param name="town">Miestas</param>
        public Branch(string race, string town)
        {
            Race = race;
            Town = town;
            Heroes = new HeroContainer();
            NPCs = new NPCContainer();
        }

        /// <summary>
        /// Prideda herojų į herojų konteinerį
        /// </summary>
        /// <param name="hero">Herojus</param>
        public void AddHero(Hero hero)
        {
            Heroes.AddHero(hero);
        }

        /// <summary>
        /// Prideda NPC į NPCs konteinerį
        /// </summary>
        /// <param name="nPC">NPC</param>
        public void AddNPC(NPC nPC)
        {
            NPCs.AddNPC(nPC);
        }
    }
}
