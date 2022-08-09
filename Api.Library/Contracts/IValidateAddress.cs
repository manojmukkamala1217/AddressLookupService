using Api.Library.Validations;

namespace Api.Library.Contracts
{
	public interface IValdiateAddress
	{
		bool IsAddressValid(string address);
		AddressType GetAddressType(string address);
	}
}
