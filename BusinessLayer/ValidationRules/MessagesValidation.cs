using EntityLayer.Concrete;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.ValidationRules
{
    public class MessagesValidation:AbstractValidator<Messages>
    {
        public MessagesValidation() 
        {
            RuleFor(x => x.Subject).NotEmpty().WithMessage("Lütfen konu yazınız");
            RuleFor(x => x.Subject).MaximumLength(100).WithMessage("Konu en fazla 100 karakter olabilir");
        }
    }
}
