// using System;
// using System.Collections.Generic;
// using System.Linq;
// using System.Threading.Tasks;
// using FluentValidation;
// using Pisaz.Backend.API.DTOs.Clients.SignIn;
// using Pisaz.Backend.API.Models.ClientModels;

// namespace Pisaz.Backend.API.Validations.ClientValidations
// {
//     public class ClientValidator : AbstractValidator<ClientAddDTO>
//     {
//         public ClientValidator()
//         {
//             RuleFor(client => client.PhoneNumber)
//                 .NotEmpty().WithMessage("Phone number is required.")
//                 .Matches(@"^09\d{9}$").WithMessage("Phone number must start with '09' and contain 11 digits.");

//             RuleFor(client => client.FirstName)
//                 .NotEmpty().WithMessage("First name is required.")
//                 .MaximumLength(40).WithMessage("First name cannot exceed 40 characters.");

//             RuleFor(client => client.LastName)
//                 .NotEmpty().WithMessage("Last name is required.")
//                 .MaximumLength(40).WithMessage("Last name cannot exceed 40 characters.");
//         }
//     }
// }