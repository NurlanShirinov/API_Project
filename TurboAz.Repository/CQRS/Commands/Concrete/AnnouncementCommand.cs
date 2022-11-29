using Dapper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TurboAz.Core.Models;
using TurboAz.Repository.CQRS.Commands.Abstract;
using TurboAz.Repository.Infrustructure;

namespace TurboAz.Repository.CQRS.Commands.Concrete
{
    public class AnnouncementCommand : IAnnouncementCommand
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IUnitOfWorkAdoNet _unitOfWorkAdoNet;

        public AnnouncementCommand(IUnitOfWork unitOfWork, IUnitOfWorkAdoNet unitOfWorkAdoNet)
        {
            _unitOfWork = unitOfWork;
            _unitOfWorkAdoNet = unitOfWorkAdoNet;
        }

        private string _deleteSq = "DELETE FROM ANNOUNCMENTS WHERE Id=@id";

        private string _addSql = $@"INSERT INTO ANNOUNCMENTS([Price],[ViewCount],[IsActive],[IsVip],[Expired],[Number],[CarId],[CityId],[CategoryId])
                                 VALUES(
                                        @{nameof(Announcement.Price)},
                                        @{nameof(Announcement.ViewCount)},
                                        @{nameof(Announcement.IsActive)},
                                        @{nameof(Announcement.IsVip)},
                                        @{nameof(Announcement.Expired)},
                                        @{nameof(Announcement.Number)},
                                        @{nameof(Announcement.CarId)},
                                        @{nameof(Announcement.CityId)},
                                        @{nameof(Announcement.CategoryId)})";

        private string _updateSql = $@"UPDATE ANNOUNCMENTS
                                       SET Number = @number,
                                           CarId = @carId,
                                           CityId = @cityId,
                                           CategoryId = @categoryId,
                                           Price = @price,
                                           ViewCount = @viewCount,
                                           IsActive = @isActive,
                                           IsVip = @isVip,
                                           Expired = @expired
                                       WHERE Id=@id";

