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

        public AnnouncementResponseDto CreateAnnouncementAction(AnnouncementCreateDto data)
        {
            return CreateAnnouncementActionExecution(data);
        }

        public ActionResponse UpdateAnnouncementAction(AnnouncementUpdateDto data)
        {
            return UpdateAnnouncementActionExecution(data);
        }

        public ActionResponse DeleteAnnouncementAction(int id)
        {
            return DeleteAnnouncementActionExecution(id);
        }
    }
}
