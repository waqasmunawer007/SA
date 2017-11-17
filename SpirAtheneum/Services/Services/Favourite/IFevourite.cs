using System;
using System.Threading.Tasks;
using Services.Models.Favourite;

namespace Services.Services.Favourite
{
    public interface IFevourite
    {
        Task<FevouriteRequest> UploadFevouriteList(FevouriteRequest requestParameters);
    }
}
