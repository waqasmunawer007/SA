using System;
using System.Threading.Tasks;

namespace SpirAtheneum.Interfaces
{
    public interface ISendEmail
    {
        Task Email(string email, string subject);
    }
}
