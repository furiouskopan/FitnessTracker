using FitnessTrackerAPI.DTOs;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace FitnessTrackerMAUI.Services
{
    public class FitnessTrackerApiService
    {
        private readonly HttpClient _httpClient;

        public FitnessTrackerApiService(HttpClient httpClient)
        {
            _httpClient = httpClient;
            _httpClient.BaseAddress = new Uri("https://localhost:7183");
        }       

        // Get all Exercise Logs
        public async Task<List<ExerciseLogDTO>> GetExerciseLogsAsync()
        {
            var response = await _httpClient.GetFromJsonAsync<List<ExerciseLogDTO>>("api/ExerciseLog");
            return response ?? new List<ExerciseLogDTO>();
        }

        // Get a single Exercise Log by Id
        public async Task<ExerciseLogDTO> GetExerciseLogAsync(int id)
        {
            var response = await _httpClient.GetFromJsonAsync<ExerciseLogDTO>($"api/ExerciseLog/{id}");
            return response;
        }

        // Post a new Exercise Log
        public async Task<ExerciseLogDTO> CreateExerciseLogAsync(ExerciseLogCreateDTO createDTO)
        {
            var response = await _httpClient.PostAsJsonAsync("api/ExerciseLog", createDTO);
            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadFromJsonAsync<ExerciseLogDTO>();
            }
            return null;
        }

        // Update an existing Exercise Log
        public async Task<ExerciseLogDTO> UpdateExerciseLogAsync(int id, ExerciseLogCreateDTO updateDTO)
        {
            var response = await _httpClient.PutAsJsonAsync($"api/ExerciseLog/{id}", updateDTO);
            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadFromJsonAsync<ExerciseLogDTO>();
            }
            return null;
        }

        // Delete an Exercise Log
        public async Task<bool> DeleteExerciseLogAsync(int id)
        {
            var response = await _httpClient.DeleteAsync($"api/ExerciseLog/{id}");
            return response.IsSuccessStatusCode;
        }

        // Get all Exercises
        //public async Task<IEnumerable<ExerciseDTO>> GetExercisesAsync()
        //{
        //    var response = await _httpClient.GetAsync("api/Exercise");
        //    response.EnsureSuccessStatusCode();
        //    return await response.Content.ReadFromJsonAsync<IEnumerable<ExerciseDTO>>();
        //}
        public async Task<List<ExerciseDTO>> GetExercisesAsync()
        {
            return await _httpClient.GetFromJsonAsync<List<ExerciseDTO>>("api/Exercise");
        }

        // Get a single Exercise by Id
        public async Task<ExerciseDTO> GetExerciseAsync(int id)
        {
            var response = await _httpClient.GetFromJsonAsync<ExerciseDTO>($"api/Exercise/{id}");
            return response;
        }

        // Post a new Exercise
        public async Task<ExerciseDTO> CreateExerciseAsync(ExerciseCreateDTO createDTO)
        {
            var response = await _httpClient.PostAsJsonAsync("api/Exercise", createDTO);
            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadFromJsonAsync<ExerciseDTO>();
            }
            return null;
        }

        // Update an existing Exercise
        public async Task<bool> UpdateExerciseAsync(int id, ExerciseCreateDTO updateDTO)
        {
            var response = await _httpClient.PutAsJsonAsync($"api/Exercise/{id}", updateDTO);
            return response.IsSuccessStatusCode;
        }


        // Delete an Exercise
        public async Task<bool> DeleteExerciseAsync(int id)
        {
            var response = await _httpClient.DeleteAsync($"api/Exercise/{id}");
            return response.IsSuccessStatusCode;
        }
    }
}
