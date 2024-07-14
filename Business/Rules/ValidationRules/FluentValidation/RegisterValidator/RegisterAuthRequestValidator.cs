﻿using Business.Dtos.Requests.AuthRequests;
using FluentValidation;

namespace Business.Rules.FluentValidation;

public class RegisterAuthRequestValidator : AbstractValidator<RegisterAuthRequest>
{
    public RegisterAuthRequestValidator()
    {
        RuleFor(u => u.FirstName).NotEmpty();
        RuleFor(u => u.LastName).NotEmpty();
        RuleFor(u => u.Email).NotEmpty();
        RuleFor(u => u.Email).EmailAddress().WithMessage("Geçersiz e-posta formatı.");
        RuleFor(u => u.Password).NotEmpty();
        RuleFor(u => u.Password).MinimumLength(6);
        RuleFor(u => u.Password).Matches(@"[A-Z]").WithMessage("Parola en az bir büyük harf içermelidir.");
        RuleFor(u => u.Password).Matches(@"[a-z]").WithMessage("Parola en az bir küçük harf içermelidir.");
        RuleFor(u => u.Password).Matches(@"\d").WithMessage("Parola en az bir rakam içermelidir.");
    }
}