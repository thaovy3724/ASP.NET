using Microsoft.AspNetCore.Http;
using System.Globalization;
using System.Net;
using System.Net.Http;
using System.Security.Cryptography;
using System.Text;

namespace SM.Infrastructure.Adapters.Payment.Libs
{
    public class VnPayLibrary
    {
        private readonly SortedList<string, string> _requestData = new SortedList<string, string>(new VnPayCompare());
        private readonly SortedList<string, string> _responseData = new SortedList<string, string>(new VnPayCompare());

        // Thêm dữ liệu gửi đi
        public void AddRequestData(string key, string value)
        {
            if (!string.IsNullOrEmpty(value)) _requestData.Add(key, value);
        }

        // Thêm dữ liệu nhận về (khi VNPay callback)
        public void AddResponseData(string key, string value)
        {
            if (!string.IsNullOrEmpty(value)) _responseData.Add(key, value);
        }

        // Lấy dữ liệu trả về
        public string GetResponseData(string key)
        {
            return _responseData.TryGetValue(key, out var retValue) ? retValue : string.Empty;
        }

        // TẠO URL THANH TOÁN
        public string CreateRequestUrl(string baseUrl, string vnp_HashSecret)
        {
            StringBuilder data = new StringBuilder();
            foreach (KeyValuePair<string, string> kv in _requestData)
            {
                if (data.Length > 0) data.Append('&');
                data.Append(kv.Key + "=" + WebUtility.UrlEncode(kv.Value));
            }

            string queryString = data.ToString();
            string vnp_SecureHash = Utils.HmacSHA512(vnp_HashSecret, queryString);

            return $"{baseUrl}?{queryString}&vnp_SecureHash={vnp_SecureHash}";
        }

        // KIỂM TRA CHỮ KÝ (Khi nhận phản hồi)
        public bool ValidateSignature(string inputHash, string secretKey)
        {
            string rspRaw = GetResponseData();
            string myChecksum = Utils.HmacSHA512(secretKey, rspRaw);
            return myChecksum.Equals(inputHash, StringComparison.InvariantCultureIgnoreCase);
        }

        private string GetResponseData()
        {
            StringBuilder data = new StringBuilder();
            if (_responseData.ContainsKey("vnp_SecureHashType")) _responseData.Remove("vnp_SecureHashType");
            if (_responseData.ContainsKey("vnp_SecureHash")) _responseData.Remove("vnp_SecureHash");

            foreach (KeyValuePair<string, string> kv in _responseData)
            {
                if (data.Length > 0) data.Append('&');
                data.Append(kv.Key + "=" + WebUtility.UrlEncode(kv.Value));
            }
            return data.ToString();
        }
    }

    // Class so sánh để sắp xếp tham số a-z
    public class VnPayCompare : IComparer<string>
    {
        public int Compare(string x, string y)
        {
            if (x == y) return 0;
            if (x == null) return -1;
            if (y == null) return 1;
            var vnpCompare = CompareInfo.GetCompareInfo("en-US");
            return vnpCompare.Compare(x, y, CompareOptions.Ordinal);
        }
    }

    // Class mã hóa
    public static class Utils
    {
        public static string HmacSHA512(string key, string inputData)
        {
            var hash = new StringBuilder();
            byte[] keyBytes = Encoding.UTF8.GetBytes(key);
            byte[] inputBytes = Encoding.UTF8.GetBytes(inputData);
            using (var hmac = new HMACSHA512(keyBytes))
            {
                byte[] hashValue = hmac.ComputeHash(inputBytes);
                foreach (var theByte in hashValue) hash.Append(theByte.ToString("x2"));
            }
            return hash.ToString();
        }

        public static string GetIpAddress(HttpContext context)
        {
            var ipAddress = string.Empty;
            try
            {
                var remoteIpAddress = context.Connection.RemoteIpAddress;
                if (remoteIpAddress != null)
                {
                    if (remoteIpAddress.AddressFamily == System.Net.Sockets.AddressFamily.InterNetworkV6)
                    {
                        remoteIpAddress = System.Net.Dns.GetHostEntry(remoteIpAddress).AddressList
                            .FirstOrDefault(x => x.AddressFamily == System.Net.Sockets.AddressFamily.InterNetwork);
                    }
                    ipAddress = remoteIpAddress.ToString();
                }
            }
            catch (Exception ex)
            {
                return "127.0.0.1"; // Mặc định nếu không lấy được
            }
            return ipAddress;
        }
    }
}