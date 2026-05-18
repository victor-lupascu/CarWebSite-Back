using CarWebSite.Domain.Models.Announcement;
using CarWebSite.Domain.Models.Responses;

namespace CarWebSite.BusinessLayer.Interfaces
{
    public interface IAnnouncementAction
    {
        List<AnnouncementResponseDto> GetAllAnnouncementAction();
        AnnouncementResponseDto? GetAnnouncementByIdAction(int id);
        AnnouncementResponseDto CreateAnnouncementAction(AnnouncementCreateDto data);
        ActionResponse UpdateAnnouncementAction(AnnouncementUpdateDto data);
        ActionResponse DeleteAnnouncementAction(int id);
    }
}
