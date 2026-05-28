using CarWebSite.Domain.Models.Announcement;
using CarWebSite.Domain.Models.Responses;

namespace CarWebSite.BusinessLayer.Interfaces
{
    public interface IAnnouncementAction
    {
        List<AnnouncementResponseDto> GetAllAnnouncementAction();
        AnnouncementResponseDto? GetAnnouncementByIdAction(int id);
        ActionResponse CreateAnnouncementAction(AnnouncementCreateDto data, int userId);
        ActionResponse UpdateAnnouncementAction(int id, AnnouncementUpdateDto data, int userId, bool isAdmin);
        ActionResponse DeleteAnnouncementAction(int id, int userId, bool isAdmin);
    }
}
