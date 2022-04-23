using FluentValidation.TestHelper;
using NUnit.Framework;
using TaskManager.Entities;
using TaskManager.Models;
using TaskManager.Validators;

namespace TaskManager.Tests.Validators
{
    [TestFixture]
    public class RegisterUserValidatorTester
    {
        private RegisterUserValidator _validator;

        [SetUp]
        public void Setup()
        {
            var context = new InitiativeContext();
            _validator = new RegisterUserValidator(context);
        }

        [Test]
        public void ShouldNotHaveErrorsWhenUserDataAreValid()
        {
            var model = CreateValidUserModel();
            var result = _validator.TestValidate(model);
            result.ShouldNotHaveValidationErrorFor(user => user.Email);
            result.ShouldNotHaveValidationErrorFor(user => user.Password);
            result.ShouldNotHaveValidationErrorFor(user => user.ConfirmPassword);
        }

        [Test]
        public void ShouldHaveErrorsWhenEmailIsEmpty()
        {
            var model = CreateValidUserModel();
            model.Email = string.Empty;
            var result = _validator.TestValidate(model);
            result.ShouldHaveValidationErrorFor(user => user.Email);
        }

        [Test]
        public void ShouldHaveErrorsWhenPasswordIsTooShort()
        {
            var model = CreateValidUserModel();
            model.Password = "$H@r7";
            var result = _validator.TestValidate(model);
            result.ShouldHaveValidationErrorFor(user => user.Password);
        }

        [Test]
        public void ShouldHaveErrorsWhenPasswordIsLong()
        {
            var model = CreateValidUserModel();
            model.Password = "£@oooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooN9";
            var result = _validator.TestValidate(model);
            result.ShouldHaveValidationErrorFor(user => user.Password);
        }

        [Test]
        public void ShouldHaveErrorsWhenPasswordHasOnlyBigLetters()
        {
            var model = CreateValidUserModel();
            model.Password = "$O[IVEJFE#233D#D@";
            var result = _validator.TestValidate(model);
            result.ShouldHaveValidationErrorFor(user => user.Password);
        }

        [Test]
        public void ShouldHaveErrorsWhenPasswordHasOnlySmallLetters()
        {
            var model = CreateValidUserModel();
            model.Password = "$o[ivejfe#233d#d@";
            var result = _validator.TestValidate(model);
            result.ShouldHaveValidationErrorFor(user => user.Password);
        }

        [Test]
        public void ShouldHaveErrorsWhenPasswordHasNoNumbers()
        {
            var model = CreateValidUserModel();
            model.Password = "$o[ivejFE#uuuD#D@";
            var result = _validator.TestValidate(model);
            result.ShouldHaveValidationErrorFor(user => user.Password);
        }

        [Test]
        public void ShouldHaveErrorsWhenPasswordHasNorSymbolNorPunctuation()
        {
            var model = CreateValidUserModel();
            model.Password = "sotivejFEt233DtDt";
            var result = _validator.TestValidate(model);
            result.ShouldHaveValidationErrorFor(user => user.Password);
        }

        [Test]
        public void ShouldNotHaveErrorsWhenPasswordHasSymbolButNoPunctuation()
        {
            var model = CreateValidUserModel();
            model.Password = "$otivejFEt233DtDt";
            var result = _validator.TestValidate(model);
            result.ShouldNotHaveValidationErrorFor(user => user.Password);
        }

        [Test]
        public void ShouldNotHaveErrorsWhenPasswordHasPunctuationButNoSymbol()
        {
            var model = CreateValidUserModel();
            model.Password = "sot:vejFEt233DtDt";
            var result = _validator.TestValidate(model);
            result.ShouldNotHaveValidationErrorFor(user => user.Password);
        }

        [Test]
        public void ShouldHaveErrorsWhenPasswordIsEmpty()
        {
            var model = CreateValidUserModel();
            model.Password = string.Empty;
            var result = _validator.TestValidate(model);
            result.ShouldHaveValidationErrorFor(user => user.Password);
        }

        [Test]
        public void ShouldHaveErrorsWhenConfirmPasswordIsEmpty()
        {
            var model = CreateValidUserModel();
            model.ConfirmPassword = string.Empty;
            var result = _validator.TestValidate(model);
            result.ShouldHaveValidationErrorFor(user => user.ConfirmPassword);
        }

        [Test]
        public void ShouldHaveErrorsWhenConfirmPasswordDiffersFromPassword()
        {
            var model = CreateValidUserModel();
            model.ConfirmPassword = "$o[ivejFE#233D#d@";
            var result = _validator.TestValidate(model);
            result.ShouldHaveValidationErrorFor(user => user.ConfirmPassword);
        }

        private RegisterUserDto CreateValidUserModel()
        {
            return new RegisterUserDto
            {
                Email = "test@test.com",
                Password = "$o[ivejFE#233D#D@",
                ConfirmPassword = "$o[ivejFE#233D#D@",
                Nationality = "national",
                DateOfBirth = System.DateTime.Now
            };
        }
    }
}
