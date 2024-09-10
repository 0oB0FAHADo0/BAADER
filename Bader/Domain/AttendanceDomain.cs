﻿using Bader.Models;
using Bader.ViewModels;
using Microsoft.EntityFrameworkCore;
using System;

namespace Bader.Domain
{
    public class AttendanceDomain
    {
        private readonly BaaderContext _context;

        public AttendanceDomain(BaaderContext context)
        {
            _context = context;
        }

        //Get all attendance records
        public async Task<IEnumerable<AttendanceViewModel>> GetAllAttendance()
        {
            try
            {
                return await _context.tblAttendance
                    .Include(a => a.Session)
                    .Select(a => new AttendanceViewModel
                    {
                        Id = a.Id,
                        SessionId = a.SessionId,
                        SessionDate = a.SessionDate,
                        UserName = a.UserName,
                        IsAttend = a.IsAttend,
                        GUID = a.GUID
                    })
                    .ToListAsync();
            }
            catch
            {
                return new List<AttendanceViewModel>();
            }
        }

        // Get attendance by session ID
        //public async Task<IEnumerable<AttendanceViewModel>> GetAttendanceBySessionId(int sessionId)
        //{
        //    try
        //    {
        //        return await _context.tblAttendance
        //            .Where(a => a.SessionId == sessionId)
        //            .Select(a => new AttendanceViewModel
        //            {
        //                Id = a.Id,
        //                SessionId = a.SessionId,
        //                SessionDate = a.SessionDate,
        //                UserName = a.UserName,
        //                IsAttend = a.IsAttend,
        //                GUID = a.GUID
        //            })
        //            .ToListAsync();
        //    }
        //    catch
        //    {
        //        return new List<AttendanceViewModel>();
        //    }
        //}




    }
}
