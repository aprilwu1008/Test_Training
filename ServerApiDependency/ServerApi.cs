using ServerApiDependency.Enum;
using ServerApiDependency.Enums;
using ServerApiDependency.Interface;
using ServerApiDependency.Utility;
using ServerApiDependency.Utility.CustomException;
using System;
using System.Net;

namespace ServerApiDependency
{
    public interface ILogger
    {
        void Error(string message);
    }

    public class Logger : ILogger
    {
        public void Error(string message)
        {
            TiDebugHelper.Error(message);
        }
    }

    public class ServerApi : IServerApi
    {
        private ILogger _logger;

        public ServerApi(ILogger logger)
        {
            _logger = logger;
        }

        public ServerResponse CancelGame()
        {
            const string apiPage = "cancel.php";
            try
            {
                var response = PostToThirdParty(ApiType.CancelGame, apiPage);
                if (response != (int)ServerResponse.Correct)
                {
                    _logger.Error($"{apiPage} response has error, ErrorCode = {response}");
                    if (response == (int)ServerResponse.AuthFail)
                    {
                        throw new AuthFailException();
                    }
                }

                return (ServerResponse)response;
            }
            catch (WebException e)
            {
                _logger.Error($" WebException: {e}");
                SaveFailRequestToDb(ApiType.CancelGame, apiPage);
                throw e;
            }
        }

        public ServerResponse GameResult()
        {
            const string apiPage = "result.php";
            try
            {
                var response = PostToThirdParty(ApiType.GameResult, apiPage);
                if (response != (int)ServerResponse.Correct)
                {
                    TiDebugHelper.Error($"{apiPage} response has error, ErrorCode = {response}");
                    if (response == (int)ServerResponse.AuthFail)
                    {
                        throw new AuthFailException();
                    }
                }
                return (ServerResponse)response;
            }
            catch (WebException e)
            {
                TiDebugHelper.Error($" WebException: {e}");
                SaveFailRequestToDb(ApiType.GameResult, apiPage);
                throw e;
            }
        }

        public ServerResponse StartGame()
        {
            const string apiPage = "start.php";
            try
            {
                var response = PostToThirdParty(ApiType.StartGame, apiPage);
                if (response != (int)ServerResponse.Correct)
                {
                    TiDebugHelper.Error($"{apiPage} response has error, ErrorCode = {response}");
                    if (response == (int)ServerResponse.AuthFail)
                    {
                        throw new AuthFailException();
                    }
                }
                return (ServerResponse)response;
            }
            catch (WebException e)
            {
                TiDebugHelper.Error($" WebException: {e}");
                SaveFailRequestToDb(ApiType.StartGame, apiPage);
                throw e;
            }
        }

        /// <summary>
        /// treat this method is a dependency to connect to third-party server
        /// </summary>
        /// <param name="apiType">Type of the API.</param>
        /// <param name="apiPage">The API page.</param>
        /// <returns></returns>
        /// <exception cref="System.NotImplementedException"></exception>
        internal virtual int PostToThirdParty(ApiType apiType, string apiPage)
        {
            // don't implement
            throw new NotImplementedException();
        }

        /// <summary>
        /// treat this method as a dependency to connect to db
        /// </summary>
        /// <param name="apiType">Type of the API.</param>
        /// <param name="apiPage">The API page.</param>
        /// <exception cref="System.NotImplementedException"></exception>
        internal virtual void SaveFailRequestToDb(ApiType apiType, string apiPage)
        {
            // don't implement
            throw new NotImplementedException();
        }
    }
}