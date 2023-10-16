using NUnit.Framework;
using OpenQA.Selenium;
using System;
using System.Xml.Linq;
using TechTalk.SpecFlow;

namespace SpecFlowProjectPizzaShop.StepDefinitions
{
    [Binding]
    public class UserRegistrationStepDefinitions
    {
        private IWebDriver _driver;
        private string newUserName = "user9@mail.com";
        private string existingUserName = "user@mail.com";
        private string invalidUserName = "usermail.com";
        private string passsword = "Pa$$w0rd";
        private string anotherPassword = "Pa$$w9rd";

        public UserRegistrationStepDefinitions(IWebDriver driver)
        {
            _driver = driver;
        }

        [Given(@"I am on the registration page")]
        public void GivenIAmOnTheRegistrationPage()
        {
            _driver.Navigate().GoToUrl("https://localhost:7013/Identity/Account/Register");
            Thread.Sleep(500);
        }

        [When(@"I enter my email")]
        public void WhenIEnterMyEmail()
        {
            IWebElement emailInput = _driver.FindElement(By.XPath("//*[@id=\"Input_Email\"]"));
            emailInput.Clear();
            emailInput.SendKeys(newUserName);   
        }

        [When(@"I enter a password")]
        public void WhenIEnterAPassword()
        {
            IWebElement passwordInput1 = _driver.FindElement(By.XPath("//*[@id=\"Input_Password\"]"));
            passwordInput1.Clear();
            passwordInput1.SendKeys(passsword);
        }

        [When(@"I confirm the password")]
        public void WhenIConfirmThePassword()
        {
            IWebElement passwordInput2 = _driver.FindElement(By.XPath("//*[@id=\"Input_ConfirmPassword\"]"));
            passwordInput2.Clear();
            passwordInput2.SendKeys(passsword);
        }

        [When(@"I click the register button")]
        public void WhenIClickTheRegisterButton()
        {
            IWebElement registrationButton = _driver.FindElement(By.XPath("//*[@id=\"registerSubmit\"]"));

            Thread.Sleep(5000);
            registrationButton.Click();
        }

        [When(@"I see registration confirmation message")]
        public void WhenISeeRegistrationConfirmationMessage()
        {
            Thread.Sleep(500);
            IWebElement confirmMessage = _driver.FindElement(By.XPath("/html/body/div/main/h1"));
            //Assert.IsTrue(confirmMessage.Displayed);
            Assert.IsTrue(confirmMessage.Text.Contains("Register confirmation"));
        }

        [When(@"I click on confirm account link")]
        public void WhenIClickOnConfirmAccountLink()
        {
            IWebElement confirmLink = _driver.FindElement(By.XPath("//*[@id=\"confirm-link\"]"));
            Thread.Sleep(500);
            confirmLink.Click();
        }

        [Then(@"I should see a confirmed email message")]
        public void ThenIShouldSeeAConfirmedEmailMessage()
        {
            IWebElement confirmMessage = _driver.FindElement(By.XPath("/ html / body / div / main / div"));
            Thread.Sleep(500);
            Assert.IsTrue(confirmMessage.Text.Contains("Thank you for confirming your email"));
        }

        [When(@"I confirm with another password")]
        public void WhenIConfirmWithAnotherPassword()
        {
            IWebElement passwordInput2 = _driver.FindElement(By.XPath("//*[@id=\"Input_ConfirmPassword\"]"));
            passwordInput2.Clear();
            passwordInput2.SendKeys(anotherPassword);
        }

        [Then(@"I should see a password mismatch error message")]
        public void ThenIShouldSeeAPasswordMismatchErrorMessage()
        { 
            IWebElement passwordMismatch = _driver.FindElement(By.XPath("//*[@id=\"Input_ConfirmPassword-error\"]"));
            Thread.Sleep(500);
            Assert.IsTrue(passwordMismatch.Text.Contains("The password and confirmation password do not match."));
        }

        [When(@"I enter an invalid email")]
        public void WhenIEnterAnInvalidEmail()
        {
            IWebElement emailInput = _driver.FindElement(By.XPath("//*[@id=\"Input_Email\"]"));
            emailInput.Clear();
            emailInput.SendKeys(invalidUserName);
        }

        [When(@"I move to the password field")]
        public void WhenIMoveToThePasswordField()
        {
            // Password field has XPath ("//*[@id="Input_Password"]")
            IWebElement passwordField = _driver.FindElement(By.XPath("//*[@id=\"Input_Password\"]"));
            IJavaScriptExecutor js = (IJavaScriptExecutor)_driver;
            js.ExecuteScript("arguments[0].focus();", passwordField);
        }

        [Then(@"I should see an invalid email error message")]
        public void ThenIShouldSeeAnInvalidEmailErrorMessage()
        {
            IWebElement invalidEmailMessage = _driver.FindElement(By.XPath("//*[@id=\"Input_Email-error\"]"));
            Thread.Sleep(500);
            Assert.IsTrue(invalidEmailMessage.Text.Contains("The Email field is not a valid e-mail address."));
        }

        [When(@"I enter existing email")]
        public void WhenIEnterExistingEmail()
        {
            IWebElement emailInput = _driver.FindElement(By.XPath("//*[@id=\"Input_Email\"]"));
            emailInput.Clear();
            emailInput.SendKeys(existingUserName);
        }

        [Then(@"I should see a email already exists error message")]
        public void ThenIShouldSeeAEmailAlreadyExistsErrorMessage()
        {
            IWebElement emailExistsMessage = _driver.FindElement(By.XPath("//*[@id=\"registerForm\"]/div[1]/ul/li"));
            Thread.Sleep(500);
            Assert.IsTrue(emailExistsMessage.Text.Contains("is already taken."));
        }
    }
}
