using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace L5_U5_12
{
    class Program
    {
        public const int TankHealth = 100; //Tanko gyvybės pagal kurias suranda tanka
        public const int TankDefence = 30; // Tanko gynyba pagal kurias suranda tanka

        static void Main(string[] args)
        {
            Console.ReadKey();
            Console.OutputEncoding = Encoding.UTF8; //Konsolėje rašo lietuviškas raides
            Program p = new Program();
            const string DataDir = @"..\..\Data";
            BranchContainer branchContainer = new BranchContainer();
            p.ReadData(DataDir, ref branchContainer);
            CreateReportTable(branchContainer, "ReportTable.txt");
            PrintMostPopularRole(branchContainer);
            Console.WriteLine("2.Išspausdina pasikartojančius veikėjų vardus į Klaidos.csv");
            WriteFilteredPlayersData(branchContainer, "Klaidos.csv");
            Console.ReadKey();
            Console.WriteLine();
            Console.WriteLine("3.Išspausdina tankus pagal gyvybės ir gynybos taškus į Tankai.csv");
            PrintTanks(branchContainer, "Tankai.csv");
            Console.WriteLine();
            int intelligenceLimit = ReadInt("4.Įveskite sveikąją reikšmę, kuria norėtumete, kad Herojai viršytų intelekto dydį: ");
            int damagePoint = ReadInt("4.Įveskite sveikąją reikšmę, kuria norėtumėte, kad NPCs neviršytų žalos taškų dydžio: ");
            Console.WriteLine("4.Išspausdina rinktinę pagal Herojų intelektą ir NPCs žalos taškus į Rinktine.csv");
            PrintGeneralSelection(branchContainer, "Rinktine.csv", intelligenceLimit, damagePoint);
            Console.ReadKey();
        }

        /// <summary>
        /// Skaito veikėjų duomenis iš failo
        /// </summary>
        /// <param name="file">Failas</param>
        /// <param name="branchContainer">Filialų konteineris</param>
        private void ReadData(string file, ref BranchContainer branchContainer)
        {
            string[] filePaths = Directory.GetFiles(file, "*.csv");

            foreach (string path in filePaths)
            {
                branchContainer.AddBranch(ReadPlayerData(path));
            }
        }

        /// <summary>
        /// Skaito veikejų duomenis iš failo
        /// </summary>
        /// <param name="file">Failas</param>
        /// <returns>Filialą</returns>
        private static Branch ReadPlayerData(string file)
        {
            Branch branch;

            using (StreamReader reader = new StreamReader(@file, Encoding.GetEncoding(1257)))
            {
                var line = reader.ReadLine();
                var race = line;
                var city = reader.ReadLine();
                line = reader.ReadLine();
                branch = new Branch(race, city);

                while (line != null)
                {
                    switch (line[0])
                    {
                        case 'H':
                            branch.Heroes.AddHero(new Hero(line));
                            break;
                        case 'N':
                            branch.NPCs.AddNPC(new NPC(line));
                            break;
                    }
                    line = reader.ReadLine();
                }
            }
            return branch;
        }

        /// <summary>
        /// Išspausdina žaidėjų lentelę
        /// </summary>
        /// <param name="branchContainer">Filialų konteineris</param>
        /// <param name="file">Failas</param>
        private static void CreateReportTable(BranchContainer branchContainer, string file)
        {
            using (var writer = new StreamWriter(file, true, Encoding.UTF8))
            {
                writer.WriteLine("Žaidėjų sąrašai");
                writer.WriteLine(new string('-', 181));
                for (int i = 0; i < branchContainer.Count; i++)
                {
                    writer.WriteLine(new string('-', 181));
                    writer.WriteLine("Naujas žaidėjo sąrašas");
                    writer.WriteLine(new string('-', 181));
                    writer.WriteLine("| {0,-10} | {1,-15} | ", "Rasė", "Miestas");
                    writer.WriteLine($"| {branchContainer.GetBranch(i).Race,-10} | {branchContainer.GetBranch(i).Town,-15} |");

                    writer.WriteLine(new string('-', 181));
                    writer.WriteLine("Herojai");
                    writer.WriteLine(new string('-', 181));
                    writer.WriteLine("| {0, -15} | {1,-15} | {2,-15} | {3,-15} | {4,-15} | {5,-15} | {6,-15} | {7,-15} | {8,-15} | {9,-15} |",
                        "Vardas", "Klasė", "Gyvybės taškai", "Mana", "Žalos taškai ", "Gynybos taškai", "Jėga", "Vikrumas", "Intelektas", "Ypatinga galia");
                    writer.WriteLine(new string('-', 181));
                    for (int j = 0; j < branchContainer.GetBranch(i).Heroes.Count; j++)
                    {
                        writer.WriteLine($"| {branchContainer.GetBranch(i).Heroes.GetHero(j).Name,-15} | {branchContainer.GetBranch(i).Heroes.GetHero(j).Role,-15} | " +
                                         $"{branchContainer.GetBranch(i).Heroes.GetHero(j).HitPoints,15} | {branchContainer.GetBranch(i).Heroes.GetHero(j).Mana,15} | " +
                                         $"{branchContainer.GetBranch(i).Heroes.GetHero(j).Damage,15} | {branchContainer.GetBranch(i).Heroes.GetHero(j).Defence,15} | " +
                                         $"{branchContainer.GetBranch(i).Heroes.GetHero(j).Strength,15} | {branchContainer.GetBranch(i).Heroes.GetHero(j).Agility,15} | " +
                                         $"{branchContainer.GetBranch(i).Heroes.GetHero(j).Intelligence,15} | {branchContainer.GetBranch(i).Heroes.GetHero(j).Power,-15} |");
                    }

                    writer.WriteLine(new string('-', 181));
                    writer.WriteLine("NPC");
                    writer.WriteLine(new string('-', 181));
                    writer.WriteLine("| {0, -15} | {1,-15} | {2,-15} | {3,-15} | {4,-15} | {5,-15} | {6,-15} |",
                        "Vardas", "Klasė", "Gyvybės taškai", "Mana", "Žalos taškai ", "Gynybos taškai", "Gildija");
                    writer.WriteLine(new string('-', 181));
                    for (int j = 0; j < branchContainer.GetBranch(i).NPCs.Count; j++)
                    {
                        writer.WriteLine($"| {branchContainer.GetBranch(i).NPCs.GetNPC(j).Name,-15} | {branchContainer.GetBranch(i).NPCs.GetNPC(j).Role,-15} | " +
                                         $"{branchContainer.GetBranch(i).NPCs.GetNPC(j).HitPoints,15} | {branchContainer.GetBranch(i).NPCs.GetNPC(j).Mana,15} | " +
                                         $"{branchContainer.GetBranch(i).NPCs.GetNPC(j).Damage,15} | {branchContainer.GetBranch(i).NPCs.GetNPC(j).Defence,15} | " +
                                         $"{branchContainer.GetBranch(i).NPCs.GetNPC(j).Guild,-15} |");
                    }
                    writer.WriteLine(new string('-', 181));
                }
            }
        }
        /// <summary>
        /// Suranda populiariausią klasę
        /// </summary>
        /// <param name="branchContainer">Filialų konteineris</param>
        /// <returns>Populiariausią klasę</returns>
        private static Dictionary<string, int> FindMostPopular(BranchContainer branchContainer)
        {
            var mostPopularRole = new Dictionary<string, int>();
            for (int i = 0; i < branchContainer.Count; i++)
            {
                for (var j = 0; j < branchContainer.GetBranch(i).Heroes.Count; j++)
                {
                    if (mostPopularRole.ContainsKey(branchContainer.GetBranch(i).Heroes.GetHero(j).Role))
                    {
                        mostPopularRole[branchContainer.GetBranch(i).Heroes.GetHero(j).Role]++;
                    }
                    else
                    {
                        mostPopularRole.Add(branchContainer.GetBranch(i).Heroes.GetHero(j).Role, 1);
                    }
                }

                for (var j = 0; j < branchContainer.GetBranch(i).NPCs.Count; j++)
                {
                    if (mostPopularRole.ContainsKey(branchContainer.GetBranch(i).NPCs.GetNPC(j).Role))
                    {
                        mostPopularRole[branchContainer.GetBranch(i).NPCs.GetNPC(j).Role]++;
                    }
                    else
                    {
                        mostPopularRole.Add(branchContainer.GetBranch(i).NPCs.GetNPC(j).Role, 1);
                    }
                }
            }
            return mostPopularRole;
        }

        /// <summary>
        /// Išspausdina populiariausią klasę
        /// </summary>
        /// <param name="branchContainer">Filialų konteineris</param>
        private static void PrintMostPopularRole(BranchContainer branchContainer)
        {
            var mostPopularRole = FindMostPopular(branchContainer);
            var maxValue = mostPopularRole.Values.Max();
            var role = mostPopularRole.FirstOrDefault(f => f.Value == maxValue).Key;

            Console.WriteLine($"1.Daugiausiai šios klasės veikėjų: {role} | Pasikartoja: {maxValue} kartus(ų)");
            Console.WriteLine();
        }

        /// <summary>
        /// Suranda vienodus veikėjus
        /// </summary>
        /// <param name="branchContainer">Filialų konteineris</param>
        /// <returns>Vienodus veikėjus</returns>
        private static Dictionary<string, int> FilterPlayers(BranchContainer branchContainer)
        {
            var samePlayers = new Dictionary<string, int>();
            for (int i = 0; i < branchContainer.Count; i++)
            {
                for (var j = 0; j < branchContainer.GetBranch(i).Heroes.Count; j++)
                {
                    if (samePlayers.ContainsKey(branchContainer.GetBranch(i).Heroes.GetHero(j).Name))
                    {
                        samePlayers[branchContainer.GetBranch(i).Heroes.GetHero(j).Name]++;
                    }
                    else
                    {
                        samePlayers.Add(branchContainer.GetBranch(i).Heroes.GetHero(j).Name, 1);
                    }
                }

                for (var j = 0; j < branchContainer.GetBranch(i).NPCs.Count; j++)
                {
                    if (samePlayers.ContainsKey(branchContainer.GetBranch(i).NPCs.GetNPC(j).Name))
                    {
                        samePlayers[branchContainer.GetBranch(i).NPCs.GetNPC(j).Name]++;
                    }
                    else
                    {
                        samePlayers.Add(branchContainer.GetBranch(i).NPCs.GetNPC(j).Name, 1);
                    }
                }
            }
            return samePlayers;
        }

        /// <summary>
        /// Įrašo pasikartojančias rasių vardus į failą
        /// </summary>
        /// <param name="branchContainer">Filialų konteineris</param>
        /// <param name="file">Failas</param>
        public static void WriteFilteredPlayersData(BranchContainer branchContainer, string file)
        {
            var filteredPlayers = FilterPlayers(branchContainer);
            var filteredPlayersCount = filteredPlayers.Values.Max();
            using (var writer = new StreamWriter(file, false, Encoding.UTF8))
            {
                writer.WriteLine("Vardas;");
                foreach (KeyValuePair<string, int> samePlayers in filteredPlayers)
                {
                    if (samePlayers.Value >= 2)
                    {
                        writer.WriteLine($"{samePlayers.Key}");
                    }
                }
            }
        }

        /// <summary>
        /// Suranda herojus kurie atitinka tanko parametrus
        /// </summary>
        /// <param name="branches">Masyvas su visų rasių duomenimis</param>
        /// <returns>Gražina tankus</returns>
        public static Branch FindTanks(BranchContainer branchContainer)
        {
            var tanks = new Branch();
            for (int i = 0; i < branchContainer.Count; i++)
            {
                for (int j = 0; j < branchContainer.GetBranch(i).Heroes.Count; j++)
                {
                    if (branchContainer.GetBranch(i).Heroes.GetHero(j).IsTank(TankHealth, TankDefence))
                    {
                        tanks.AddHero(branchContainer.GetBranch(i).Heroes.GetHero(j));
                    }
                }

                for (int j = 0; j < branchContainer.GetBranch(i).NPCs.Count; j++)
                {
                    if (branchContainer.GetBranch(i).NPCs.GetNPC(j).IsTank(TankHealth, TankDefence))
                    {
                        tanks.AddNPC(branchContainer.GetBranch(i).NPCs.GetNPC(j));
                    }
                }

            }
            return tanks;
        }

        /// <summary>
        /// Įrašo surikiuotus tankus į failą.
        /// </summary>
        /// <param name="branchContainer">Filialų konteineris</param>
        /// <param name="file">Failas</param>
        public static void PrintTanks(BranchContainer branchContainer, string file)
        {
            var filteredTanks = FindTanks(branchContainer);
            filteredTanks.Heroes.SortHeroes();
            filteredTanks.NPCs.SortNPCs();
            using (var writer = new StreamWriter(file, false, Encoding.UTF8))
            {
                writer.WriteLine("Herojai");
                writer.WriteLine("Vardas;Klasė;Gyvybės taškai;Mana;Žalos taškai;Gynybos taškai;Jėga;Vikrumas;Intelektas;Ypatinga galia");
                for (int i = 0; i < filteredTanks.Heroes.Count; i++)
                {
                    writer.WriteLine(filteredTanks.Heroes.GetHero(i).ToString());
                }
                writer.WriteLine();
                writer.WriteLine("NPC");
                writer.WriteLine("Vardas;Klasė;Gyvybės taškai;Mana;Žalos taškai;Gynybos taškai;Gildija");
                for (int i = 0; i < filteredTanks.NPCs.Count; i++)
                {
                    writer.WriteLine(filteredTanks.NPCs.GetNPC(i).ToString());
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="branchContainer">Filialų konteineris</param>
        /// <param name="intelligenceLimit">Intelekto nurodytas dydis</param>
        /// <param name="damagePoint">Žalos taškų nurodytas dydis</param>
        /// <returns>Veikėjų rinktinę</returns>
        public static Branch GeneralSelection(BranchContainer branchContainer, int intelligenceLimit, int damagePoint)
        {
            var selection = new Branch();

            for (int i = 0; i < branchContainer.Count; i++)
            {
                for (int j = 0; j < branchContainer.GetBranch(i).Heroes.Count; j++)
                {
                    if (branchContainer.GetBranch(i).Heroes.GetHero(j).IsIntelligence(intelligenceLimit))
                    {
                        selection.AddHero(branchContainer.GetBranch(i).Heroes.GetHero(j));
                    }
                }

                for (int j = 0; j < branchContainer.GetBranch(i).NPCs.Count; j++)
                {
                    if (branchContainer.GetBranch(i).NPCs.GetNPC(j).IsNotDamaged(damagePoint))
                    {
                        selection.AddNPC(branchContainer.GetBranch(i).NPCs.GetNPC(j));
                    }
                }
            }
            return selection;
        }

        /// <summary>
        /// Spausdina į failą veikėjų rinktinę
        /// </summary>
        /// <param name="branchContainer">Filialų konteineris</param>
        /// <param name="file">Failas</param>
        /// <param name="intelligenceLimit">Intelekto nurodytas dydis</param>
        /// <param name="damagePoint">Žalos taškų nurodytas dydis</param>
        public static void PrintGeneralSelection(BranchContainer branchContainer, string file, int intelligenceLimit, int damagePoint)
        {
            var filteredSelection = GeneralSelection(branchContainer, intelligenceLimit, damagePoint);
            filteredSelection.Heroes.SortHeroesByIntelligence();
            filteredSelection.NPCs.SortNPCsByDamage();
            using (var writer = new StreamWriter(file, false, Encoding.UTF8))
            {
                writer.WriteLine("Herojai");
                writer.WriteLine("Vardas;Klasė;Gyvybės taškai;Mana;Žalos taškai;Gynybos taškai;Jėga;Vikrumas;Intelektas;Ypatinga galia");
                for (int i = 0; i < filteredSelection.Heroes.Count; i++)
                {
                    writer.WriteLine(filteredSelection.Heroes.GetHero(i).ToString());
                }
                writer.WriteLine();
                writer.WriteLine("NPC");
                writer.WriteLine("Vardas;Klasė;Gyvybės taškai;Mana;Žalos taškai;Gynybos taškai;Gildija");
                for (int i = 0; i < filteredSelection.NPCs.Count; i++)
                {
                    writer.WriteLine(filteredSelection.NPCs.GetNPC(i).ToString());
                }
            }
        }

        /// <summary>
        /// Skaito ar teisingai įvestas sveikas skaičius
        /// </summary>
        /// <param name="prompt">Antraštė</param>
        /// <returns>Teisingai įvestą sveiką skaičių</returns>
        public static int ReadInt(string prompt)
        {
            int limit = 0;
            do
            {
                try
                {
                    Console.WriteLine(prompt);
                    limit = int.Parse(Console.ReadLine());
                    if (limit < -100000 || limit > 100000)
                    {
                        Console.WriteLine("Įvedėte per didelius arba per mažus sveikus skaičius");
                    }
                    continue;
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                }
            } while (limit < -100000 || limit > 100000);

            return limit;
        }
    }
}