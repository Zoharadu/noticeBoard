using Application.Interfaces;
using Domain.Entities;
using Infrastructure.Storage.Model;
using System.Text.Json;

namespace Infrastructure.Repository
{
    public class NoticeRepository : INoticeRepository
    {
        private readonly string _filePath;

        public NoticeRepository(string filePath)
        {
            _filePath = filePath;
        }

        public async Task<List<Notice>> GetAllAsync()
        {
            var data = await ReadAsync();
            return data.Notices;
        }

        public async Task<Notice?> GetByIdAsync(Guid id)
        {
            var data = await ReadAsync();
            return data.Notices.FirstOrDefault(n => n.Id == id);// JSON storage requires loading the file into memory; in a database-backed implementation this would be a direct query by ID.

        }
        public async Task AddAsync(Notice notice)
        {
            var data = await ReadAsync();
            data.Notices.Add(notice);
            data.LastUpdatedAt = DateTime.UtcNow;
            await WriteAsync(data);
        }

        public async Task UpdateAsync(Notice notice)
        {
            var data = await ReadAsync();
            var index = data.Notices.FindIndex(n => n.Id == notice.Id);
            if (index < 0)
            {
                return;
            }

            data.Notices[index] = notice;
            data.LastUpdatedAt = DateTime.UtcNow;
            await WriteAsync(data);
        }

        public async Task DeleteAsync(Guid id)
        {
            var data = await ReadAsync();
            data.Notices.RemoveAll(n => n.Id == id);
            data.LastUpdatedAt = DateTime.UtcNow;
            await WriteAsync(data);
        }

        private static readonly JsonSerializerOptions ReadOptions = new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true
        };

        private async Task<NoticesFileModel> ReadAsync()
        {
            if (!File.Exists(_filePath))
            {
                return new NoticesFileModel();
            }
            Console.WriteLine(_filePath);
            var json = await File.ReadAllTextAsync(_filePath);

            var data = JsonSerializer.Deserialize<NoticesFileModel>(json, ReadOptions);
            if (data == null)
            {
                return new NoticesFileModel();
            }

            return data;
        }

        private async Task WriteAsync(NoticesFileModel data)
        {
            var json = JsonSerializer.Serialize(
                data,
                new JsonSerializerOptions
                {
                    WriteIndented = true
                });

            await File.WriteAllTextAsync(_filePath, json);
        }
    }
}
