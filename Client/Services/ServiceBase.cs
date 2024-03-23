using Constants;
using System.Net.Http.Json;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using ViewModels.Pages.Account.ModelState;
using System.Net.Http;
namespace Client.Services;

public abstract class ServiceBase : object
{
    #region Constructor

    public ServiceBase
        (HttpClient httpClient, LogsService logsService) : base()
    {
        Http = httpClient;
        LogsService = logsService;
    }
    #endregion /Constructor

    #region Properties 

    protected string? BaseApiUrl
    {
        get
        {
            return CommonRouting.BaseApiUrl;
        }
    }

    protected HttpClient Http { get; }

    protected LogsService LogsService { get; }

    #endregion /Properties

    #region GetAsync
    protected virtual
        async
        Task<TResponse?>
        GetAsync<TResponse>(string url, string? query = null) where TResponse : class
    {
        HttpResponseMessage? response = null;


        var requestUri =
            $"{BaseApiUrl}/{url}";
        if (string.IsNullOrWhiteSpace(value: query) == false)
        {
            requestUri =
                $"{requestUri}?{query}";
        }
        Console.WriteLine($"requestUri={requestUri}");
        try
        {
           
            response =
            await
            Http.GetAsync(requestUri: requestUri);

            TResponse? result = null;

            var statusCode = ((int)response.StatusCode);
            if (statusCode >=200 || statusCode<=500)
            {
                var jsonSerializerOptions =
                new System.Text.Json.JsonSerializerOptions
                {
                    MaxDepth = 10,
                    PropertyNameCaseInsensitive = true,

                }; Console.WriteLine(nameof(statusCode) + "::" + statusCode.ToString());

                result = await response.Content.
               ReadFromJsonAsync<TResponse>(jsonSerializerOptions);

                if (result != null)
            {
                Console.WriteLine($"statusCode={statusCode}");
                SetProp(result,
                        "status", statusCode);
            }
            return result;
            }
            Console.WriteLine($"statusCode={statusCode}");

            return default;
        }


        catch(Exception ex)
        {
            Console.WriteLine($"ex={ex.InnerException}");

            return default;

        }

        finally
        {
            response?.Dispose();
        }
    }
    #endregion /GetAsync

    #region PostAsync
    protected virtual async
            Task<TResponse?> PostAsync<TRequest, TResponse>
            (string url, TRequest viewModel) where TResponse : class
    {
        HttpResponseMessage? response = null;
        TResponse? result = null;
        try
        {
            var requestUri = $"{url}";
            if (viewModel == null)
            {

            }
            response =
                await Http.PostAsJsonAsync
                (requestUri: requestUri, value: viewModel);
            var statusCode = ((int)response.StatusCode);

            var jsonSerializerOptions =
                new System.Text.Json.JsonSerializerOptions
                {
                    MaxDepth = 10,
                    PropertyNameCaseInsensitive = true,

                };
            result = await response.Content.
               ReadFromJsonAsync<TResponse>(jsonSerializerOptions);

            if (result != null)
            {
                SetProp(result,
                        "status", statusCode);
            }
            return result;

        }
        catch (Exception ex)
        {

            Console.WriteLine(ex.ToString());
            return default;
        }
        finally
        {
            if (result != null)
                response?.Dispose();
        }

    }
    //protected virtual
    //    async
    //    System.Threading.Tasks.Task<TResponse?>
    //    PostAsync<TRequest, TResponse>(string url, TRequest viewModel)
    //{
    //    System.Net.Http.HttpResponseMessage? response = null;

    //    try
    //    {
    //        var requestUri =
    //            $"{BaseApiUrl}/{url}";

    //        response =
    //            await Http.PostAsJsonAsync
    //            (requestUri: requestUri, value: viewModel);

    //        //response.EnsureSuccessStatusCode();

    //        if (response.IsSuccessStatusCode)
    //        {
    //            try
    //            {

    //                var jsonSerializerOptions =
    //                    new System.Text.Json.JsonSerializerOptions
    //                    {
    //                        MaxDepth = 5,
    //                    };

    //                var result =
    //                    await
    //                    response.Content.ReadFromJsonAsync
    //                    <TResponse>(options: jsonSerializerOptions);


    //                return result;
    //            }
    //            // When content type is not valid
    //            catch (System.NotSupportedException)
    //            {
    //                var errorMessage =
    //                    "The content type is not supported!";

    //               Console.WriteLine (errorMessage);
    //            }
    //            // Invalid JSON
    //            catch (System.Text.Json.JsonException)
    //            {
    //                var errorMessage = "Invalid JSON!";

    //                Console.WriteLine(errorMessage);

    //            }
    //        }
    //    }
    //    finally
    //    {
    //        response?.Dispose();
    //    }

    //    return default;
    //}
    #endregion PostAsync

    #region PutAsync

    protected virtual
        async
        Task<TResponse?>
        PutAsync<TRequest, TResponse>(string url, TRequest viewModel)
    {
        HttpResponseMessage? response = null;

        try
        {
            var requestUri =
                $"{BaseApiUrl}/{url}";

            response =
                await Http.PutAsJsonAsync
                (requestUri: requestUri, value: viewModel);

            response.EnsureSuccessStatusCode();

            if (response.IsSuccessStatusCode)
            {
                try
                {
                    var result =
                        await
                        response.Content.ReadFromJsonAsync<TResponse>();

                    return result;
                }
                // When content type is not valid
                catch (NotSupportedException)
                {
                    var errorMessage =
                        "The content type is not supported!";

                    LogsService.AddLog
                        (type: GetType(), message: errorMessage);
                }
                // Invalid JSON
                catch (System.Text.Json.JsonException)
                {
                    var errorMessage = "Invalid JSON!";

                    LogsService.AddLog
                        (type: GetType(), message: errorMessage);
                }
            }
        }
        catch (HttpRequestException ex)
        {
            LogsService.AddLog
                (type: GetType(), message: ex.Message);
        }
        finally
        {
            response?.Dispose();
        }

        return default;
    }

    #endregion /PutAsync

    #region DeleteAsync

    protected virtual
        async
        Task<TResponse?>
        DeleteAsync<TResponse>(string url, string? query = null)
    {
        HttpResponseMessage? response = null;

        try
        {
            var requestUri =
                $"{BaseApiUrl}/{url}";

            if (string.IsNullOrWhiteSpace(value: query) == false)
            {
                requestUri =
                    $"{requestUri}?{query}";
            }

            response =
                await Http.DeleteAsync(requestUri: requestUri);

            response.EnsureSuccessStatusCode();

            if (response.IsSuccessStatusCode)
            {
                try
                {
                    var result =
                        await
                        response.Content.ReadFromJsonAsync<TResponse>();

                    return result;
                }
                // When content type is not valid
                catch (NotSupportedException)
                {
                    var errorMessage =
                        "The content type is not supported!";

                    LogsService.AddLog
                        (type: GetType(), message: errorMessage);
                }
                // Invalid JSON
                catch (System.Text.Json.JsonException)
                {
                    var errorMessage = "Invalid JSON!";

                    LogsService.AddLog
                        (type: GetType(), message: errorMessage);
                }
            }
        }
        catch (HttpRequestException ex)
        {
            LogsService.AddLog
                (type: GetType(), message: ex.Message);
        }
        finally
        {
            response?.Dispose();
        }

        return default;
    }
    #endregion /DeleteAsync

    protected void
            SetProp<TSource>(TSource source,
            string propName, object value) where TSource : class
    {

        var prop = source.GetType().
            GetProperty(propName);
        if (prop != null)
        {
            prop.SetValue(source, value);
        }
    }
}
