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
            try
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
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return Enumerable.Empty<ContentViewModel>();
            }
        }
        public async Task<IEnumerable<tblCourses>> GetCourses()
        {
            try
            {
            return await _context.tblCourses.Where(u => u.IsDeleted == false).ToListAsync();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return Enumerable.Empty<tblCourses>();
            }

            
        }
        public async Task<int> AddContent(ContentViewModel content)
        {
            try
            {
            tblContents Contents = new tblContents();
            Contents.ContentsAr = content.ContentsAr;
            Contents.ContentsEn = content.ContentsEn;
            Contents.Links = content.Links;
            Contents.TitleAr = content.TitleAr;
            Contents.TitleEn = content.TitleEn;
            Contents.CourseId = content.CourseId;
            _context.tblContents.Add(Contents);

                _context.SaveChanges();              
                    tblContentsLogs log = new tblContentsLogs();
                    log.ContentID = Contents.Id;
                    log.DateTime = DateTime.Now;
                    log.OperationType = "Add";
                    log.CreatedBy = "Hussain";
                    log.AdditionalInfo = $"تم إضافة محتوى مقرر عن طريق هذا المستخدم";
                    _context.tblContentsLogs.Add(log);
                

                int check=await _context.SaveChangesAsync();

                return check;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return 0;
            }

        }
        public async Task<ContentViewModel> GetContentByGUID(Guid id)
        {
            try
            {
            var content = await _context.tblContents.AsNoTracking().FirstOrDefaultAsync(x => x.GUID == id);
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
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return null;
            }

            
        }
        public async Task<int> UpdateContent(ContentViewModel content)
        {
            try
            {
            var contentss = await _context.tblContents.AsNoTracking().FirstOrDefaultAsync(x => x.GUID == content.GUID);
            tblContents Contents = new tblContents();
            Contents.Id = contentss.Id;
            Contents.ContentsAr = content.ContentsAr;
            Contents.ContentsEn = content.ContentsEn;
            Contents.Links = content.Links;
            Contents.TitleAr = content.TitleAr;
            Contents.TitleEn = content.TitleEn;
            Contents.CourseId = content.CourseId;
            Contents.GUID = content.GUID;
            Contents.IsDeleted = content.IsDeleted;
            _context.tblContents.Update(Contents);

                _context.SaveChanges();
                
                    tblContentsLogs log = new tblContentsLogs();
                    log.ContentID = Contents.Id;
                    log.DateTime = DateTime.Now;
                    log.OperationType = "Update";
                    log.CreatedBy = "Hussain";
                    log.AdditionalInfo = $"تم تحديث محتوى مقرر عن طريق هذا المستخدم";
                    _context.tblContentsLogs.Add(log);
                

                int check=await _context.SaveChangesAsync();
                return check;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return 0;
            }

           
        }
        public async Task<int> DeleteContent(ContentViewModel content)
        {
            try
            {
            var contentss = await _context.tblContents.AsNoTracking().FirstOrDefaultAsync(x => x.GUID == content.GUID);
            tblContents Contents = new tblContents();
            Contents.Id = contentss.Id;
            Contents.ContentsAr = contentss.ContentsAr;
            Contents.ContentsEn = contentss.ContentsEn;
            Contents.Links = contentss.Links;
            Contents.TitleAr = contentss.TitleAr;
            Contents.TitleEn = contentss.TitleEn;
            Contents.CourseId = contentss.CourseId;
            Contents.GUID = contentss.GUID;
            Contents.IsDeleted = true;
            _context.tblContents.Update(Contents);

                _context.SaveChanges();
                
                    tblContentsLogs log = new tblContentsLogs();
                    log.ContentID = Contents.Id;
                    log.DateTime = DateTime.Now;
                    log.OperationType = "Delete";
                    log.CreatedBy = "Hussain";
                    log.AdditionalInfo = $"تم حذف محتوى مقرر عن طريق هذا المستخدم";
                    _context.tblContentsLogs.Add(log);
                

                int check=await _context.SaveChangesAsync();
                return check;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return 0;
            }
            
        }
    }

}
