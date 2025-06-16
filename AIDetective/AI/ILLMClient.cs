using DetectiveAI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DetectiveAI.AI
{

    public interface ILLMClient
    {
        Task<string> GetResponseAsync(List<Message> messages);
    }
}
