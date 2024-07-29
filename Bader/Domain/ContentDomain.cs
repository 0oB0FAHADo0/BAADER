using Bader.Models;
using Bader.ViewModels;
using Microsoft.EntityFrameworkCore;

namespace Bader.Domain
{
    public class ContentDomain
    {
        private readonly BaaderContext _context;

        public ContentDomain(BaaderContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<ContentViewModel>> GetContents()
        {
            return await _context.tblContents.Where(u => u.IsDeleted == false).Select(x => new ContentViewModel
            {
                Id = x.Id,
                CourseId = x.CourseId,
                ContentsAr = x.ContentsAr,
                ContentsEn = x.ContentsEn,
                GUID = x.GUID,
                Links = x.Links,
                TitleAr = x.TitleAr,
                TitleEn = x.TitleEn,
            }).ToListAsync();
        }
        public async Task<IEnumerable<tblCourses>> GetCourses()
        {
            return await _context.tblCourses.Where(u => u.IsDeleted == false).ToListAsync();
        }
        public async Task AddContent(ContentViewModel content)
        {
            tblContents Contents = new tblContents();
            Contents.ContentsAr = content.ContentsAr;
            Contents.ContentsEn = content.ContentsEn;
            Contents.Links = content.Links;
            Contents.TitleAr = content.TitleEn;
            Contents.TitleEn = content.TitleEn;
            Contents.CourseId = content.CourseId;
            _context.tblContents.Add(Contents);
            await _context.SaveChangesAsync();
        }
        public async Task<ContentViewModel> GetContentByGUID(Guid GUID)
        {
            var content = await _context.tblContents.FindAsync(GUID);
            ContentViewModel Content = new ContentViewModel();
            Content.Id = content.Id;
            Content.TitleEn = content.TitleEn;
            Content.TitleAr = content.TitleAr;
            Content.ContentsAr = content.ContentsAr;
            Content.Links = content.Links;  
            Content.ContentsEn = content.ContentsEn;
            Content.CourseId = content.CourseId;
            Content.GUID = content.GUID;
            return Content;
        }
        public async Task UpdateContent(ContentViewModel content)
        {
            tblContents Contents = new tblContents();
            Contents.ContentsAr = content.ContentsAr;
            Contents.ContentsEn = content.ContentsEn;
            Contents.Links = content.Links;
            Contents.TitleAr = content.TitleEn;
            Contents.TitleEn = content.TitleEn;
            Contents.CourseId = content.CourseId;
            Contents.GUID = content.GUID;
            _context.tblContents.Update(Contents);
            await _context.SaveChangesAsync();
        }
    }

}
