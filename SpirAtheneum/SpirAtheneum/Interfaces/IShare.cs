using System;
using System.Threading.Tasks;

namespace SpirAtheneum.Interfaces
{
    public interface IShare
    {
		Task Show(string TextContents, string ShareMessage);
    }
}
