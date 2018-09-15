using Alexa.NET;
using Alexa.NET.Request;
using Alexa.NET.Request.Type;
using Alexa.NET.Response;
using Roxanne.Contracts;
using System;

namespace Roxanne.Services
{
    public class AlexaService : IAlexaService
    {
        private readonly IOmegaRequester requester;

        public AlexaService(IOmegaRequester _requester)
        {
            requester = _requester;
        }

        public SkillResponse ProcessRequest(SkillRequest input)
        {
            var requestType = input.GetRequestType();
            var response = new SkillResponse();

            if (requestType == typeof(IntentRequest))
            {
                response = GetResponse(input.Request as IntentRequest);
            }
            else if (requestType == typeof(LaunchRequest))
            {
                response = GetResponse(input.Request as LaunchRequest);
            }
            else if (requestType == typeof(SessionEndedRequest))
            {
                response = GetResponse(input.Request as SessionEndedRequest);
            }

            return response;

            
        }

        private SkillResponse GetResponse(SessionEndedRequest sessionEndedRequest)
        {
            return CreateResponse("OK. Goodbye for now");
        }

        private SkillResponse GetResponse(LaunchRequest launchRequest)
        {
            var response = CreateResponse("Roxanne is here. What can I do for you");
            response.Response.ShouldEndSession = false;

            return response;
        }

        private SkillResponse GetResponse(IntentRequest intentRequest)
        {
            var response = new SkillResponse();
            
            switch (intentRequest.Intent.Name)
            {
                case "TurnOnIntent":
                    if (requester.SetBulbState(1))
                    {
                        response = CreateResponse("The red light is on");
                    }
                    else
                    {
                        response = CreateResponse("Roxanne is busy. Try again later");
                    }
                    
                    break;
                case "TurnOffIntent":
                    if (requester.SetBulbState(0))
                    {
                        response = CreateResponse("The red light is off");
                    }
                    else
                    {
                        response = CreateResponse("Roxanne is busy. Try again later");
                    }

                    break;
                case "ThankYouIntent":
                    response = CreateResponse("Thank you for your attention. If you have any questions, please don't hasitate to ask. Stamo will be happy to answer them. See you arround.");
                    break;
                case "AMAZON.CancelIntent":
                case "AMAZON.StopIntent":
                case "AMAZON.NavigateHomeIntent":
                    response = CreateResponse("OK. Goodbye for now");
                    break;
                case "AMAZON.HelpIntent":
                    response = CreateResponse("I can put on the red light");
                    break;
                default:
                    response = CreateResponse("Sorry, I didn't understand that");
                    break;
            };

            return response;
        }

        private SkillResponse CreateResponse(string message)
        {
            var speech = new Alexa.NET.Response.SsmlOutputSpeech();
            speech.Ssml = $"<speak>{ message }</speak>";

            var finalResponse = ResponseBuilder.TellWithCard(speech, "Roxanne", message);

            return finalResponse;
        }
    }
}
