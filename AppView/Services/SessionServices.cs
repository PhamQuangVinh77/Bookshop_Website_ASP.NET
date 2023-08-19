using AppData.Models;
using Newtonsoft.Json;
using System.Net.NetworkInformation;

namespace AppView.Services
{
    public static class SessionServices
    {
        // 1. Đọc dữ liệu từ Session => Trả về 1 list
        public static List<CartDetail> GetObjFromSession(ISession session, string key)
        {
            string jsonData = session.GetString(key); // Lấy dữ liệu dạng string từ session
            if (jsonData == null)
            {
                return new List<CartDetail>(); // Tạo 1 list mới để chứa sp khi dữ liệu null => Session chưa được tạo ra
            }
            else
            {
                var cartDetails = JsonConvert.DeserializeObject<List<CartDetail>>(jsonData); // Nếu có dữ liệu thì sẽ chuyển dữ liệu về dạng list
                return cartDetails;
            }
        }
        // 2. Ghi đè dữ liệu vào Session từ 1 list
        public static void SetObjToSession(ISession session, string key, object data)
        {
            var jsonData = JsonConvert.SerializeObject(data); // Chuyển dữ liệu truyền vào về jsonData
            session.SetString(key, jsonData); // Ghi đè vào session
        }

        // 3. Kiểm tra xem 1 đối tượng có trong 1 list hay không
        public static bool CheckObjInList(Guid id, List<CartDetail> cartDetails)
        {
            return cartDetails.Any(p => p.BookId == id);
        }
    }
}

