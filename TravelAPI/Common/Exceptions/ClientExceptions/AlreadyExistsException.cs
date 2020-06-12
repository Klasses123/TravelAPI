using System;

namespace TravelAPI.Common.Exceptions.ClientExceptions
{
    public class AlreadyExistsException : Exception
    {
        public string ParamName { get; protected set; }
        public AlreadyExistsException(string nameOfEntity) : base(nameOfEntity) { }
        /// <summary>
        /// existsParameter в творительном падеже для корректности вывода ошибки на фронте
        /// </summary>
        public AlreadyExistsException(string nameOfEntity, string existsParameter) : base (nameOfEntity) 
        {
            ParamName = existsParameter;
        }
    }
}
