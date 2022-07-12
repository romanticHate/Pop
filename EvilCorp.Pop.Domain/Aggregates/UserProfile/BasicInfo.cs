using EvilCorp.Pop.Domain.Exceptions;
using EvilCorp.Pop.Domain.Validators.UserProfile;

namespace EvilCorp.Pop.Domain.Aggregates.UserProfile
{
    public class BasicInfo
    {
        public BasicInfo()
        {

        }
        public string FirstName { get; private set; }
        public string LastName { get; private set; }
        public string EmailAddress { get; private set; }
        public string Phone { get; private set; }
        public DateTime DateOfBirth { get; private set; }
        public string CurrentCity { get; private set; }

        /// <summary>
        /// Create New BasicInfo instance
        /// </summary>
        /// <param name="firstName"></param>
        /// <param name="lastName"></param>
        /// <param name="emailAddress"></param>
        /// <param name="phone"></param>
        /// <param name="dateOfBirth"></param>
        /// <param name="currentCity"></param>
        /// <returns> <see cref="BasicInfo"/></returns>
        /// <exception cref="UserProfileNotValidException"></exception>
        public static BasicInfo CreateBasicInfo(string firstName, string lastName, string emailAddress,
            string phone, DateTime dateOfBirth, string currentCity)
        {
            #region Old Code ...

            //return new BasicInfo
            //{
            //    FirstName = firstName,
            //    LastName = lastName,
            //    EmailAddress = emailAddress,
            //    Phone = phone,
            //    DateOfBirth = dateOfBirth,
            //    CurrentCity = currentCity
            //};
            #endregion

            // To Do: fix validation, fix error handling, fix error notification
            var validator = new BasicInfoVldtr();
            var basicInfo = new BasicInfo
            {
                FirstName = firstName,
                LastName = lastName,
                EmailAddress = emailAddress,
                Phone = phone,
                DateOfBirth = dateOfBirth,
                CurrentCity = currentCity
            };
            var result = validator.Validate(basicInfo);
            if (result.IsValid) return basicInfo;
            var exception = new UserProfileNotValidException("The user profile is not valid");
            foreach (var error in result.Errors)
            {
                exception.Errors.Add(error.ErrorMessage);
            }
            throw exception;
           
        }
    }
}
