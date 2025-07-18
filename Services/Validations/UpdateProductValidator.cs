using App.Services.DTOs.ProductDTOs;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Services.Validations
{
    public class UpdateProductValidator:AbstractValidator<UpdateProductRequest>
    {
        public UpdateProductValidator()
        {
            //NotNull->Nullable geçebilir olan string proplarımda geçerlidir.
            RuleFor(x => x.Name).NotNull().NotEmpty().WithMessage("Lütfen Ürün İsmini Boş Bırakmayınız!").Length(3, 30).WithMessage("Ürün İsmi 3 İle 30 Karakter Arasında Olmalıdır");
            RuleFor(x => x.Price).GreaterThan(0).WithMessage("Ürün Fiyatım 0'dan büyük olmalıdır!");
            RuleFor(x => x.Stock).InclusiveBetween(1, 100).WithMessage("Ürün Stoğum 1 ile 100 Arasında Olmalıdır!");
        }
    }
}