        private string _setVipSql = $@"UPDATE ANNOUNCMENTS
                                       SET IsVip = {1}
                                       WHERE Id=id";
        public async Task<int> Add(Announcement announcement)
        {
            var conn = _unitOfWorkAdoNet.GetConnection();
            try
            {
                #region Dapper
                //var res = await _unitOfWork.GetConnection().QueryFirstOrDefaultAsync<int>(_addSql, announcement, _unitOfWork.GetTransaction());
                //return res;
                #endregion

                #region AdoNet
                var command = new SqlCommand(_addSql, conn);

                if (conn.State == ConnectionState.Closed)
                    conn.Open();

                var paramPrice = new SqlParameter();
                paramPrice.ParameterName = $"@{nameof(Announcement.Price)}";
                paramPrice.SqlDbType = SqlDbType.Int;
                paramPrice.Value = announcement.Price;
                command.Parameters.Add(paramPrice);

                var paramView = new SqlParameter();
                paramView.ParameterName = $"@{nameof(Announcement.ViewCount)}";
                paramView.SqlDbType = SqlDbType.Int;
                paramView.Value = announcement.ViewCount;
                command.Parameters.Add(paramView);

                var paramActive = new SqlParameter();
                paramActive.ParameterName = $"@{nameof(Announcement.IsActive)}";
                paramActive.SqlDbType = SqlDbType.Bit;
                paramActive.Value = announcement.IsActive;
                command.Parameters.Add(paramActive);

                var paramVip = new SqlParameter();
                paramVip.ParameterName = $"@{nameof(Announcement.IsVip)}";
                paramVip.SqlDbType = SqlDbType.Bit;
                paramVip.Value = announcement.IsVip;
                command.Parameters.Add(paramVip);

                var paramExpired = new SqlParameter();
                paramExpired.ParameterName = $"@{nameof(Announcement.Expired)}";
                paramExpired.SqlDbType = SqlDbType.DateTime2;
                paramExpired.Value = announcement.Expired;
                command.Parameters.Add(paramExpired);

                var paramNumber = new SqlParameter();
                paramNumber.ParameterName = $"@{nameof(Announcement.Number)}";
                paramNumber.SqlDbType = SqlDbType.Int;
                paramNumber.Value = announcement.Number;
                command.Parameters.Add(paramNumber);

                var paramCarId = new SqlParameter();
                paramCarId.ParameterName = $"@{nameof(Announcement.CarId)}";
                paramCarId.SqlDbType = SqlDbType.Int;
                paramCarId.Value = announcement.CarId;
                command.Parameters.Add(paramCarId);

                var paramCityId = new SqlParameter();
                paramCityId.ParameterName = $"@{nameof(Announcement.CityId)}";
                paramCityId.SqlDbType = SqlDbType.Int;
                paramCityId.Value = announcement.CityId;
                command.Parameters.Add(paramCityId);

                var paramCategoryId = new SqlParameter();
                paramCategoryId.ParameterName = $"@{nameof(Announcement.CategoryId)}";
                paramCategoryId.SqlDbType = SqlDbType.Int;
                paramCategoryId.Value = announcement.CategoryId;
                command.Parameters.Add(paramCategoryId);

                var result = command.ExecuteNonQuery();
                return result;

                #endregion
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<bool> Delete(int id)
        {
            var conn = _unitOfWorkAdoNet.GetConnection();
            try
            {
                #region Dapper
                //var param = new { id };
                //await _unitOfWork.GetConnection().QueryAsync(_deleteSq, param, _unitOfWork.GetTransaction());
                //return true;
                #endregion

                #region AdoNet
                if (conn.State == ConnectionState.Closed)
                    conn.Open();
                var command = new SqlCommand(_deleteSq, conn);
                var paramId = new SqlParameter();
                paramId.ParameterName = "@id";
                paramId.SqlDbType = SqlDbType.Int;
                paramId.Value = id;
                command.Parameters.Add(paramId);
                command.ExecuteNonQuery();
                return true;

                #endregion
            }
            catch (Exception ex)
            {
                throw ex;
                return false;
            }
        }

        public async Task SetVip(int announcmentId)
        {
            var conn = _unitOfWorkAdoNet.GetConnection();
            try
            {
                #region Dapper
                //var param = new { announcmentId };
                //await _unitOfWork.GetConnection().QueryFirstOrDefaultAsync<int>(_setVipSql, param, _unitOfWork.GetTransaction());
                #endregion

                #region AdoNet
                if (conn.State == ConnectionState.Closed)
                     conn.Open();
                var command = new SqlCommand( _setVipSql, conn);

                var paramId = new SqlParameter();
                paramId.ParameterName = @"id";
                paramId.SqlDbType = SqlDbType.Int;
                paramId.Value = announcmentId;
                command.Parameters.Add(paramId);
                command.ExecuteNonQuery();

                #endregion
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<Announcement> Update(Announcement announcement)
        {
            var conn = _unitOfWorkAdoNet.GetConnection();
            try
            {
                #region Dapper
                //await _unitOfWork.GetConnection().QueryAsync(_updateSql, announcement, _unitOfWork.GetTransaction());
                //return announcement;
                #endregion

                #region AdoNet
                var command = new SqlCommand(_updateSql, conn);
                if (conn.State == ConnectionState.Closed)
                    conn.Open();

                var paramPrice = new SqlParameter();
                paramPrice.ParameterName = $"@price";
                paramPrice.SqlDbType = SqlDbType.Int;
                paramPrice.Value = announcement.Price;
                command.Parameters.Add(paramPrice);

                var paramView = new SqlParameter();
                paramView.ParameterName = $"@viewCount";
                paramView.SqlDbType = SqlDbType.Int;
                paramView.Value = announcement.ViewCount;
                command.Parameters.Add(paramView);

                var paramActive = new SqlParameter();
                paramActive.ParameterName = $"@isActive";
                paramActive.SqlDbType = SqlDbType.Bit;
                paramActive.Value = announcement.IsActive;
                command.Parameters.Add(paramActive);

                var paramVip = new SqlParameter();
                paramVip.ParameterName = $"@isVip";
                paramVip.SqlDbType = SqlDbType.Bit;
                paramVip.Value = announcement.IsVip;
                command.Parameters.Add(paramVip);

                var paramExpired = new SqlParameter();
                paramExpired.ParameterName = $"@expired";
                paramExpired.SqlDbType = SqlDbType.DateTime2;
                paramExpired.Value = announcement.Expired;
                command.Parameters.Add(paramExpired);

                var paramNumber = new SqlParameter();
                paramNumber.ParameterName = $"@number";
                paramNumber.SqlDbType = SqlDbType.Int;
                paramNumber.Value = announcement.Number;
                command.Parameters.Add(paramNumber);

                var paramCarId = new SqlParameter();
                paramCarId.ParameterName = $"@carId";
                paramCarId.SqlDbType = SqlDbType.Int;
                paramCarId.Value = announcement.CarId;
                command.Parameters.Add(paramCarId);

                var paramCityId = new SqlParameter();
                paramCityId.ParameterName = $"@cityId";
                paramCityId.SqlDbType = SqlDbType.Int;
                paramCityId.Value = announcement.CityId;
                command.Parameters.Add(paramCityId);

                var paramCategoryId = new SqlParameter();
                paramCategoryId.ParameterName = $"@categoryId";
                paramCategoryId.SqlDbType = SqlDbType.Int;
                paramCategoryId.Value = announcement.CategoryId;
                command.Parameters.Add(paramCategoryId);

                var paramId = new SqlParameter();
                paramId.ParameterName = @"id";
                paramId.SqlDbType = SqlDbType.Int;
                paramId.Value = announcement.Id;
                command.Parameters.Add(paramId);

                command.ExecuteNonQuery();

                return announcement;

                #endregion
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
