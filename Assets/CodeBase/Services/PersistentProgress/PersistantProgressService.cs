using CodeBase.Data;

namespace CodeBase.Services.PersistantProgress
{
    public class PersistentProgressService : IPersistentProgressService
    {
        private PlayerProgress Progress { get; set; }
    }

    public interface IPersistentProgressService: IService
    {
    }
}