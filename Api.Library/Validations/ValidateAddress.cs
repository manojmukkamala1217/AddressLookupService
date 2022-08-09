using Api.Library.Contracts;
using System.Net;
using System.Text.RegularExpressions;

namespace Api.Library.Validations
{
	public class ValidateAddress : IValdiateAddress
    {
        public bool IsAddressValid(string address)
        {
            return GetAddressType(address) != AddressType.None;
        }

        public AddressType GetAddressType(string address)
        {
            if (string.IsNullOrWhiteSpace(address)) return AddressType.None;
            else if (IsUrlValid(address)) return AddressType.Domain;
            else if (IsIpAddressValid(address)) return AddressType.IPAddress;
            else return AddressType.None;
        }

        private bool IsUrlValid(string url)
        {
            string pattern = @"^(http|https|)\://|[a-zA-Z0-9\-\.]+\.[a-zA-Z](:[a-zA-Z0-9]*)?/?([a-zA-Z0-9\-\._\?\,\'/\\\+&amp;%\$#\=~])*[^\.\,\)\(\s]$";
            Regex reg = new Regex(pattern, RegexOptions.Compiled | RegexOptions.IgnoreCase);
            //
            return reg.IsMatch(url);
        }

        private bool IsIpAddressValid(string address)
        {
            IPAddress ipAddress;
            //
            return IPAddress.TryParse(address, out ipAddress);
        }
    }
}