using System.Text.Json;
using TCSTest.Models;

namespace TCSTest.Data
{

    public class JsonDataService
    {
        private readonly string _dataDir = Path.Combine(Directory.GetCurrentDirectory(), "Data");

        private T ReadData<T>(string filename)
        {
            var path = Path.Combine(_dataDir, filename);
            if (!File.Exists(path)) return Activator.CreateInstance<T>();
            var json = File.ReadAllText(path);
            return JsonSerializer.Deserialize<T>(json) ?? Activator.CreateInstance<T>();
        }

        private void WriteData<T>(string filename, T data)
        {
            var path = Path.Combine(_dataDir, filename);
            var json = JsonSerializer.Serialize(data, new JsonSerializerOptions { WriteIndented = true });
            File.WriteAllText(path, json);
        }

        public List<ContentCatalog> GetContent() => ReadData<List<ContentCatalog>>("content_catalog.json");
        public void SaveContent(List<ContentCatalog> data) => WriteData("content_catalog.json", data);

        public List<ChannelManager> GetChannels() => ReadData<List<ChannelManager>>("channels.json");
        public void SaveChannels(List<ChannelManager> data) => WriteData("channels.json", data);
        public List<ScheduleSystem> GetSchedule() => ReadData<List<ScheduleSystem>>("channel_schedule.json");
        public void SaveSchedule(List<ScheduleSystem> data) => WriteData("schedule.json", data);
    }
}

