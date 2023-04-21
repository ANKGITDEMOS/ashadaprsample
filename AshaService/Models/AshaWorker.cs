namespace AshaService.Models
{
    public record struct AshaWorker 
    {
        public AshaWorker(string id, string name) {
            this.AshaWorkerID = id;
            this.AshaWorkerName = name;
        }
        public string AshaWorkerID { get; init; }
        public string AshaWorkerName { get; init; }
    
    }
}
