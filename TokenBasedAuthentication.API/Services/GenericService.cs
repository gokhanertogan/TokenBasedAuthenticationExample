using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using TokenBasedAuthentication.API.Domain.Repositories;
using TokenBasedAuthentication.API.Domain.Responses;
using TokenBasedAuthentication.API.Domain.Services;
using TokenBasedAuthentication.API.Domain.UnitOfWork;

namespace TokenBasedAuthentication.API.Services
{
    public class GenericService<T> : IGenericService<T> where T : class
    {
        private readonly IGenericRepository<T> _genericRepository;
        private readonly IUnitOfWork _unitOfWork;

        public GenericService(IGenericRepository<T> genericRepository, IUnitOfWork unitOfWork)
        {
            _genericRepository = genericRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<BaseResponse<T>> Add(T entry)
        {
            try
            {
                await _genericRepository.Add(entry);
                await _unitOfWork.CompleteAsync();
                return new BaseResponse<T>(entry);
            }
            catch (Exception ex)
            {
                return new BaseResponse<T>(ex.Message);
            }
        }

        public async Task<int> CountWhere(Expression<Func<T, bool>> predicate)
        {
            return await _genericRepository.CountWhere(predicate);
        }

        public async Task<BaseResponse<T>> Delete(int id)
        {
            try
            {
                T model = await _genericRepository.GetById(id);

                if (model != null)
                {
                    await _genericRepository.Delete(id);
                    await _unitOfWork.CompleteAsync();
                    return new BaseResponse<T>(model);
                }

                return new BaseResponse<T>($"Could not found. Id:{id}");
            }
            catch (Exception ex)
            {
                return new BaseResponse<T>(ex.Message);
            }
        }

        public async Task<BaseResponse<T>> GetById(int id)
        {
            try
            {
                T model = await _genericRepository.GetById(id);

                if (model != null)
                {
                    return new BaseResponse<T>(model);
                }

                return new BaseResponse<T>($"Could not found. Id:{id}");
            }
            catch (Exception ex)
            {
                return new BaseResponse<T>(ex.Message);
            }
        }

        public async Task<BaseResponse<IEnumerable<T>>> GetWhere(Expression<Func<T, bool>> predicate)
        {
            IEnumerable<T> model = await _genericRepository.GetWhere(predicate);
            return new BaseResponse<IEnumerable<T>>(model);
        }

        public async Task<BaseResponse<T>> Update(T entry)
        {
            try
            {
                _genericRepository.Update(entry);
                await _unitOfWork.CompleteAsync();
                return new BaseResponse<T>(entry);
            }
            catch (Exception ex)
            {
                return new BaseResponse<T>(ex.Message);
            }
        }
    }
}