using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;

namespace TravelAPI.Common.Exceptions.ClientExceptions
{
    public class IdentityUserException : Exception
    {
        public IEnumerable<IdentityError> Errors { get; protected set; }
        public string Description { get; protected set; }
        public IdentityUserException(IdentityResult failedResult) 
        {
            Errors = failedResult.Errors;
            Description = IdentityUserExceptionDefinition(Errors);
        }

        private string IdentityUserExceptionDefinition(IEnumerable<IdentityError> errors)
        {
            //если будут найдены ещё ошибки при Identity, дописать их обработку 
            //через .Replace("error.code", "desired message")
            //P.S. название ошибки типа IdentityUserExc.. (в IdentityResult.Errors Error.code)
            //будет автоматически отправлена в виде "DuplicateUserName" на фронт и
            //залогирована, если не описано её переопределение тут
            var result = string.Join(", ", errors.Select(e => e.Code));
            return result
                .Replace("DuplicateUserName", "Имя пользователя уже занято")
                .Replace("InvalidUserName", "Недопустимый логин")
                .Replace("DuplicateEmail", "Такой E-mail уже занят");
        }
    }
}
