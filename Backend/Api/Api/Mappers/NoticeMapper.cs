using Api.Responses;
using Domain.Entities;

namespace Api.Mappers;

public static class NoticeMapper
{
    public static NoticeResponse ToResponse(this Notice notice)
        => new()
        {
            Id = notice.Id,
            Title = notice.Title,
            Content = notice.Content,
            CreatedAt = notice.CreatedAt,
            Location = new LocationResponse
            {
                Latitude = notice.Location.Latitude,
                Longitude = notice.Location.Longitude,
                Address = notice.Location.Address
            }
        };
}
