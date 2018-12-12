using System.Linq;

namespace L5_U5_12
{
    /// <summary>
    /// NPCs konteineris
    /// </summary>
    class NPCContainer
    {
        private const int MaxNPC = 100;
        NPC[] NPCs;
        public int Count { get; set; }

        /// <summary>
        /// Konstruktorius
        /// </summary>
        public  NPCContainer()
        {
            NPCs = new NPC[MaxNPC];
            Count = 0;
        }

        /// <summary>
        /// Pridedamas NPC į masyvą
        /// </summary>
        /// <param name="index">Indeksas</param>
        /// <param name="nPC">NPC</param>
        public void AddNPC(int index, NPC nPC)
        {
            NPCs[index] = nPC;
        }

        /// <summary>
        /// Pridedamas NPC į masyvą
        /// </summary>
        /// <param name="nPC">NPC</param>
        public void AddNPC(NPC nPC)
        {
            NPCs[Count++] = nPC;
        }

        /// <summary>
        /// Paimamas NPC iš masyvo pagal nurodytą indeksą
        /// </summary>
        /// <param name="index">Indeksas</param>
        /// <returns></returns>
        public NPC GetNPC(int index)
        {
            return NPCs[index];
        }

        /// <summary>
        /// Ieško ar masyve yra nurodytas NPC
        /// </summary>
        /// <param name="nPC"></param>
        /// <returns></returns>
        public bool Contains(NPC nPC)
        {
            return NPCs.Contains(nPC);
        }

        /// <summary>
        /// Išrikiuoja NPCs
        /// </summary>
        public void SortNPCs()
        {
            for (int i = 0; i < Count - 1; i++)
            {
                var minValueNPC = NPCs[i];
                int minValueIndex = i;
                for (int j = i + 1; j < Count; j++)
                {
                    if (NPCs[j] <= minValueNPC)
                    {
                        minValueNPC = NPCs[j];
                        minValueIndex = j;
                    }
                }
                NPCs[minValueIndex] = NPCs[i];
                NPCs[i] = minValueNPC;
            }
        }

        /// <summary>
        /// Išrikiuoja NPCs pagal žalos taškus
        /// </summary>
        public void SortNPCsByDamage()
        {
            for (int i = 0; i < Count; i++)
            {
                for (int j = 0; j < Count; j++)
                {
                    if (NPCs[i].Damage >= NPCs[j].Damage)
                    {
                        var temp = NPCs[i];
                        NPCs[i] = NPCs[j];
                        NPCs[j] = temp;
                    }
                }
            }
        }
    }
}
