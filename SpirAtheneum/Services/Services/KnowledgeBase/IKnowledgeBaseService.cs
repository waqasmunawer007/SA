using Services.Models.KnowledgeBase;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Services.Services.KnowledgeBase
{
    public interface IKnowledgeBaseService
    {
        Task<KnowledgeBaseModel[]> fetchAllKnowledgeBaseAsync();
        Task<KnowledgeBaseModel> fetchKnowledgeBaseUsingIdAsync(string id);
    }
}
