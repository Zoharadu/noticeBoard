using Application.Interfaces;
using Domain.Entities;

namespace Application.Services;

public class NoticeService
{
    private readonly INoticeRepository _repository;

    public NoticeService(INoticeRepository repository)
    {
        _repository = repository;
    }

    public Task<List<Notice>> GetAllAsync()
        => _repository.GetAllAsync();

    public Task<Notice?> GetByIdAsync(Guid id)
        => _repository.GetByIdAsync(id);

    public async Task<Notice> CreateAsync(string title, string content, Location location)
    {
        var notice = new Notice(title, content, location);
        await _repository.AddAsync(notice);
        return notice;
    }

    public async Task<bool> UpdateAsync(Guid id, string title, string content, Location location)
    {
        var existing = await _repository.GetByIdAsync(id);
        if (existing is null)
            return false;

        var updated = new Notice(title, content, location);

        typeof(Notice).GetProperty(nameof(Notice.Id))!
            .SetValue(updated, existing.Id);

        typeof(Notice).GetProperty(nameof(Notice.CreatedAt))!
            .SetValue(updated, existing.CreatedAt);

        await _repository.UpdateAsync(updated);
        return true;
    }

    public async Task<bool> DeleteAsync(Guid id)
    {
        var existing = await _repository.GetByIdAsync(id);
        if (existing is null)
            return false;

        await _repository.DeleteAsync(id);
        return true;
    }
}
