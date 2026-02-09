using Domain.Entities;

namespace Application.Interfaces
{
    public interface INoticeRepository
    {
        Task<List<Notice>> GetAllAsync();
        Task<Notice?> GetByIdAsync(Guid id);
        Task AddAsync(Notice notice);
        Task UpdateAsync(Notice notice);
        Task DeleteAsync(Guid id);
    }
}
