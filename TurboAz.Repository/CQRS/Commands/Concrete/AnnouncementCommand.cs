using Dapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TurboAz.Core.Models;
using TurboAz.Repository.CQRS.Commands.Abstract;

namespace TurboAz.Repository.CQRS.Commands.Concrete
{
    public class AnnouncementCommand : IAnnouncementCommand
    {
        private readonly IUnitOfWork _unitOfWork;

        public AnnouncementCommand(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        private string _deleteSq = "DELETE FROM ANNOUNCEMENTS WHERE Id=@id";

        private string _addSql = $@"INSERT INTO ANNOUNCMENTS([CreatedDate],[RowNum],[CarId],[CityIdint],[CategoryId],[Price],[ViewCount],[IsActive],[IsVip],[Expired])
                                 VALUES(@{nameof(Announcement.CreatedDate)},
                                        @{nameof(Announcement.AnnouncemenNumber)}
                                        @{nameof(Announcement.AnnouncedCarId)}
                                        @{nameof(Announcement.AnnouncedCityId)}
                                        @{nameof(Announcement.AnnouncedCarCategoryId)}
                                        @{nameof(Announcement.Price)}
                                        @{nameof(Announcement.ViewCount)}
                                        @{nameof(Announcement.IsActive)}
                                        @{nameof(Announcement.IsVip)}
                                        @{nameof(Announcement.AnnouncementDeadline)})";

        private string _updateSql = $@"UPDATE ANNOUNCMENTS
                                       SET CreatedDate = @createdDate
                                           RowNum = @rowNum
                                           CarId = @carId
                                           CityIdint = @cityIdint
                                           CategoryId = @categoryId
                                           Price = @price
                                           ViewCount = @vievCount
                                           IsActive = @isActive
                                           IsVip = @icActive
                                           Expired = @expired
                                       WHERE Id=@id";

        private string _setVipSql = $@"UPDATE ANNOUNCMENTS
                                       SET IsVip = {1}
                                       WHERE Id=id";


        public async Task<int> Add(Announcement announcement)
        {
            try
            {
                var res = await _unitOfWork.GetConnection().QueryFirstOrDefaultAsync<int>(_addSql, announcement, _unitOfWork.GetTransaction());
                return res;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public async Task<bool> Delete(int id)
        {
            try
            {
                var param = new { id };
                await _unitOfWork.GetConnection().QueryAsync(_deleteSq, param, _unitOfWork.GetTransaction());
                return true;
            }
            catch (Exception ex)
            {

                throw ex;
                return false;
            }
        }

        public async Task SetVip(int announcmentId)
        {
            try
            {
                var param = new { announcmentId };
                await _unitOfWork.GetConnection().QueryFirstOrDefaultAsync<int>(_setVipSql, param, _unitOfWork.GetTransaction());
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public async Task<Announcement> Update(Announcement announcement)
        {
            try
            {
                await _unitOfWork.GetConnection().QueryAsync(_updateSql, announcement, _unitOfWork.GetTransaction());
                return announcement;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
    }
}
