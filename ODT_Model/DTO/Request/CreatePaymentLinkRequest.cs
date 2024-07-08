namespace ODT_Model.DTO.Request;

public record CreatePaymentLinkRequest(
    long WalletID,
    string productName,
    string description,
    int price,
    string returnUrl,
    string cancelUrl
);