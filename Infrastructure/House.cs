namespace Infrastructure
{
    public class House
    {
        private House(string id, int agentId, string agentName, bool hadGarden)
        {
            Id = id;
            AgentId = agentId;
            AgentName = agentName;
            HadGarden = hadGarden;
        }

        public string Id { get; }
        public int AgentId { get; }
        public string AgentName { get; }
        public bool HadGarden { get; }

        public static House CreateWithGarden(string id, int agentId, string agentName)
            => new House(id, agentId, agentName, true);
        
        public static House CreateWithNoGarden(string id, int agentId, string agentName)
            => new House(id, agentId, agentName, false);
        
    }
}