using AzposAdminApi.Constants;
using AzposAdminApi.Dtos.Responses;
using AzposAdminApi.Exceptions;
using AzposAdminApi.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AzposAdminApi.Controllers
{
    [ApiController]
    [Route("api/v1/akuntansi/[controller]")]
    public class BackupController : ControllerBase
    {
        private readonly AzPosDBContext dBContext;
        public BackupController(AzPosDBContext context)
        {
            dBContext = context;
        }

        [Route("{id}")]
        [HttpGet]
        public async Task<BaseResponse> GetData(string id)
        {
            try
            {
                var data = await dBContext.Akun.Where(a => a.AkunId == id).ToListAsync();
                if (data is null)
                    throw new KnownException(ErrorMessageConstant.ItemNotFound);

                return new BaseResponse(true, data);
            }
            catch (Exception)
            {
                return new BaseResponse(false, ErrorMessageConstant.UnknowException);
            }
        }
    }
}