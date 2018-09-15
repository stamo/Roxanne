using Alexa.NET.Request;
using Alexa.NET.Response;

namespace Roxanne.Contracts
{
    public interface IAlexaService
    {
        SkillResponse ProcessRequest(SkillRequest input);
    }
}
