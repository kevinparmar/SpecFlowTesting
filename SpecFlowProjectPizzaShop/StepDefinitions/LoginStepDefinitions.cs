using System;
using TechTalk.SpecFlow;
using OpenQA.Selenium;
using NUnit.Framework;

namespace SpecFlowProjectPizzaShop.StepDefinitions
{
    [Binding]
    public class LoginStepDefinitions
    {
        private IWebDriver _driver;

        public LoginStepDefinitions(IWebDriver driver) 
        {
            _driver = driver;
        }

        [Given(@"I am on the login page")]
        public void GivenIAmOnTheLoginPage()
        {
            _driver.Navigate().GoToUrl("https://localhost:7013/Identity/Account/Login");
            Thread.Sleep(1000);
        }

        [When(@"I enter my username and password")]
        public void WhenIEnterMyUsernameAndPassword()
        {
            IWebElement emailInput = _driver.FindElement(By.XPath("//*[@id=\"Input_Email\"]"));
            IWebElement passwordInput = _driver.FindElement(By.XPath("//*[@id=\"Input_Password\"]"));

            emailInput.Clear();
            emailInput.SendKeys("user@mail.com");

            passwordInput.Clear();
            passwordInput.SendKeys("Pa$$w0rd");          
        }

        [When(@"I click on login button")]
        public void WhenIClickOnLoginButton()
        {
            IWebElement loginButton = _driver.FindElement(By.XPath("//*[@id=\"login-submit\"]"));
            
            Thread.Sleep(1000);
            loginButton.Click();
        }

        [Then(@"I should be logged in successfully")]
        public void ThenIShouldBeLoggedInSuccessfully()
        {
            IWebElement welcomeMessage = _driver.FindElement(By.XPath("//*[@id='username']"));
            Assert.IsTrue(welcomeMessage.Displayed);
            Assert.IsTrue(welcomeMessage.Text.Contains("Hello"));
        }

        [When(@"I enter invalid username and password")]
        public void WhenIEnterInvalidUsernameAndPassword()
        {
            IWebElement emailInput = _driver.FindElement(By.XPath("//*[@id=\"Input_Email\"]"));
            IWebElement passwordInput = _driver.FindElement(By.XPath("//*[@id=\"Input_Password\"]"));

            emailInput.Clear(); 
            emailInput.SendKeys("user@mail.com"); 

            passwordInput.Clear();
            passwordInput.SendKeys("invalidpassword");
        }

        [When(@"I click the login button")]
        public void WhenIClickTheLoginButton()
        {
            IWebElement loginButton = _driver.FindElement(By.XPath("//*[@id=\"login-submit\"]"));

            Thread.Sleep(1000);
            loginButton.Click();
        }

        [Then(@"I should see an error message")]
        public void ThenIShouldSeeAnErrorMessage()
        {
            IWebElement errorMessage = _driver.FindElement(By.XPath("//div[@class='text-danger validation-summary-errors']/ul/li[text()='Invalid login attempt.']"));
            Assert.IsTrue(errorMessage.Displayed);
        }
    }
}
