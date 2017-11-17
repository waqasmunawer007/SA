using System;
using System.Threading.Tasks;
using Services.Models.Subscription;

namespace Services.Services.SubScription
{
    public interface ISubscription
    {
        Task<AppSubscription[]> GetAppSubscriptionList();
    }
}
