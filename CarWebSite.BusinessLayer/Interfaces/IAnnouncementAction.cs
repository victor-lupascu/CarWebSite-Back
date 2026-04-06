using CarWebSite.Domain.Models.Announcement;
using CarWebSite.Domain.Models.Responses;

namespace CarWebSite.BusinessLayer.Interfaces
{
    public interface IAnnouncementAction
    {
        Task<List<AnnouncementResponseDto>> GetAllAnnouncementAction();
        Task<AnnouncementResponseDto?> GetAnnouncementByIdAction(int id);
        Task<ActionResponse> CreateAnnouncementAction(AnnouncementCreateDto data);
        Task<ActionResponse> UpdateAnnouncementAction(AnnouncementUpdateDto data);
        Task<ActionResponse> DeleteAnnouncementAction(int id);
    }
}
