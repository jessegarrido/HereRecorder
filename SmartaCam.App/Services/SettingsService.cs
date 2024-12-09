using Microsoft.AspNetCore.Mvc;
using static Dropbox.Api.Files.ListRevisionsMode;
using System.Net.Http;
using System.Text.Json;
using SmartaCam.App.Services;
using Newtonsoft.Json;

namespace SmartaCam
{
    public interface ISettingsService
    {
        Task<IActionResult> SetNormalize(bool willNormalize);
        Task<bool> GetNormalize();
        Task<IActionResult> SetUpload(bool willUpload);
        Task<bool> GetUpload();
        Task<IActionResult> SetCopyToUsb(bool willCopy);
        Task<bool> GetCopyToUsb();
        Task<bool> GetNetworkStatus();
    }

    public class SettingsService : ISettingsService
    {
        private readonly HttpClient _httpClient;
        public SettingsService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }
        public async Task<bool> GetNormalize()
        {
            return await System.Text.Json.JsonSerializer.DeserializeAsync<bool>
            // return OK(await _httpClient.GetStreamAsync($"api/getnormalized"))
            (await _httpClient.GetStreamAsync($"api/getnormalize"), new JsonSerializerOptions() { PropertyNameCaseInsensitive = true });
        }
        public async Task<IActionResult> SetNormalize(bool willNormalize)
        {
            return await _httpClient.GetAsync($"api/setnormalize/{willNormalize}") as IActionResult;
        }
        public async Task<bool> GetUpload()
        {
            return await System.Text.Json.JsonSerializer.DeserializeAsync<bool>
            // return OK(await _httpClient.GetStreamAsync($"api/getnormalized"))
            (await _httpClient.GetStreamAsync($"api/getpush"), new JsonSerializerOptions() { PropertyNameCaseInsensitive = true });
        }
        public async Task<IActionResult> SetUpload(bool willUpload)
        {
            return await _httpClient.GetAsync($"api/setpush/{willUpload}") as IActionResult;
        }
        public async Task<bool> GetCopyToUsb()
         {
            return await System.Text.Json.JsonSerializer.DeserializeAsync<bool>
            // return OK(await _httpClient.GetStreamAsync($"api/getnormalized"))
            (await _httpClient.GetStreamAsync($"api/getcopy"), new JsonSerializerOptions() { PropertyNameCaseInsensitive = true });
        }
        public async Task<IActionResult> SetCopyToUsb(bool willCopy)
        {
            return await _httpClient.GetAsync($"api/setcopy/{willCopy}") as IActionResult;
        }
        public async Task<bool> GetNetworkStatus()
        {
            return await System.Text.Json.JsonSerializer.DeserializeAsync<bool>
            // return OK(await _httpClient.GetStreamAsync($"api/getnormalized"))
            (await _httpClient.GetStreamAsync($"api/getnetwork"), new JsonSerializerOptions() { PropertyNameCaseInsensitive = true });
        }

    }
    }