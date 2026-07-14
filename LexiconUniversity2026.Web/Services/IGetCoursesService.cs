using Microsoft.AspNetCore.Mvc.Rendering;

namespace LexiconUniversity2026.Web.Services
{
    public interface IGetCoursesService
    {
        Task<IEnumerable<SelectListItem>> GetCoursesAsync();
    }
}