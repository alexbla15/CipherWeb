namespace CipherData.Interfaces
{
    public class WorkerGroup
    {
        public string? Role { get; set; }
        public int Level { get; set; }

        public static readonly WorkerGroup Manager = new() { Role = "מנהל מערכת", Level=2 };
        public static readonly WorkerGroup Editor = new() { Role = "עורך", Level=1 };
        public static readonly WorkerGroup Viewer = new() { Role = "צופה", Level=0 };

    }

    [HebrewTranslation(nameof(IWorker))]
    public interface IWorker
    {
        [HebrewTranslation(nameof(IWorker))]
        string Name { get; set; }

        WorkerGroup Group { get; set; }

        public List<IWorker> AllWorkers();

        public bool CanView(CipherNavLink link)
            => Group.Level >= link.RestrictionLevel;
    }

    public abstract class BaseWorker : IWorker
    {
        private string _Name = string.Empty;

        public string Name { get => _Name; set => _Name = value.Trim(); }

        public WorkerGroup Group { get; set; } = WorkerGroup.Viewer;

        public abstract List<IWorker> AllWorkers();
    }
}
