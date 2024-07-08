using FluentValidation;
using NetCoreIdentityAdminPanel.Models.Admins.AppRoles.RequestModels;

namespace NetCoreIdentityAdminPanel.Models.Validators
{
    public class CreateRoleValidator : AbstractValidator<CreateRoleRequestModel>
    {
        public CreateRoleValidator()
        {
            RuleFor(x => x.RoleName).NotNull().WithMessage("Rol ismi girilmesi zorunludur");

        }
    }
}
