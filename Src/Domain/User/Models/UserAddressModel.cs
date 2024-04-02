using Domain.User.Records;

namespace Domain.User.Models;

public class UserAddressModel
{
    public string Street { get; set; } = string.Empty;

    public int Number { get; set; }

    public string ZipCode { get; set; } = string.Empty;

    public string Neighborhood { get; set; } = string.Empty;

    public string City { get; set; } = string.Empty;

    public string State { get; set; } = string.Empty;

    public string Country { get; set; } = string.Empty;

    public Guid UserId { get; private set; }

    public UserModel User { get; private set; } = null!;

    public static implicit operator UserAddressModel(UserAddress address)
    {
        return new()
        {
            Street = address.Street,
            Number = address.Number,
            ZipCode = address.ZipCode,
        };
    }
}
