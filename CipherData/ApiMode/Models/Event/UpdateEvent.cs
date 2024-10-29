namespace CipherData.ApiMode
{
    public class UpdateEvent : CipherClass, IUpdateEvent
    {
        private string? _EventComment;
        private string? _ActionComments = null;

        public int Status { get; set; } = 0;

        public string? ProcessId { get; set; }

        public string? EventComment
        {
            get => _EventComment;
            set => _EventComment = value?.Trim();
        }

        public string? ActionComments
        {
            get => _ActionComments;
            set => _ActionComments = value?.Trim();
        }
    }
}
