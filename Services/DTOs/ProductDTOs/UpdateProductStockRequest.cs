namespace App.Services.DTOs.ProductDTOs
{
    //Eğerki ikiden fazla parametrem içeriyorsa metodum request class oluşturabiliriz.
    public record UpdateProductStockRequest(int productId, int quantity);
}
