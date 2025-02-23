namespace Shared
{
    public record PrintJobMessage
    {
        public string JobId { get; init; }
        public string DocumentName { get; init; }
        public string Content { get; init; }
        
        public string Identifier { get; set; }
    }
}