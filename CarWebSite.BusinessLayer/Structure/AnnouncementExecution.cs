using CarWebSite.BusinessLayer.Core;
using CarWebSite.BusinessLayer.Interfaces;
using CarWebSite.Domain.Models.Announcement;
using CarWebSite.Domain.Models.Responses;

namespace CarWebSite.BusinessLayer.Structure
{
    public class AnnouncementExecution : AnnouncementActions, IAnnouncementAction
    {
        public List<AnnouncementResponseDto> GetAllAnnouncementAction()
        {
            return GetAllAnnouncementActionExecution();
        }

        public AnnouncementResponseDto? GetAnnouncementByIdAction(int id)
        {
            return GetAnnouncementByIdActionExecution(id);
        }

        public ActionResponse CreateAnnouncementAction(AnnouncementCreateDto data, int userId)
        {
            return CreateAnnouncementActionExecution(data, userId);
        }

        public ActionResponse UpdateAnnouncementAction(int id,AnnouncementUpdateDto data, int userId, bool isAdmin)
        {
            return UpdateAnnouncementActionExecution(id, data, userId, isAdmin);
        }

        public ActionResponse DeleteAnnouncementAction(int id, int userId, bool isAdmin)
        {
            return DeleteAnnouncementActionExecution(id, userId, isAdmin);
        }
    }
}
